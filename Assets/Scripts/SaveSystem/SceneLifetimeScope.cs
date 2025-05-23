﻿using Tools.Managers;
using Tools.Managers.Interfaces;
using Tools.Weapons;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class SceneLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<FirstPersonMovementManager>().As<IPlayerTransformReader>();
			builder.RegisterComponentInHierarchy<FirstPersonCamera>().As<ICameraReader>();
			builder.RegisterComponentInHierarchy<WeaponController>().As<IWeaponReader>();
			
			builder.RegisterComponentInHierarchy<SaveLoadManager>();
		}
	}
}