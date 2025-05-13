using SaveSystem;

namespace Tools.Managers.Interfaces
{
	public interface ICameraReader
	{
		SerializableVector3 CameraRotation { get; }
	}
}