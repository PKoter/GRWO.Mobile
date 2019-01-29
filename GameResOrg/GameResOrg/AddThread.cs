using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameResOrg.Data.Project;
using GameResOrg.Glue;
using GameResOrg.Helpers;
using GameResOrg.Logic;
using GameResOrg.Logic.Team;
using GameResOrg.Models;
using JetBrains.Annotations;

namespace GameResOrg
{
	public class ThreadMember
	{
		public ThreadMember(SlimUser u)
		{
			User = u;
		}

		public SlimUser User { get; set; }

		public bool Included { get; set; } = true;
	}


	public sealed class AddThreadController : PageController
	{
		private IGroupThreadBuilder _builder;

		public AddThreadController(IGroupThreadBuilder builder)
		{
			_builder = builder;
		}

		private Group _group;

		public Group Group
		{
			get { return _group; }
			set
			{
				_group = value;
				_builder.SetGroup(_group);
				OnPropertyChanged("Group");
			}
		}

		public string Name { get; set; }

		public IUserLinker<SlimUser> UserLinker => _builder.UserLinker;

		public List<ThreadMember> GetIncludableMembers()
		{
			var ul = UserLinker;
			return ul.GetLinkedUsers().SelectToList(u => new ThreadMember(u));
		}

		public int Validate(List<ThreadMember> members)
		{
			if (Name.IsNullOrWhitespace())
				return 1;

			return members?.Count(m => m.Included) >= 2 ? 0 : 2;
		}

		public Task<bool> Save([NotNull] List<ThreadMember> members)
		{
			_builder.SetName(Name);
			var ul = UserLinker;
			foreach (var m in members.Where(m => !m.Included))
			{
				ul.UnlinkItem(m.User);
			}

			return Bouncer.StartSingleTask(() => _builder.Save());
		}
	}
}
