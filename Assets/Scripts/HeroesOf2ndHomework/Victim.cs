using GlobalConstants;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HeroesOf2ndHomework
{
    public class Victim : MonoBehaviour
    {
        private const float MaxHealth = 100f;
        private const string ResurrectButton = InputConstants.F;
        private const string ResurrectActionName = "Resurrect";
        
        private readonly HealthManager _healthManager = new (MaxHealth);
        private InputAction _resurrect;

        private void Awake()
        {
            _resurrect = new InputAction(ResurrectActionName, InputActionType.Button);
            _resurrect.AddBinding($"{InputConstants.KeyBoard}/{ResurrectButton}");
            _resurrect.performed += OnResurrectPerformed;
            _resurrect.Enable();
            
            _healthManager.DeathHasComeEvent += Die;
        }

        private void OnDestroy()
        {
            _resurrect.performed -= OnResurrectPerformed;
            _resurrect.Disable();
            
            _healthManager.DeathHasComeEvent -= Die;
        }
        
        private void OnResurrectPerformed(InputAction.CallbackContext context)
        {
            Resurrect();
        }

        private void Resurrect()
        {
            gameObject.SetActive(true);
            _healthManager.Health = MaxHealth;
            Debug.Log("I'm alive! Again! Thank you, God!");
        }

        public void ApplyDamage(float damage)
        {
            if (_healthManager.IsDead)
            {
                Debug.LogWarning($"Resurrect me by pressing '{ResurrectButton}'");
                return;
            }
        
            _healthManager.Health -= damage;
        }
        
        public void ApplyAutoDamage(float damage)
        {
            
            if (_healthManager.IsDead)
            {
                Debug.LogWarning($"Resurrect me by pressing '{ResurrectButton}'");
                return;
            }
            
            while (!_healthManager.IsDead)
            {
                _healthManager.Health -= damage;
            }
        }

        private void Die()
        {
            Debug.LogError("I'm dying...");
            gameObject.SetActive(false);
            Debug.LogWarning($"Press \"{ResurrectButton}\" to pay respect... And to resurrect me!");
        }
    }
}
