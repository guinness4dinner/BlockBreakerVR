using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour {

    public Ball ball1;
    public Paddle paddle1;
    [SerializeField] Canvas winCanvas;
    [SerializeField] int breakableBlocks = 0; //serialized for debug purposes
    public PowerUp[] powerUpTypes;
    public Canvas pauseMenu;
    public bool hasStarted = false;
    public float winCanvasTimerLength = 5f;
    


    GameSession gameSession;
    SceneLoader sceneLoader;

    float gameSpeedBeforePause;
    bool winCanvasTimerEnable = false;
    float elapsedTime = 0f;

    // Use this for initialization
    void Start () 
    {
        gameSession = GameSession.instance;
        sceneLoader = FindObjectOfType<SceneLoader>();
        Cursor.visible = false;
    }

    private void Update()
    {
        if (winCanvasTimerEnable)
        {
            elapsedTime += Time.deltaTime;
        }
        
        if (elapsedTime >= winCanvasTimerLength)
        {
            sceneLoader.LoadNextLevel();
        }
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestoyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        Destroy(ball1.GetComponent<Rigidbody>());
        paddle1.enableMovement = false;
        hasStarted = false;
        winCanvas.enabled = true;
        ResetPowerUps();
        //add animated particle system and delay here.
        Cursor.visible = true;
        winCanvasTimerEnable = true;
    }

    public void NewPaddle()
    {
        ResetPowerUps();
        ResetBallToPaddle();
    }

    private void ResetPowerUps()
    {
        gameSession.SetDefaultGameSpeed();
        paddle1.NormalPaddle();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.enabled)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseMenu.enabled = true;
        gameSpeedBeforePause = gameSession.gameSpeed;
        paddle1.enableMovement = false;
        Cursor.visible = true;
        FindObjectOfType<GameSession>().gameSpeed = 0f;
    }

    private void ResumeGame()
    {
        pauseMenu.enabled = false;
        paddle1.enableMovement = true;
        Cursor.visible = false;
        gameSession.gameSpeed = gameSpeedBeforePause;
    }

    private void ResetBallToPaddle()
    {
        paddle1.enableMovement = false;
        ball1.GetComponent<Rigidbody>().Sleep();
        hasStarted = false;
        ball1.MoveBallToPaddle();
        ball1.GetComponent<Rigidbody>().WakeUp();
        paddle1.enableMovement = true;
    }

}
