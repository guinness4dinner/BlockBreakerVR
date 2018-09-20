using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] AudioClip paddleHitSound;
    [SerializeField] AudioClip blockHitSound;
    [SerializeField] float xPaddleOffsetStart = 0f;
    [SerializeField] float yPaddleOffsetStart = 0.7f;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;

    //state
    Vector2 ballStartPos;
    Vector2 paddleToBallVector;

    //Component References
    AudioSource ballAudioSource;
    LevelManager levelManager;

	// Use this for initialization
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ballAudioSource = GetComponent<AudioSource>();
        MoveBallToPaddle();
    }

    public void MoveBallToPaddle()
    {
        ballStartPos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y) + new Vector2(xPaddleOffsetStart, yPaddleOffsetStart);
        transform.position = ballStartPos;
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!levelManager.hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        } 
    }

    private void LaunchOnMouseClick()
    {
       if (Input.GetMouseButtonDown(0)) //&& gameManager.gameSpeed > 0f)
       {
            levelManager.hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush); 
       }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Paddle>() && levelManager.hasStarted)
        {
            ballAudioSource.PlayOneShot(paddleHitSound);
        }
        else if (collision.gameObject.GetComponent<Block>())
        {
            ballAudioSource.PlayOneShot(blockHitSound);
        }
    }

}
