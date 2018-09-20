using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    [SerializeField] LevelBlockGrid3D levelBlockGrid;

    [System.NonSerialized] public Transform[] blockTypes;
    [System.NonSerialized] public Color[] colors;

    // Use this for initialization
    void Start ()
    {
        //Grab Block Types and Block Colors from Level Scriptable Object
        int[,][] blockTypes = levelBlockGrid.GetBlockTypesIntArray();
        int[,][] blockColors = levelBlockGrid.GetBlockColorsIntArray();
        this.blockTypes = levelBlockGrid.GetBlockTypes();
        colors = levelBlockGrid.GetColors();

        //Spawn the blocks, col by row

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                SpawnRow(ref blockTypes[row, col], ref blockColors[row, col], ref row, ref col);
            } 
        }
    }
    
    //Method for spawning one row of blocks
    private void SpawnRow(ref int[] blockTypes, ref int[] blockColors, ref int row, ref int col)
    {
        int index = 0;
        foreach (int element in blockTypes)  //Get the Block Type
        {
            if (element != 0)
            {
                var blockcolor = blockColors[index];  //Get the Block Color
                SpawnBlock(row, col, index, element, blockcolor); //Spawn One Block
            }
            index++;
        }      
    }

    //Method for Spawning a single block of a type (prefab) and color using on the grid by col and row.
    //These all use indexes into arrays of their type.
    private void SpawnBlock(int row, int col, int index, int blocktype, int blockcolor)
    {
        Transform block;
        block = Instantiate(blockTypes[blocktype - 1], new Vector3((3.17f * index) - 6.35f, 11.28f - (row * 1.02f), 2.275f + (col * 1.02f)), Quaternion.identity);
        if (blockcolor != 0)
        {
            block.GetComponent<Renderer>().material.color = colors[blockcolor - 1];
        }
        
    }

}
