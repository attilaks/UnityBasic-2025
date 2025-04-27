using Tools.Managers;
using Tools.Managers.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class GameInstaller : LifetimeScope
	{
		[SerializeField] private SaveLoadManager saveLoadManagerPefab;
		
		protected override void Awake()
		{
			DontDestroyOnLoad(this);
			base.Awake();
			// DontDestroyOnLoad(this);
		}
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			builder.Register<IPlayerTransformReader, FirstPersonMovementManager>(Lifetime.Scoped);
			builder.Register<ICameraReader, FirstPersonCamera>(Lifetime.Scoped);
			
			var manager = Instantiate(saveLoadManagerPefab);
			DontDestroyOnLoad(manager.gameObject);

			// Регистрируем экземпляр в контейнере
			builder.RegisterInstance(manager);

			// builder.RegisterComponentOnNewGameObject(typeof(SaveLoadManager), Lifetime.Singleton, "SaveLoadManager" )
			// 	.DontDestroyOnLoad();
		}
	}
}