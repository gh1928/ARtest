using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    string scoreStr = "Score : ";
    StringBuilder sb = new StringBuilder(16);

    [SerializeField]
    TextMeshProUGUI textMeshPro;

    private int score = 0;

    [SerializeField]
    Ball ballPrefab;
    Ball ball;

    [SerializeField]
    Transform ballPivot;

    Camera cam;

    [SerializeField]
    float forceAdjust = 0.001f;

    Vector3 touchStartPos;

    bool fired = false;

    bool readyLaunch = false;

    [SerializeField]
    GameObject restBallButton;

    [SerializeField]
    Hoop hoop;

    private void Start()
    {
        Instance= this;
        cam = Camera.main;    
    }
    public void StartGame()
    {   
        ball = Instantiate(ballPrefab, ballPivot);
        ball.transform.position = ballPivot.position;
        readyLaunch = true;
    }
    public void Fire(float force)
    {
        hoop.GoalChecker = 0;

        ball.Fire(force);
        fired = true;

        restBallButton.SetActive(true);
    }

    public void ResetBall()
    {
        ball.RestBall(ballPivot);
        restBallButton.SetActive(false);

        Invoke(nameof(ReadyFire), 0.5f);
    }

    private void ReadyFire()
    {
        fired = false;
    }

    public void GetScore()
    {
        sb.Clear();

        ++score;
        sb.Append(scoreStr).Append(score);

        textMeshPro.text = sb.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (fired)
            return;

        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPos = touch.position;
        }

        if(touch.phase == TouchPhase.Moved)
        {
            readyLaunch = false;
        }

        if (readyLaunch)
            return;

        if (touch.phase == TouchPhase.Ended)
        {
            var force = touch.position.y - touchStartPos.y;
            
            Fire(force < 0 ? 0 : force * forceAdjust);
        }        
    }
}
