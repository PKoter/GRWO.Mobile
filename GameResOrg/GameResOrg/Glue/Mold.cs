using System;
using GameResOrg.Data;
using GameResOrg.Data.Contracts;
using GameResOrg.Data.Repositories;
using GameResOrg.Debug;
using GameResOrg.Helpers.Intermediate;
using GameResOrg.Local;
using GameResOrg.Local.Caches;
using GameResOrg.Local.Contracts;
using GameResOrg.Logic.Configuration;
using GameResOrg.Logic.Configuration.Impl;
using GameResOrg.Logic.Infrastructure;
using GameResOrg.Logic.Infrastructure.Internal;
using GameResOrg.Logic.Objects;
using GameResOrg.Logic.Team;
using GameResOrg.Logic.Team.Impl;
using GameResOrg.Logic.Team.Internal;
using GameResOrg.Logic.Team.Services;
using JetBrains.Annotations;
using SimpleInjector;

namespace GameResOrg.Glue
{
	/// <summary>
	/// Service managing dependency injection
	/// </summary>
	public sealed class Mold : IDependencyMold, IDisposable
	{
		private Container _ioc;
		//private IServiceContainer _pageIoc;

		private static Mold _self;
		private static IDependencyMold _dependencyMold;

		public Mold(bool compile = true)
		{
			_ioc = new Container();
			RegisterCore();
			RegisterPages();
			RegisterData();
			RegisterLogic();
			RegisterLocal();
			
			_self = this;
			_dependencyMold = _self;
			if (compile)
			{
				Build();
			}
		}

		public static Mold Provide {get {return _self;}}

		public static IDependencyMold DependencyMold
		{
			get { return _dependencyMold; }
			set { _dependencyMold = value; }
		}

		[NotNull]
		public T Get<T>() where T : class
		{
			var i = _ioc.GetInstance<T>();
			@Asure.NotNull(i);
			return i;
			//return _ioc_di.Resolve<T>();
		}

		[NotNull]
		public T Get<T>([NotNull] string key) where T : class
		{
			@Asure.NotNull(key);
			var i = _ioc.GetInstance<T>();
			@Asure.NotNull(i);
			return i;
		}

		public object Get(Type dependencyType)
		{
			@Asure.NotNull(dependencyType );
			var i = _ioc.GetInstance(dependencyType);
			@Asure.NotNull(i);
			return i;
			//return _ioc_di.Resolve(dependencyType);
		}

		public T GetPage<T>() where T : class
		{
			return null;

			//return _pageIoc_di.Resolve<T>();
		}

		public Mold Register<T1, T2>(bool singleton = true) where T2 : T1
		{
			return this;
		}

		public void Build()
		{
			_ioc.Verify(VerificationOption.VerifyAndDiagnose);
			//_pageIoc.Compile();
			//Caller.AppProxy = Get<IAppProxy>();
		}

		private void RegisterCore()
		{
			_ioc.RegisterInstance<IDependencyMold>(this);
			//_ioc.Register<IAppProxy, AppProxy>(new PerContainerLifetime());
		}

		private void RegisterPages()
		{
			_ioc.Register<LoginPageController>();
			_ioc.Register<ProjectSelectController>();
			_ioc.Register<ManageUsersController>();
			_ioc.Register<GroupChatController>();
			_ioc.Register<MainPageController>();
			_ioc.Register<AddThreadController>();
		}

		private void RegisterLogic()
		{
			_ioc.Register<IUserConfig, UserConfig>(Lifestyle.Singleton);
			_ioc.Register<IDbConfig, DbConfig>(Lifestyle.Singleton);
			//	.Register<ISyncBuilder, InsBuilder>(new PerContainerLifetime())
			//	.Register<InternalsService>(new PerContainerLifetime())
			//_ioc.Register<RelatedDataAggregator>(Lifestyle.Singleton);
				;
			_ioc.Register<IUserService, UserService>();
			_ioc.Register<IProjectService, ProjectService>();
			_ioc.Register<IProjectJoiningService, ProjectJoiningService>();
			_ioc.Register<IGroupService, GroupService>();
			_ioc.Register<IChattingService, ChattingService>();
			_ioc.Register<IAssetService, MockAssetService>();
			//	.Register<IAssetCategoryService, AssetCategoryService>()
			//	.Register<IStorageService, StorageService>()
			//	.Register<ILocalChangesService, LocalChangesService>()
			//	.Register<IStorageTransferService, StorageTransferService>()
			_ioc.Register<ICollaboratorService, CollaboratorService>();
			_ioc.Register<IPrivilegeService, PrivilegesService>();
			_ioc.Register<RelatedDataAggregator>(Lifestyle.Singleton);
			_ioc.Register<IRelatedDataService, RelatedDataService>();
			//	.Register<IObjectService, ObjectService>()
			_ioc.Register<ISyncBuilder, InsBuilder>(Lifestyle.Singleton);
			_ioc.Register<InternalsService>();
			_ioc.Register<IGroupThreadBuilder, GroupThreadBuilder>();
			//_ioc.Register<IAssetBuilder, AssetBuilder>()
			//	.Register<IStorageBuilder, LocalStorageBuilder>()
			//	.Register<IGroupBuilder, GroupBuilder>()
			//	;
		}

		private void RegisterLocal()
		{
			_ioc.Register<ILocalContext, LocalContext>(Lifestyle.Singleton);
			_ioc.Register<ISecureConfigStore, SecureConfigStore>(Lifestyle.Singleton);
			_ioc.Register<IRuntimeConfigStore, RuntimeConfigStore>(Lifestyle.Singleton);
			//  .Register<IConfigSource, ConfigSource>(Lifestyle.Singleton);
			_ioc.Register<UserCache>(Lifestyle.Singleton);
			_ioc.Register<ISettingsProvider, Settings>(Lifestyle.Singleton);
			_ioc.Register<AssetCategoryCache>(Lifestyle.Singleton);
			//	.Register<AssetCache>(new PerContainerLifetime())
			//_ioc.Register<UserGroupsCache>(Lifestyle.Singleton);
			_ioc.Register<UnreadInsCache>(Lifestyle.Singleton);
			//	.Register<TransferBook>(new PerContainerLifetime())
			//  .Register<IFileStorageContext, FileStorageContext>(new PerContainerLifetime())
			//	.Register<IChangeTracker, ChangeTracker>(new PerContainerLifetime())
				;
		}

		private void RegisterData()
		{
			_ioc.Register<IContextFactory, ContextFactory>(Lifestyle.Singleton);
			_ioc.Register<IProjectRepository, ProjectRepository>(Lifestyle.Singleton);
			_ioc.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
			_ioc.Register<ICollaboratorRepository, CollaboratorRepository>(Lifestyle.Singleton);
			_ioc.Register<IGroupRepository, GroupRepository>(Lifestyle.Singleton);
			_ioc.Register<IChattingRepository, ChattingRepository>(Lifestyle.Singleton);
				//.Register<IAssetRepository, AssetRepository>()
				//.Register<IAssetCategoryRepository, AssetCategoryRepository>()
				//.Register<IWorkstationRepository, WorkstationRepository>()
			_ioc.Register<InternalsRepository>(Lifestyle.Singleton);
		}

		public void Dispose()
		{
			//Get<ILocalContext>().Dispose();
			//Get<IConfigSource>().Dispose();
			var ioc = _ioc;
			_ioc = null;
			ioc?.Dispose();

			//ioc = _pageIoc;
			//_pageIoc = null;
			//ioc?.Dispose();
		}
	}
}