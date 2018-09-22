using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] Material[] blockBroken;
    [SerializeField] int powerUpChanceMetal = 40;
    [SerializeField] int powerUpChance = 15;

    int currentBlockIndex = 0;

    LevelManager levelManager;
    GameSession gameSession;
    PowerUp[] powerUpTypes;


    private void Start()
    {
        gameSession = GameSession.instance;
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.CountBreakableBlocks();
        powerUpTypes = levelManager.powerUpTypes;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentBlockIndex > blockBroken.Length - 1)
        {
            DestroyBlock();
        }
        else
            {
                GetComponent<Renderer>().material = blockBroken[currentBlockIndex];
                currentBlockIndex++;
            }
    }

    public void DestroyBlock()
    {
        gameSession.IncreaseScore();
        levelManager.BlockDestoyed();
        //RollForPowerUp(blockBroken.Length);
        Destroy(gameObject);
    }

    private void RollForPowerUp(int blockType)
    {
        int powerUpRoll = Random.Range(0, 100);

        if (blockType > 1 && powerUpRoll < powerUpChanceMetal)
        {
            SpawnPowerUp();
        }
        else if (powerUpRoll < powerUpChance)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        int powerUpTypeRoll = Random.Range(0, 100);
        int blockTypeRolled;

        if (powerUpTypeRoll >= 0 && powerUpTypeRoll < 12)
        {
            blockTypeRolled = 0;
        }
        else if (powerUpTypeRoll >= 12 && powerUpTypeRoll < 25)
        {
            blockTypeRolled = 1;
        }
        else if (powerUpTypeRoll >= 25 && powerUpTypeRoll < 37)
        {
            blockTypeRolled = 2;
        }
        else if (powerUpTypeRoll >= 37 && powerUpTypeRoll < 50)
        {
            blockTypeRolled = 3;
        }
        else if (powerUpTypeRoll >= 50 && powerUpTypeRoll < 62)
        {
            blockTypeRolled = 4;
        }
        else if (powerUpTypeRoll >= 62 && powerUpTypeRoll < 75)
        {
            blockTypeRolled = 5;
        }
        else if (powerUpTypeRoll >= 75 && powerUpTypeRoll < 87)
        {
            blockTypeRolled = 6;
        }
        else
        {
            blockTypeRolled = 7;
        }

        Vector3 blockPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        Instantiate(powerUpTypes[blockTypeRolled], blockPos, Quaternion.identity);
    }
}
