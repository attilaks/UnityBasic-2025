using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HeroesOf2ndHomework
{
    public class Victim : MonoBehaviour
    {
        private const float MaxHealth = 100f;
        private const string ResurrectButton = "f";
        private const string ResurrectActionName = InputConstants.FKey;
    
        private float _health = 100f;
        private InputAction _resurrect;

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
            _health = MaxHealth;
            Debug.Log("I'm alive! Again! Thank you, God!");
        }

        public void ApplyDamage(float damage)
        {
            if (_health <= 0f)
            {
                Debug.LogWarning($"Resurrect me by pressing '{ResurrectButton}'");
                return;
            }
        
            _health -= damage;
            Debug.LogError($"Aaargh... I'm injured! My Health is {_health}");

            if (_health <= 0f)
            {
                Die();
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
