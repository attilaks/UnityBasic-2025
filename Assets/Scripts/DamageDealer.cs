using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamageDealer : MonoBehaviour
{
    private InputAction _dealDamage;
    private const string DamageButtonName = "space";
    private const float Damage = 10f;
    
    public event Action<float> DamageIsBeingDoneEvent = delegate { };
    
    [SerializeField] private float damageMultiplier = 1f;

    private void Awake()
    {
        // Создание новой Input Action
        _dealDamage = new InputAction("DealDamage", InputActionType.Button);
        _dealDamage.AddBinding($"<Keyboard>/{DamageButtonName}");
        
        // Подписка на событие
        _dealDamage.performed += _ => DealDamage();
        
        // Активация
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
    
    void OnDestroy()
    {
        _dealDamage.Disable();
    }
}
