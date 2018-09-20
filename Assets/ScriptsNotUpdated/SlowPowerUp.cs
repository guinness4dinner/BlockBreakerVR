using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPowerUp : MonoBehaviour {

    GameSession gameSession;
    float speedDecrease = 25f;

    private void Start()
    {
        gameSession = GameSession.instance;
    }

    public void Activate()
    {
        gameSession.gameSpeed *= 1 - speedDecrease / 100;
    }
}
