using System;
using System.Collections.Generic;

namespace SaveSystem
{
	[Serializable]
	public sealed class SaveData
	{
		public SerializableVector3 playerPosition;
		public SerializableVector3 playerRotation;
		public SerializableVector3 cameraRotation;
		public List<SerializableVector3> enemyPositions;
		public byte currentAmmoCount;
		//todo
	}
}