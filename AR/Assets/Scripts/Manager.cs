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
    float forceAdjust = 0.0003f;
    float originForceAdjust;

    Vector3 touchStartPos;
    float touchStartTime;

    bool fired = false;

    [SerializeField]
    GameObject resetBallButton;
    CanvasGroup resetBallButtonCanvasGroup;

    [SerializeField]
    Hoop hoop;

    [SerializeField]
    BallContolPanal ballContolPanal;

    [SerializeField]
    GameObject optionPanel;

    private void Awake()
    {
        ballContolPanal.PointDownAction += ReadyFire;
        ballContolPanal.PointUpAction += TryFire;

        resetBallButtonCanvasGroup = resetBallButton.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        Instance= this;
        cam = Camera.main;
        originForceAdjust = forceAdjust;
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

        resetBallButton.SetActive(true);
    }

    public void ResetBall()
    {
        hoop.ResetGoalCheck();

        ball.RestBall(ballPivot);
        resetBallButton.SetActive(false);

        Invoke(nameof(FireDelay), 0.2f);
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
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffOptionPanel();
        }
    }

    public void OnOffOptionPanel()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);

        resetBallButtonCanvasGroup.alpha = optionPanel.activeSelf ? 0 : 1;
    }

    public void SensiSliderListener(float value)
    {
        forceAdjust = originForceAdjust * value;
    }

    public void GoalScaleListner(float value)
    {
        hoop.transform.localScale = Vector3.one * value;
    }

}
