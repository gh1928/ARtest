using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    public float changeInterval = 0.5f;

    WaitForSeconds intervalWait;

    [SerializeField]
    Renderer line;

    [SerializeField]
    GameObject spotLight;

    [SerializeField]
    GoalChecker topChecker;

    [SerializeField]
    GoalChecker bottomChecker;

    private bool topGoaledin = false;

    private void Awake()
    {
        intervalWait = new WaitForSeconds(changeInterval);

        topChecker.TriggerEvent.AddListener(()=>topGoaledin = true);
        bottomChecker.TriggerEvent.AddListener(CheckGoal);
    }

    public void ResetGoalCheck()
    {
        topGoaledin = false;
    }

    private void CheckGoal()
    {
        if (!topGoaledin)
            return;

        StartCoroutine(GetScoreBlinkEffectCoroutine());

        Manager.Instance.GetScore();
    }

    IEnumerator GetScoreBlinkEffectCoroutine()
    {
        for(int i = 0; i < 3; i++)
        {
            line.material.color = Color.yellow;
            spotLight.SetActive(true);
            yield return intervalWait;                      

            line.material.color = Color.white;
            spotLight.SetActive(false);            
            yield return intervalWait;
        }
    }
}
