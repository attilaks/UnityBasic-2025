using Characters;
using UnityEngine;

namespace Tools
{
    public class ScriptCoordinator : MonoBehaviour
    {
        [SerializeField] private ValuesLogger valuesLogger;
        [SerializeField] private DamageDealer damageDealer;
        [SerializeField] private Victim victim;

        private void Awake()
        {
            valuesLogger.LoggerJobIsFinishedEvent += OnLoggerJobIsFinished;
            damageDealer.DamageIsBeingDoneEvent += OnDamageIsBeingDone;
            damageDealer.AutoDamageIsBeingDoneEvent += OnAutoDamageIsBeingDone;
            
            damageDealer.gameObject.SetActive(false);
            valuesLogger.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            valuesLogger.LoggerJobIsFinishedEvent -= OnLoggerJobIsFinished;
            damageDealer.DamageIsBeingDoneEvent -= OnDamageIsBeingDone;
            damageDealer.AutoDamageIsBeingDoneEvent -= OnAutoDamageIsBeingDone;
        }

        private void OnAutoDamageIsBeingDone(float damageDone)
        {
            victim.ApplyAutoDamage(damageDone);
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
    }
}
