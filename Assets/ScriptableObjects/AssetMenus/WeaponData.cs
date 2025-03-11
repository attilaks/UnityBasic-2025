using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable ConvertToConstant.Global

namespace ScriptableObjects.AssetMenus
{
	[CreateAssetMenu(menuName = "ScriptableObjects/WeaponData", fileName = "NewWeaponData")]
	public class WeaponData : ScriptableObject
	{
		[Header("Prefab references")]
		[Tooltip("Prefab of the bullet, rocket, etc.")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private GameObject casingPrefab;
		[SerializeField] private GameObject muzzleFlashPrefab;

		[FormerlySerializedAs("EjectPower")]
		[Header("Settings")] 
		[Tooltip("Casing Ejection Speed")] 
		[SerializeField] private float ejectPower = 150f;
		[FormerlySerializedAs("FireRate")]
		[Tooltip("How fast the weapon can shoot")] 
		[SerializeField] private float fireRate = 0.14f;
		[FormerlySerializedAs("FireRange")]
		[Tooltip("How far the weapon can shoot")] 
		[SerializeField] private float fireRange = 100f;
		[FormerlySerializedAs("BulletForce")]
		[Tooltip("Force with which bullets fly out of weapon")] 
		[SerializeField] private float bulletForce = 150;
		
		public GameObject BulletPrefab => bulletPrefab;
		public GameObject CasingPrefab => casingPrefab;
		public GameObject MuzzleFlashPrefab => muzzleFlashPrefab;

		public float EjectPower => ejectPower;
		public float FireRate => fireRate;
		public float FireRange => fireRange;
		public float BulletForce => bulletForce;
	}
}