using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : MonoBehaviour {

    Paddle paddle1;

    public void Activate()
    {
        Debug.Log("LASER ACTIVATE");
        paddle1 = FindObjectOfType<Paddle>();
        paddle1.LaserPaddle();
    }
}
