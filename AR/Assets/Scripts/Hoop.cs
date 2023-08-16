using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    public int GoalChecker { get; set; } = 0;

    public float changeInterval = 0.5f;

    WaitForSeconds intervalWait;

    [SerializeField]
    Renderer line;

    [SerializeField]
    GameObject spotLight;

    private void Awake()
    {
        intervalWait = new WaitForSeconds(changeInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        GoalChecker++;

        if (GoalChecker == 2)
        {
            StartCoroutine(LineBlinkCoroutine());

            GoalChecker = 0;
            Manager.Instance.GetScore();
        }
    }

    IEnumerator LineBlinkCoroutine()
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
