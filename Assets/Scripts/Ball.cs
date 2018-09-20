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
    [SerializeField] float yPaddleOffsetStart = 0f;
    [SerializeField] float zPaddleOffsetStart = 0.7f;
    [SerializeField] float xPush = 5f;
    [SerializeField] float yPush = 2f;
    [SerializeField] float zPush = 15f;

    //state
    Vector3 ballStartPos;
    Vector3 paddleToBallVector;

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
        ballStartPos = new Vector3(paddle1.transform.position.x, paddle1.transform.position.y, paddle1.transform.position.z) + new Vector3(xPaddleOffsetStart, yPaddleOffsetStart, zPaddleOffsetStart);
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
            GetComponent<Rigidbody>().velocity = new Vector3(xPush, yPush, zPush); 
       }
    }

    private void LockBallToPaddle()
    {
        Vector3 paddlePos = new Vector3(paddle1.transform.position.x, paddle1.transform.position.y, paddle1.transform.position.z);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter(Collision collision)
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
