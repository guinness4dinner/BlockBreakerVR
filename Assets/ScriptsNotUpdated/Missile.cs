using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField] float yPush = 9f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, yPush);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Block>())
        { 
            collision.gameObject.GetComponent<Block>().DestroyBlock();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
