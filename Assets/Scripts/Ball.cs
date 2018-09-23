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
    [SerializeField] float randomPushRangeMax = 0.7f;
    [SerializeField] float randomPushRangeMin = 0.3f;

    //state
    Vector3 ballStartPos;
    Vector3 paddleToBallVector;

    //Component References
    AudioSource ballAudioSource;
    LevelManager levelManager;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ballAudioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
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
            rigidBody.velocity = new Vector3(xPush, yPush, zPush); 
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

    private void OnCollisionExit(Collision collision)
    {
        var velocity = rigidBody.velocity;

        if (velocity.z < 1f && velocity.z > -1f && transform.position.z > -3f)
        {
            if (velocity.z > 0f)
            {
                Debug.Log("Nudged Foward " + velocity.z);
                Nudge(Vector3.forward, randomPushRangeMin, randomPushRangeMax);
            }
            else
            {
                Debug.Log("Nudged Back" + velocity.z);
                Nudge(Vector3.back, randomPushRangeMin, randomPushRangeMax);
            }
        }

        if (velocity.y < 0.05f && velocity.y > -0.05f)
        {
            if (transform.position.y < 6f)
            {
                Nudge(Vector3.up, randomPushRangeMin, randomPushRangeMax);
            }
            else if (transform.position.y > 10.5f)
            {
                Nudge(Vector3.down, randomPushRangeMin, randomPushRangeMax);
            }
        }

    }

    private void Nudge(Vector3 direction, float RangeMin, float RangeMax)
    {       
        rigidBody.AddForce(direction * UnityEngine.Random.Range(RangeMin, RangeMax), ForceMode.Impulse); 
    }

}
