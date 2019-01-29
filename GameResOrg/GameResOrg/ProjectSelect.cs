using System.Collections.Generic;
using System.Threading.Tasks;
using GameResOrg.Glue;
using GameResOrg.Intermediate;
using GameResOrg.Logic.Configuration;
using GameResOrg.Models;
using JetBrains.Annotations;

namespace GameResOrg
{
	[UsedImplicitly]
    public sealed class ProjectSelectController : PageController
	{
		private IProjectService _projectService;
		private IProjectJoiningService _joiningService;
		

		public ProjectSelectController(IProjectService projectService, IProjectJoiningService joiningService)
		{
			_projectService = projectService;
			_joiningService = joiningService;
		}

		public ProjectModel SelectedProject { get; set; }

		public Task<IList<ProjectModel>> GetProjects() => Bouncer.StartSingleTask(() => _projectService.LoadProjects());

		public Task<FailInfo> SelectProject()
		{
			return Bouncer.StartSingleTask(() => _joiningService.JoinProject(SelectedProject)).AwaitCall();
		}
	}
}
