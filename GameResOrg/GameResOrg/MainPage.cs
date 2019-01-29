using System.Threading.Tasks;
using GameResOrg.Glue;
using GameResOrg.Logic.Configuration;
using GameResOrg.Logic.Infrastructure.Internal;

namespace GameResOrg
{
	public sealed class MainPageController : PageController
	{
		private IPrivilegeService _privilegeService;
		private InternalsService _dataService;

		public MainPageController(IPrivilegeService privilegeService, InternalsService dataService)
		{
			_privilegeService = privilegeService;
			_dataService = dataService;
		}

		public bool CanManagePrivileges => _privilegeService.CanManageUsers();

		public Task<int> GetNewMessagesCount()
		{
			return Bouncer.StartSingleTask(() => _dataService.GetNewDataInfo()?.NewGroupMessages ?? 0);
		}

	}
}
