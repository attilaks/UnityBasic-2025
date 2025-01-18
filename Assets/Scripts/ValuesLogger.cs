using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ValuesLogger : MonoBehaviour
{
    public event Action LoggerJobIsFinishedEvent = delegate { };
    
    private void Start()
    {
        var minInt = 1;
        var maxInt = 10;
        var comparableInt = 6;
        
        var integer = Random.Range(minInt, maxInt);
        var floating = Random.Range(0f, 1f);
        var boolean = integer < 6;
        
        Debug.Log($"Random integer from {minInt} to {maxInt} is: {integer}");
        Debug.Log($"Is it true, that this integer is less than {comparableInt}? {boolean}");
        Debug.Log($"And float is {floating}");
        
        LoggerJobIsFinishedEvent.Invoke();
    }
}
