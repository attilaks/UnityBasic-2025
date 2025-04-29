using SaveSystem;
using UnityEngine;

namespace Tools.Managers.Interfaces
{
	public interface IPlayerTransformReader
	{
		SerializableVector3 PlayerPosition { get; }
		SerializableVector3 PlayerRotation { get; }
		
		void Set(Vector3 position, Vector3 rotation);
	}
}