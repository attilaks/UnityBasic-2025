using SaveSystem.Interfaces;
using VContainer;
using VContainer.Unity;

namespace SaveSystem
{
	public class ProjectLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
			builder.Register<ISaveDataApplier, JsonSaveService>(Lifetime.Singleton);
		}
	}
}