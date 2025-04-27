using Tools.Managers;
using Tools.Managers.Interfaces;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class GameInstaller : LifetimeScope
	{
		protected override void Awake()
		{
			DontDestroyOnLoad(this);
			base.Awake();
		}
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			builder.Register<IPlayerTransformReader, FirstPersonMovementManager>(Lifetime.Scoped);
			builder.Register<ICameraReader, FirstPersonCamera>(Lifetime.Scoped);

			builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}
	}
}