using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLineIndicator : MonoBehaviour {

    public Ball ball1;
	
	// Update is called once per frame
	void Update ()
    {
        LockToBall();
    }

    private void LockToBall()
    {
        var ballPosZ = ball1.transform.position.z;
        transform.position = new Vector3(transform.position.x, transform.position.y, ballPosZ);
    }
}
