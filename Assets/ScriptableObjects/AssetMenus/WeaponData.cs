using UnityEngine;

// ReSharper disable ConvertToConstant.Global

namespace ScriptableObjects.AssetMenus
{
	[CreateAssetMenu(menuName = "ScriptableObjects/WeaponData", fileName = "NewWeaponData")]
	public class WeaponData : ScriptableObject
	{
		[Header("Prefab references")]
		[Tooltip("Prefab of the bullet, rocket, etc.")]
		[SerializeField] private Rigidbody bulletPrefab;
		[SerializeField] private Rigidbody casingPrefab;
		[SerializeField] private GameObject muzzleFlashPrefab;
		
		[Header("Settings")] 
		[Tooltip("Casing Ejection Speed")] 
		[SerializeField] private float ejectPower = 150f;
		[Tooltip("How fast the weapon can shoot")] 
		[SerializeField] private float fireRate = 0.14f;
		[Tooltip("How far the weapon can shoot")] 
		[SerializeField] private float fireRange = 100f;
		[Tooltip("Force with which bullets fly out of weapon")] 
		[SerializeField] private float bulletForce = 150;
		[SerializeField] private float bulletSpread = 5f;
		[SerializeField] private byte clipCapacity = 7;
		[SerializeField] private bool isAutomatic;
		
		[Header("Audio")]
		[SerializeField] private AudioClip shootSound;
		[SerializeField] private AudioClip reloadSound;
		[SerializeField] private AudioClip emptyClipSound;
		
		[Header("Sprites")]
		[SerializeField] private Sprite ammoUiSprite;
		
		public Rigidbody BulletPrefab => bulletPrefab;
		public Rigidbody CasingPrefab => casingPrefab;
		public GameObject MuzzleFlashPrefab => muzzleFlashPrefab;

		public float EjectPower => ejectPower;
		public float FireRate => fireRate;
		public float FireRange => fireRange;
		public float BulletForce => bulletForce;
		public float BulletSpread => bulletSpread;
		public byte ClipCapacity => clipCapacity;
		public bool IsAutomatic => isAutomatic;

		public AudioClip ShootSound => shootSound;
		public AudioClip ReloadSound => reloadSound;
		public AudioClip EmptyClipSound => emptyClipSound;
		
		public Sprite AmmoUiSprite => ammoUiSprite;
	}
}