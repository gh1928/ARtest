using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalChecker : MonoBehaviour
{
    public UnityEvent TriggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.Invoke();
    }
}
