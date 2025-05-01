using Tools.Managers;
using Tools.Managers.Interfaces;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class SceneInstaller : LifetimeScope
	{
		// protected override void Awake()
		// {
		// 	var gameInstaller = FindObjectOfType<GameInstaller>();
		// 	parentReference.Object = gameInstaller;
		// 	base.Awake();
		// }
		
		protected override void Configure(IContainerBuilder builder)
		{
			var parent = Parent;
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
		}
	}
}