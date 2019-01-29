using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GameResOrg.Glue;
using GameResOrg.Intermediate;
using GameResOrg.Logic.Configuration;
using GameResOrg.Logic.Configuration.Models;
using JetBrains.Annotations;

namespace GameResOrg
{
	[UsedImplicitly]
	public sealed class LoginPageController : PageController
	{
		private IDbConfig    _dbConfig;
		private IUserService _service;
		private LoginModel   _model;

		public LoginPageController(IUserService service, IDbConfig dbConfig)
		{
			_service = service;
			_dbConfig = dbConfig;
			_model = new LoginModel();
		}

		public string Server { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
		}

		public async Task<FailInfo> TryLogin()
		{
			_model.Password = Password;
			_model.Email = Email;

			var conn = new SqlConnectionStringBuilder()
			{
				DataSource     = Server,
				InitialCatalog = "GRWOMain",
				UserID         = "GRWO_DPUser",
				Password       = "GRW0$ecDefPerUL",
			};
			var fail = await TestConnection(conn);
			if (fail != null)
				return fail;

			_dbConfig.SaveConnection(conn, DbConnectionType.StandardMain);
			return await Bouncer.StartSingleTask(() => _service.Login(_model)).AwaitCall();
		}

		private Task<FailInfo> TestConnection(SqlConnectionStringBuilder connBuilder)
		{
			var connString = connBuilder.ToString();
			return Bouncer.StartSingleTask(() => {
				using (SqlConnection connection = new SqlConnection(connString))
				{
					try
					{
						if (connection.State == ConnectionState.Closed)
							connection.Open();
					}
					catch (SqlException e)
					{
						throw new ActionFailException("Connection error. Check server address.");
					}
				}
			}).AwaitCall();
		}
	}
}
