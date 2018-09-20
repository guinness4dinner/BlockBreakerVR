using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoseCollider : MonoBehaviour 
{
    GameSession gameSession;

    private void Start()
    {
        gameSession = GameSession.instance;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            gameSession.LostBall();
        }
    }

}
