using Tools.Managers;
using Tools.Managers.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class GameInstaller : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			
			builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}
	}
}