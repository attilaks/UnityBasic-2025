using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GlobalConstants;

namespace HeroesOf2ndHomework
{
    public class DamageDealer : MonoBehaviour
    {
        public event Action<float> DamageIsBeingDoneEvent = delegate { };
        public event Action<float> AutoDamageIsBeingDoneEvent = delegate { };
        
        [SerializeField] private float damageMultiplier = 1f;
        
        private InputAction _dealDamage;
        private InputAction _dealAutoDamage;

        private const string DamageButtonName = InputConstants.Space;
        private const string DealDamageActionName = "DealDamage";

        private const string AutoDamageButtonName = InputConstants.A;
        private const string DealAutoDamageActionName = "DealAutoDamage";
        
        private const float Damage = 10f;

        private void Awake()
        {
            _dealDamage = new InputAction(DealDamageActionName, InputActionType.Button);
            _dealDamage.AddBinding($"{InputConstants.KeyBoard}/{DamageButtonName}");
            _dealAutoDamage = new InputAction(DealAutoDamageActionName, InputActionType.Button);
            _dealAutoDamage.AddBinding($"{InputConstants.KeyBoard}/{AutoDamageButtonName}");
            
            _dealDamage.performed += OnDamagePerformed;
            _dealAutoDamage.performed += OnAutoDamagePerformed;
            
            _dealDamage.Enable();
            _dealAutoDamage.Enable();
        }

        private void Start()
        {
            Debug.LogError("I will do some damage now!");
            Debug.LogWarning($"Press {DamageButtonName} to deal damage!");
            Debug.LogWarning($"Press {AutoDamageButtonName} to deal auto damage!");
        }
        
        private void OnDestroy()
        {
            _dealDamage.performed -= OnDamagePerformed;
            _dealAutoDamage.performed -= OnAutoDamagePerformed;
            
            _dealDamage.Disable();
            _dealAutoDamage.Disable();
        }
    
        private void DealDamage()
        {
            DamageIsBeingDoneEvent.Invoke(Damage * damageMultiplier);
        }

        private void DealAutoDamage()
        {
            AutoDamageIsBeingDoneEvent.Invoke(Damage * damageMultiplier);
        }
        
        private void OnDamagePerformed(InputAction.CallbackContext context)
        {
            DealDamage();
        }
        
        private void OnAutoDamagePerformed(InputAction.CallbackContext context)
        {
            DealAutoDamage();
        }
    }
}