using System.Collections.Generic;
using System.Threading.Tasks;
using GameResOrg.Glue;
using GameResOrg.Intermediate;
using GameResOrg.Logic.Configuration;
using GameResOrg.Logic.Team;
using GameResOrg.Logic.Team.Models;
using JetBrains.Annotations;

namespace GameResOrg
{
	[UsedImplicitly]
    public sealed class ManageUsersController : PageController
    {
		private ICollaboratorService _collabService;
		private UserModel            _currentUser;
		private Privileges[]         _indexes;

		public ManageUsersController(ICollaboratorService collabService)
		{
			_collabService = collabService;
			_indexes       = new[] { Privileges.ManageObjects, Privileges.ManageDefinitions, Privileges.ManageGroups };
		}

		private int _rights;

		private bool _admin;

		public bool Admin
		{
			get { return _admin; }
			set
			{
				if (value != _admin)
				{
					_admin = value;
					OnPropertyChanged(nameof(Admin));
				}
			}
		}

		private UserModel _selectedUser;

		public bool IsSelected { get; set; }

		public UserModel SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				_selectedUser = value;
				IsSelected = value != null;
				OnPropertyChanged("IsSelected");
			}
		}

		public bool Toggle1
		{
			get => this[1];
			set => this[1] = value;
		}
		public bool Toggle2
		{
			get => this[2];
			set => this[2] = value;
		}
		public bool Toggle3
		{
			get => this[3];
			set => this[3] = value;
		}

		public bool this[int index]
		{
			get
			{
				var  priv = _indexes[index - 1];
				bool has  = (_rights & (int)priv) == (int)priv;
				return has;
			}
			set
			{
				var priv = _indexes[index - 1];
				if (value)
					_rights |= (int)priv;
				else
					_rights &= ~(int)priv;
			}
		}

		public void SetContext(UserModel user)
		{
			_currentUser = user;
			if (user != null)
			{
				bool t1 = Toggle1;
				bool t2 = Toggle2;
				bool t3 = Toggle3;
				_rights = user.User.Privileges;
				Admin   = _rights == (int)Privileges.Admin;
				if(t1 != Toggle1)
					OnPropertyChanged("Toggle1");
				if(t2 != Toggle2)
					OnPropertyChanged("Toggle2");
				if(t3 != Toggle3)
					OnPropertyChanged("Toggle3");
			}
		}

		[NotNull]
		public Task<List<UserModel>> GetUsers()
		{
			return Bouncer.StartSingleTask(() => _collabService.GetCollaborators());
		}

		[NotNull]
		public Task SavePrivileges()
		{
			if (Admin)
				_rights = (int)Privileges.Admin;

			_currentUser.User.Privileges = _rights;
			var user = _currentUser;
			return Bouncer.StartSingleTask(() =>
				  {
					  _collabService.SavePrivileges(user.User, user.IsNew);
					  user.IsNew = false;
				  }).AwaitCall();
		}
    }
}
