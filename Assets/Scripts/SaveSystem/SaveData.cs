using System;
using System.Collections.Generic;

namespace SaveSystem
{
	[Serializable]
	public struct SaveData
	{
		public SerializableVector3 playerPosition;
		public SerializableVector3 playerRotation;
		public SerializableVector3 cameraRotation;
		public IReadOnlyList<SerializableVector3> EnemyPositions;
		public byte currentAmmoCount;
		//todo
	}
}