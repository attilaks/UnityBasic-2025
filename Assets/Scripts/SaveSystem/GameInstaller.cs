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
		
		protected override void Awake()
		{
			if (_instance)
			{
				Destroy(gameObject);
				return;
			}
			
			_instance = this;
		
			base.Awake();
		}

		private void RegisterPersistentDependencies(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			builder.RegisterComponentInHierarchy<SaveLoadManager>().DontDestroyOnLoad();
		}

		private void RegisterSceneDependencies(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			// builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterSceneDependencies(builder);
			RegisterPersistentDependencies(builder);
			
			DontDestroyOnLoad(this);
		}
	}
}