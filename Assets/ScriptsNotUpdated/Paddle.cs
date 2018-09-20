using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthInUnits = 21.333333f;
    [SerializeField] float paddleMinClampNormal = 1f;
    [SerializeField] float paddleMaxClampNormal = 15f;
    [SerializeField] float paddleMinClampNarrow = 0.71f;
    [SerializeField] float paddleMaxClampNarrow = 15.31f;
    [SerializeField] float paddleMinClampExt = 1.4f;
    [SerializeField] float paddleMaxClampExt = 14.59f;
    [SerializeField] Sprite[] paddleSprites;
    [SerializeField] Missile missile;
    [SerializeField] LaserBeam laserBeam;
    public bool enableMovement = true;
    bool enableMissile = false;
    bool enableLaser = false;

    float paddleMinClamp;
    float paddleMaxClamp;

    private void Start()
    {
        paddleMinClamp = paddleMinClampNormal;
        paddleMaxClamp = paddleMaxClampNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableMovement)
        {
            float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.x = Mathf.Clamp(mousePosInUnits, paddleMinClamp, paddleMaxClamp);
            transform.position = paddlePos;
        }

        if (enableMissile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                Instantiate(missile, new Vector3(paddlePos.x, paddlePos.y + 0.1f, 0f), Quaternion.identity);
            }
        }
        else if (enableLaser)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
                Instantiate(laserBeam, new Vector3(paddlePos.x + 0.6f, paddlePos.y + 0.4f, 0f), Quaternion.identity);
                Instantiate(laserBeam, new Vector3(paddlePos.x - 0.6f, paddlePos.y + 0.4f, 0f), Quaternion.identity);
            }
        }

    }

    public void ExtendPaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = false;
        colliders[1].enabled = true;
        colliders[2].enabled = false;
        enableMissile = false;
        enableLaser = false;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[1];
        paddleMinClamp = paddleMinClampExt;
        paddleMaxClamp = paddleMaxClampExt;
    }

    public void NarrowPaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        colliders[2].enabled = true;
        enableMissile = false;
        enableLaser = false;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[2];
        paddleMinClamp = paddleMinClampNarrow;
        paddleMaxClamp = paddleMaxClampNarrow;
    }

    public void MissilePaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
        enableMissile = true;
        enableLaser = false;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[0];
        paddleMinClamp = paddleMinClampNormal;
        paddleMaxClamp = paddleMaxClampNormal;
    }

    public void LaserPaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
        enableMissile = false;
        enableLaser = true;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[0];
        paddleMinClamp = paddleMinClampNormal;
        paddleMaxClamp = paddleMaxClampNormal;
    }

    public void CatchPaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
        enableMissile = false;
        enableLaser = false;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[0];
        paddleMinClamp = paddleMinClampNormal;
        paddleMaxClamp = paddleMaxClampNormal;
    }

    public void NormalPaddle()
    {
        EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
        enableMissile = false;
        enableLaser = false;
        GetComponent<SpriteRenderer>().sprite = paddleSprites[0];
        paddleMinClamp = paddleMinClampNormal;
        paddleMaxClamp = paddleMaxClampNormal;
    }
}
