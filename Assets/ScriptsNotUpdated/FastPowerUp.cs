using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPowerUp : MonoBehaviour {

    GameSession gameSession;
    float speedIncrease = 25f;

    private void Start()
    {
        gameSession = GameSession.instance;
    }

    public void Activate()
    {
        gameSession.gameSpeed *= 1 + speedIncrease / 100;
    }
}
