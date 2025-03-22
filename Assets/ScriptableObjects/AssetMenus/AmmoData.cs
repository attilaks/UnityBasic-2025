using System;
using UnityEngine;

namespace ScriptableObjects.AssetMenus
{
	[Serializable]
	[CreateAssetMenu(menuName = "ScriptableObjects/AmmoData", fileName = "NewAmmoData")]
	public class AmmoData :ScriptableObject
	{
		[SerializeField] private GameObject decalPrefab;
		[SerializeField] private float standardDamage = 10.0f;
		
		public GameObject DecalPrefab => decalPrefab;
		public float StandardDamage => standardDamage;
	}
}