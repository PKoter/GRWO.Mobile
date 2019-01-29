using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameResOrg.Data.Project;
using GameResOrg.Debug;
using GameResOrg.Glue;
using GameResOrg.Logic.Infrastructure;
using GameResOrg.Logic.Team;
using GameResOrg.Logic.Team.Models;
using JetBrains.Annotations;
using Task = System.Threading.Tasks.Task;

namespace GameResOrg
{
	[UsedImplicitly]
    public sealed class GroupChatController : PageController
	{
		private IGroupService          _groupService;
		private IChattingService       _chattingService;
		private IRelatedDataService    _dataService;
		private Group                  _contextGroup;
		private List<PrivateConverser> _conversers;

		public GroupChatController(IChattingService chattingService, IRelatedDataService dataService, 
								   IGroupService groupService)
		{
			_chattingService = chattingService;
			_dataService     = dataService;
			_groupService    = groupService;
		}

		public void SetContext([NotNull] Group group)
		{
			if (_contextGroup != group)
			{
				Threads    = null;
				_conversers = null;
				Messages   = null;
			}
			_contextGroup = group;
		}

		public List<PrivateConversation> Threads { get; private set; }

		//public List<PrivateConverser> Conversers => _conversers;

		public List<MessageModel> Messages { get; private set; }

		public string MessageText { get; set; }
		public PrivateConversation SelectedThread { get; set; }
		public Group SelectedGroup { get; set; }

		[NotNull]
		public Task GetConversations()
		{
			return Bouncer.StartSingleTask(() =>
				{
					Threads = _chattingService.GetGroupThreads(_contextGroup.Id);
					//OnPropertyChanged("Threads");
				});
		}

		[NotNull][Obsolete]
		public Task GetConversersForThread([NotNull] PrivateConversation thread)
		{
			return Bouncer.StartSingleTask(() => _conversers = _chattingService.GetGroupThreadConversers(thread));
		}

		[NotNull]
		public Task GetMessagesForThread([NotNull] PrivateConversation thread)
		{
			return Bouncer.StartSingleTask(() =>
				{
					Messages = _chattingService.GetThreadMessages(thread);
					OnPropertyChanged("Messages");
				});
		}

		public Task SendMessage()
		{
			@Asure.NotNull(SelectedThread, MessageText, Messages);
			return Bouncer.StartSingleTask(() =>
				{
					var newMessage = _chattingService.SendThreadMessage(SelectedThread, MessageText);
					MessageText = "";
					Messages.Add(newMessage);
					Messages = Messages.ToList(Messages.Count);

					OnPropertyChanged("MessageText");
					OnPropertyChanged("Messages");
				});
		}

		[NotNull]
		public Task ThreadSelected([NotNull] PrivateConversation thread)
		{
			if (thread.NewMessages > 0)
			{
				var secBouncer = new ProcessBouncer(this);
				secBouncer.StartSingleTask(() => _dataService.MarkRead(thread));
			}
			return GetMessagesForThread(thread);
		}

		public Task<IList<Group>> GetGroups()
		{
			return Bouncer.StartSingleTask(() => _groupService.GetUserGroups());
		}
	}
}
