using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "ScriptableObjects/Weapon", fileName = "NewWeapon")]
	public class Weapon : ScriptableObject
	{
		[Header("Prefab references")]
		[Tooltip("Prefab of the bullet, rocket, etc.")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private GameObject casingPrefab;
		[SerializeField] private GameObject muzzleFlashPrefab;
		
		[Header("Location references")]
		[SerializeField] private Transform casingExitLocation;
		[SerializeField] private Transform firePoint;
		
		[Header("Settings")]
		[Tooltip("Casing Ejection Speed")] 
		[SerializeField] private float ejectPower = 150f;
		[Tooltip("How fast the weapon can shoot")]
		[SerializeField] private float fireRate = 0.14f;
		[Tooltip("How far the weapon can shoot")] 
		[SerializeField] private float fireRange = 100f;
		[Tooltip("Force with which bullets fly out of weapon")]
		[SerializeField] private float bulletForce = 150;
	}
}