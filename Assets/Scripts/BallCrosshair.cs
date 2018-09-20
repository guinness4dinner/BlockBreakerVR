using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCrosshair : MonoBehaviour {

    public Ball ball1;
    public bool vertical = true;

	
	// Update is called once per frame
	void Update ()
    {
        LockToBall();
    }

    private void LockToBall()
    {
        if (vertical)
        {
            var ballPosX = ball1.transform.position.x;
            transform.position = new Vector3(ballPosX, transform.position.y, transform.position.z);
        }
        else
        {
            var ballPosY = ball1.transform.position.y;
            transform.position = new Vector3(transform.position.x, ballPosY, transform.position.z);
        }

    }
}
