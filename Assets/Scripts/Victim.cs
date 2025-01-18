using UnityEngine;
using UnityEngine.InputSystem;

public class Victim : MonoBehaviour
{
    private const float MaxHealth = 100f;
    private float _health = 100f;
    private const string ResurrectButton = "f";
    private InputAction _resurrect;

    private void Awake()
    {
        // Создание новой Input Action
        _resurrect = new InputAction("Resurrect", InputActionType.Button);
        _resurrect.AddBinding($"<Keyboard>/{ResurrectButton}");
        
        // Подписка на событие
        _resurrect.performed += _ => Resurrect();
        
        // Активация
        _resurrect.Enable();
    }
    
    private void OnDestroy()
    {
        _resurrect.Disable();
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
