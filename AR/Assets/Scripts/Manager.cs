using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

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
    float touchStartTime;

    bool fired = false;

    [SerializeField]
    GameObject restBallButton;

    [SerializeField]
    Hoop hoop;

    [SerializeField]
    BallContolPanal ballContolPanal;

    private void Awake()
    {
        ballContolPanal.PointDownAction += ReadyFire;
        ballContolPanal.PointUpAction += TryFire;
    }
    private void Start()
    {
        Instance= this;
        cam = Camera.main;    
    }
    public void StartGame()
    {   
        ball = Instantiate(ballPrefab, ballPivot);
        ball.transform.position = ballPivot.position;
    }
    public void Fire(float force)
    {
        ball.Fire(force);
        fired = true;

        restBallButton.SetActive(true);
    }

    public void ResetBall()
    {
        hoop.ResetGoalCheck();

        ball.RestBall(ballPivot);
        restBallButton.SetActive(false);

        Invoke(nameof(FireDelay), 0.5f);
    }

    private void FireDelay()
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

    public void ReadyFire(Vector2 pos)
    {
        touchStartPos = pos;
        touchStartTime = Time.time;
    }

    public void TryFire(Vector2 pos)
    {
        if (fired)
            return;
        
        float touchTime = Time.time - touchStartTime;

        var force = (pos.y - touchStartPos.y) / touchTime;   

        Fire(force < 0 ? 0 : force * forceAdjust);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();   
    }
}
