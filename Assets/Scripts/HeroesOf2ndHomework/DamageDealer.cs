using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GlobalConstants;

namespace HeroesOf2ndHomework
{
    public class DamageDealer : MonoBehaviour
    {
        public event Action<float> DamageIsBeingDoneEvent = delegate { };
        
        [SerializeField] private float damageMultiplier = 1f;
        
        private InputAction _dealDamage;

        private const string DamageButtonName = InputConstants.SpaceKey;
        private const string DealDamageActionName = "DealDamage";
        private const float Damage = 10f;

        private void Awake()
        {
            _dealDamage = new InputAction(DealDamageActionName, InputActionType.Button);
            _dealDamage.AddBinding($"{InputConstants.KeyBoard}/{DamageButtonName}");
            
            _dealDamage.performed += OnDamagePerformed;
            
            _dealDamage.Enable();
        }

        private void Start()
        {
            Debug.LogError("I will do some damage now!");
            Debug.LogWarning($"Press {DamageButtonName} to deal damage!");
        }
    
        private void DealDamage()
        {
            DamageIsBeingDoneEvent.Invoke(Damage * damageMultiplier);
        }
        
        private void OnDamagePerformed(InputAction.CallbackContext context)
        {
            DealDamage();
        }
    
        private void OnDestroy()
        {
            _dealDamage.performed -= OnDamagePerformed;
            _dealDamage.Disable();
        }
    }
}