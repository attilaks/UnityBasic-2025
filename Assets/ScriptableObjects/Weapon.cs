using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "ScriptableObjects/Weapon", fileName = "NewWeapon")]
	public class Weapon : ScriptableObject
	{
		[Tooltip("Name of the weapon")]
		[SerializeField] private string weaponName;
		[SerializeField] private int damage;
		[Tooltip("How fast the weapon can shoot")]
		[SerializeField] private float fireRate;
		[Tooltip("How far the weapon can shoot")] 
		[SerializeField] private float fireRange;
		[Tooltip("Force with which bullets fly out of weapon")]
		[SerializeField] private float bulletForce;
		[SerializeField] private GameObject projectilePrefab;
	}
}