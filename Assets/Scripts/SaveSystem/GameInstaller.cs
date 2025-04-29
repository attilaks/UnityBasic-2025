using Tools.Managers;
using Tools.Managers.Interfaces;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class GameInstaller : LifetimeScope
	{
		private static GameInstaller _instance;
		
		private IContainerBuilder _builder;
		
		protected override void Awake()
		{
			if (_instance)
			{
				Destroy(gameObject);
				return;
			}
			
			_instance = this;

			SceneManager.sceneLoaded += ReRegisterComponentsInHierarchy;
		
			base.Awake();
		}

		protected override void OnDestroy()
		{
			SceneManager.sceneLoaded -= ReRegisterComponentsInHierarchy;
			base.OnDestroy();
		}
		
		private void RegisterPersistentDependencies(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			// builder.RegisterComponentInHierarchy<SaveLoadManager>().DontDestroyOnLoad();
		}

		private void RegisterSceneDependencies(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}

		private void ReRegisterComponentsInHierarchy(Scene scene, LoadSceneMode mode)
		{
			if (Container != null && gameObject.scene.name == nameof(DontDestroyOnLoad))
			{
				// var builder = new ContainerBuilder();
				RegisterSceneDependencies(_builder);
				// Container.Inject(_builder);
				// builder.Build().Update(Container);
			}
		}
		
		// private void ReRegisterComponentsInHierarchy(Scene arg0, LoadSceneMode arg1)
		// {
		// 	builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
		// 	builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
		// }

		protected override void Configure(IContainerBuilder builder)
		{
			_builder = builder;
			// builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			
			RegisterSceneDependencies(builder);
			// builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			// builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			
			RegisterPersistentDependencies(builder);
			// builder.RegisterComponentInHierarchy<SaveLoadManager>().DontDestroyOnLoad();
			
			DontDestroyOnLoad(this);
		}
	}
}