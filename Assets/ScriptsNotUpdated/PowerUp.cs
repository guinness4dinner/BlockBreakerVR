using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] float yPush = -2f;
    
    // Use this for initialization
    void Start () 
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, yPush);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Paddle>())
        {
            IdentifyPowerUp();
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<LoseCollider>())
        {
            Destroy(gameObject);
        }
    }

    private void IdentifyPowerUp()
    {
        Debug.Log(gameObject.name);
        if (gameObject.GetComponent<FastPowerUp>()) { gameObject.GetComponent<FastPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<SlowPowerUp>()) { gameObject.GetComponent<SlowPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<ExtendPowerUp>()) { gameObject.GetComponent<ExtendPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<NarrowPowerUp>()) { gameObject.GetComponent<NarrowPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<CatchPowerUp>()) { gameObject.GetComponent<CatchPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<LaserPowerUp>()) { gameObject.GetComponent<LaserPowerUp>().Activate(); return; }
        if (gameObject.GetComponent<MissilePowerUp>()) { gameObject.GetComponent<MissilePowerUp>().Activate(); return; }
        if (gameObject.GetComponent<ExtraPaddlePowerUp>()) { gameObject.GetComponent<ExtraPaddlePowerUp>().Activate(); return; }
    }

}
