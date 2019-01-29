using System;
using GameResOrg.Data;
using GameResOrg.Data.Contracts;
using GameResOrg.Data.Repositories;
using GameResOrg.Debug;
using GameResOrg.Helpers.Intermediate;
using GameResOrg.Local;
using GameResOrg.Logic.Configuration;
using GameResOrg.Logic.Configuration.Impl;
using JetBrains.Annotations;
using LightInject;

namespace GameResOrg.Glue
{
	/// <summary>
	/// Service managing dependency injection
	/// </summary>
	public sealed class Mold : IDependencyMold, IDisposable
	{
		private IServiceContainer _ioc;
		//private IServiceContainer _pageIoc;

		private static Mold _self;
		private static IDependencyMold _dependencyMold;

		public Mold(bool compile = true)
		{
			_ioc = new ServiceContainer(new ContainerOptions { EnablePropertyInjection = false });
			//_pageIoc = new ServiceContainer(new ContainerOptions { EnablePropertyInjection = false });
			RegisterCore();
			RegisterPages();
			RegisterData();
			RegisterLogic();
			RegisterLocal();
			RegisterComponents();
			
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
		public T Get<T>()
		{
			var i = _ioc.GetInstance<T>();
			@Asure.NotNull(i);
			return i;
			//return _ioc_di.Resolve<T>();
		}

		[NotNull]
		public T Get<T>([NotNull] string key)
		{
			@Asure.NotNull(key);
			var i = _ioc.GetInstance<T>(key);
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
			_ioc.Register<T1, T2>(singleton 
								  ? (ILifetime)new PerContainerLifetime() 
								  : new PerRequestLifeTime());
			return this;
		}

		public void Build()
		{
			_ioc.Compile();
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
			_ioc.Register<LoginPageController>()
				;
		}

		private void RegisterComponents()
		{
			//_ioc.Register<IElementLinker, UserLinker>(nameof(UserLinker));
			
		}

		private void RegisterLogic()
		{
			_ioc.Register<IUserConfig, UserConfig>(new PerContainerLifetime())
				.Register<IDbConfig, DbConfig>(new PerContainerLifetime())
			//	.Register<ISyncBuilder, InsBuilder>(new PerContainerLifetime())
			//	.Register<InternalsService>(new PerContainerLifetime())
			//	.Register<RelatedDataAggregator>(new PerContainerLifetime())
				;
			_ioc.Register<IUserService, UserService>()
			//	.Register<IProjectService, ProjectService>()
			//	.Register<IProjectJoiningService, ProjectJoiningService>()
			//	.Register<IGroupService, GroupService>()
			//	.Register<IAssetService, AssetService>()
			//	.Register<IAssetCategoryService, AssetCategoryService>()
			//	.Register<IStorageService, StorageService>()
			//	.Register<ILocalChangesService, LocalChangesService>()
			//	.Register<IStorageTransferService, StorageTransferService>()
			//	.Register<ICollaboratorService, CollaboratorService>()
			//	.Register<IRelatedDataService, RelatedDataService>()
			//	.Register<IObjectService, ObjectService>()
				;
			//_ioc.Register<IAssetBuilder, AssetBuilder>()
			//	.Register<IStorageBuilder, LocalStorageBuilder>()
			//	.Register<IGroupBuilder, GroupBuilder>()
			//	;
		}

		private void RegisterLocal()
		{
			_ioc//.Register<ILocalContext, LocalContext>(new PerContainerLifetime())
				.Register<ISecureConfigStore, SecureConfigStore>(new PerContainerLifetime())
			//	.Register<IRuntimeConfigStore, RuntimeConfigStore>(new PerContainerLifetime())
			//	.Register<IConfigSource, ConfigSource>(new PerContainerLifetime())
				.Register<UserCache>(new PerContainerLifetime())
			//	.Register<AssetCategoryCache>(new PerContainerLifetime())
			//	.Register<AssetCache>(new PerContainerLifetime())
			//	.Register<UserGroupsCache>(new PerContainerLifetime())
			//	.Register<UnreadInsCache>(new PerContainerLifetime())
			//	.Register<TransferBook>(new PerContainerLifetime())
			//	//.Register<IFileStorageContext, FileStorageContext>(new PerContainerLifetime())
			//	.Register<IChangeTracker, ChangeTracker>(new PerContainerLifetime())
				;
		}

		private void RegisterData()
		{
			_ioc.Register<IContextFactory, ContextFactory>(new PerContainerLifetime())
				.Register<IProjectRepository, ProjectRepository>()
				.Register<IUserRepository, UserRepository>()
				//.Register<ICollaboratorRepository, CollaboratorRepository>()
				//.Register<IGroupRepository, GroupRepository>()
				//.Register<IAssetRepository, AssetRepository>()
				//.Register<IAssetCategoryRepository, AssetCategoryRepository>()
				//.Register<IWorkstationRepository, WorkstationRepository>()
				//.Register<InternalsRepository>()
				;
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