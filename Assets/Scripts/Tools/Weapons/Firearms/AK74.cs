using GlobalConstants;
using ScriptableObjects.AssetMenus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons.Firearms
{
    // ReSharper disable once InconsistentNaming
    public class AK74 : MonoBehaviour
    {
        [SerializeField] private Camera firstPersonCamera;
        [SerializeField] private WeaponData weaponData;
        
        [Header("Location references")]
        [SerializeField] private Transform casingExitLocation;
        [SerializeField] private Transform firePoint;
        
        private float _nextFireTime;
        
        private const float DestroyTimer = 2f;
        
        private readonly InputAction _shoot = new("Shoot", InputActionType.Button, 
            $"{InputConstants.Mouse}/{InputConstants.LeftButton}");
        
        private void OnEnable()
        {
            _shoot.Enable();
        }

        private void Update()
        {
            if (_shoot.IsPressed())
            {
                if (Time.time >= _nextFireTime)
                {
                    _nextFireTime = Time.time + weaponData.FireRate;
                    Shoot();
                    CasingRelease();
                }
            }
        }

        private void OnDisable()
        {
            _shoot.Disable();
        }
        
        private void Shoot()
        {
            if (weaponData.MuzzleFlashPrefab)
            {
                var tempFlash = Instantiate(weaponData.MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(tempFlash, DestroyTimer);
            }

            if (weaponData.BulletPrefab)
            {
                SetFirePointDirection();
			
                var bullet = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
                var rb = bullet.GetComponent<Rigidbody>();
                var direction = Quaternion.Euler(
                    Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread),
                    Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread),
                    0) * firePoint.forward;
                rb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);
			
                Destroy(bullet, 2f);
            }
        }
        
        private void CasingRelease()
        {
            if (!casingExitLocation || !weaponData.CasingPrefab)
            {
                return;
            }

            var tempCasing = Instantiate(weaponData.CasingPrefab, casingExitLocation.position, casingExitLocation.rotation);
            var tempCasingRigidBody = tempCasing.GetComponent<Rigidbody>();
            tempCasingRigidBody.AddExplosionForce(Random.Range(weaponData.EjectPower * 0.7f, weaponData.EjectPower), 
                casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f, 1f);
            tempCasingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
			
            Destroy(tempCasing, DestroyTimer);
        }
        
        private void SetFirePointDirection()
        {
            Vector3 screenCenter = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
            Ray ray = firstPersonCamera.ScreenPointToRay(screenCenter);

            // Если луч пересекает какой-либо объект
            if (Physics.Raycast(ray, out var hit, weaponData.FireRange))
            {
                // Направляем объект на точку пересечения
                firePoint.transform.LookAt(hit.point);
            }
            else
            {
                // Если луч не пересекает объект, направляем объект на точку вдали
                Vector3 targetPosition = ray.GetPoint(weaponData.FireRange);
                firePoint.transform.LookAt(targetPosition);
            }
        }
    }
}
