using System.Collections.Generic;

namespace Tools.Weapons
{
	public interface IWeaponReader
	{
		byte CurrentFireArmId { get; }
		Dictionary<byte, byte> GetFireArmAmmoDict();
	}
}