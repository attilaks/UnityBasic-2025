using UnityEngine;

public class ScriptCoordinator : MonoBehaviour
{
    [SerializeField] private ValuesLogger valuesLogger;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Victim victim;

    private void Awake()
    {
        valuesLogger.LoggerJobIsFinishedEvent += OnLoggerJobIsFinished;
        damageDealer.DamageIsBeingDoneEvent += OnDamageIsBeingDone;
        damageDealer.gameObject.SetActive(false);
        valuesLogger.gameObject.SetActive(true);
    }

    private void OnDamageIsBeingDone(float damageDone)
    {
        victim.ApplyDamage(damageDone);
    }

    private void OnLoggerJobIsFinished()
    {
        valuesLogger.gameObject.SetActive(false);
        damageDealer.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        valuesLogger.LoggerJobIsFinishedEvent -= OnLoggerJobIsFinished;
        damageDealer.DamageIsBeingDoneEvent -= OnDamageIsBeingDone;
    }
}
