using Tools.Managers;
using Tools.Managers.Interfaces;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class ProjectLifetimeScope : LifetimeScope
	{
		private void RegisterPersistentDependencies(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			
			// builder.RegisterComponentInHierarchy<SaveLoadManager>().DontDestroyOnLoad();
		}

		private void RegisterSceneDependencies(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			// builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterPersistentDependencies(builder);
		}
	}
}