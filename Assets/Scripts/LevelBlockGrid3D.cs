using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Block Grid 3D")]
public class LevelBlockGrid3D : ScriptableObject 
{
    [SerializeField] string[] col1Types = new string[8];
    [SerializeField] string[] col2Types = new string[8];
    [SerializeField] string[] col3Types = new string[8];
    [SerializeField] string[] col4Types = new string[8];
    [SerializeField] string[] col5Types = new string[8];
    [SerializeField] string[] col6Types = new string[8];
    [SerializeField] Transform[] blockTypes;
    [SerializeField] string[] col1Colors = new string[8];
    [SerializeField] string[] col2Colors = new string[8];
    [SerializeField] string[] col3Colors = new string[8];
    [SerializeField] string[] col4Colors = new string[8];
    [SerializeField] string[] col5Colors = new string[8];
    [SerializeField] string[] col6Colors = new string[8];
    [SerializeField] Color[] colors;

    public int[,][] GetBlockTypesIntArray()
    {
        int[,][] result = new int[8, 6][];
        for (int row = 0; row < 8; row++)
        {
            string[] colStrings = new string[] { col1Types[row], col2Types[row], col3Types[row], col4Types[row], col5Types[row], col6Types[row] };
            
            for (int col = 0; col < 6; col++)
            {
                int[] intsFromStrings = StringToInt(colStrings[col]);
                result[row, col] = intsFromStrings;
            }
            
        }
        return result;
    }

    public Transform[] GetBlockTypes()
    {
        return blockTypes;
    }

    public int[,][] GetBlockColorsIntArray()
    {
        int[,][] result = new int[8, 6][];
        for (int row = 0; row < 8; row++)
        {
            string[] colStrings = new string[] { col1Colors[row], col2Colors[row], col3Colors[row], col4Colors[row], col5Colors[row], col6Colors[row] };
            
            for (int col = 0; col < 6; col++)
            {
                int[] intsFromStrings = StringToInt(colStrings[col]);
                result[row, col] = intsFromStrings;
            }

        }
        return result;
    }

    public Color[] GetColors()
    {
        return colors;
    }

    private int[] StringToInt(string stringofints)
    {

        string[] intStringSplit = stringofints.Split(',');
        int[] result = new int[intStringSplit.Length];
        for (int i = 0; i < intStringSplit.Length; i++)
        {
            //parse and store each value into int[] to be returned
            result[i] = int.Parse(intStringSplit[i]);
        }

        return result;
    }
}
