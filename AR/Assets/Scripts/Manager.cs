using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

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

    public GameObject hoop;
    public void TestCode()
    {   
        hoop.transform.LookAt(cam.transform.position);

        Vector3 eulerAngles = hoop.transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        hoop.transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
