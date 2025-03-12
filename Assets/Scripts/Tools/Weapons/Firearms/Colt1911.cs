using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons.Firearms
{
	[RequireComponent(typeof(Animator))]
	public sealed class Colt1911 : FireArm
	{
		private Animator _animator;

		private const string AnimatorFire = "Fire";
		private static readonly int Fire = Animator.StringToHash(AnimatorFire);

		#region Monobehaviour methods

		private new void Awake()
		{
			base.Awake();
			_animator = gameObject.GetComponent<Animator>();
			
			var animatorParameterNames = _animator.parameters.Select(x => x.name).ToArray();
			if (!animatorParameterNames.Contains(AnimatorFire))
			{
				throw new Exception($"Animator parameter {AnimatorFire} not found");
			}
		}

		private new void OnEnable()
		{
			ShootAction.performed += OnShootActionPerformed;
			base.OnEnable();
		}

		private new void OnDisable()
		{
			ShootAction.performed -= OnShootActionPerformed;
			base.OnDisable();
		}

		#endregion
		
		private void OnShootActionPerformed(InputAction.CallbackContext context)
		{
			Shoot();
		}

		private new void Shoot()
		{
			if (Time.time >= NextFireTime && CurrentAmmoCount > 0)
			{
				NextFireTime = Time.time + weaponData.FireRate;
				_animator.SetTrigger(Fire);
			}
		}
	}
}