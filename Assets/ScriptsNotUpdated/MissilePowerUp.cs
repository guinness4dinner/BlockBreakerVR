using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerUp : MonoBehaviour {

    Paddle paddle1;

    public void Activate()
    {
        paddle1 = FindObjectOfType<Paddle>();
        paddle1.MissilePaddle();
    }
}
