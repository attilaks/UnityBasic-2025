using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveSystem
{
	[Serializable]
	public struct SaveData
	{
		public SerializableVector3 playerPosition;
		public SerializableVector3 playerRotation;
		public SerializableVector3 cameraRotation;
		public byte currentFireArmId;
		// public Dictionary<byte, byte> FireArmAmmoCountDict;
		//todo
		public byte[] fireArmAmmoKeys;
		public byte[] fireArmAmmoValues;
    
		// Методы для конвертации в Dictionary и обратно
		public Dictionary<byte, byte> GetAmmoDictionary()
		{
			var dict = new Dictionary<byte, byte>();
			for (var i = 0; i < fireArmAmmoKeys.Length; i++)
			{
				dict[fireArmAmmoKeys[i]] = fireArmAmmoValues[i];
			}
			return dict;
		}
    
		public void SetAmmoDictionary(Dictionary<byte, byte> dict)
		{
			fireArmAmmoKeys = dict.Keys.ToArray();
			fireArmAmmoValues = dict.Values.ToArray();
		}
	}
}