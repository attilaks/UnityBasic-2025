using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HeroesOf2ndHomework
{
    public class Victim : MonoBehaviour
    {
        private const float MaxHealth = 100f;
        private const string ResurrectButton = InputConstants.F;
        private const string ResurrectActionName = "Resurrect";

        private float _health;
        private float Health
        {
            get => _health;
            set
            {
                if (_health > value)
                {
                    Debug.LogError($"Aaargh... I'm injured! My Health is {Health}");
                }
                _health = value;
                if (_health <= 0f)
                {
                    Die();
                }
            }
        }

        private InputAction _resurrect;
        private bool _isDead;

        private void Awake()
        {
            _resurrect = new InputAction(ResurrectActionName, InputActionType.Button);
            _resurrect.AddBinding($"{InputConstants.KeyBoard}/{ResurrectButton}");
            
            _resurrect.performed += OnResurrectPerformed;
            
            _resurrect.Enable();
        }
    
        private void OnDestroy()
        {
            _resurrect.performed -= OnResurrectPerformed;
            _resurrect.Disable();
        }
        
        private void OnResurrectPerformed(InputAction.CallbackContext context)
        {
            Resurrect();
        }

        private void Resurrect()
        {
            gameObject.SetActive(true);
            Health = MaxHealth;
            _isDead = false;
            Debug.Log("I'm alive! Again! Thank you, God!");
        }

        public void ApplyDamage(float damage)
        {
            if (_isDead)
            {
                Debug.LogWarning($"Resurrect me by pressing '{ResurrectButton}'");
                return;
            }
        
            Health -= damage;
        }
        
        public void ApplyAutoDamage(float damage)
        {
            
            if (_isDead)
            {
                Debug.LogWarning($"Resurrect me by pressing '{ResurrectButton}'");
                return;
            }
            
            while (!_isDead)
            {
                Health -= damage;
            }
        }

        private void Die()
        {
            Debug.LogError("I'm dying...");
            gameObject.SetActive(false);
            _isDead = true;
            Debug.LogWarning($"Press \"{ResurrectButton}\" to pay respect... And to resurrect me!");
        }
    }
}
