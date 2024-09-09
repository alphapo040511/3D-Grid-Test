using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetBlock : MonoBehaviour
{
    public LevelGrid Level;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < Level.Blocks.GetLength(0); x++)
        {
            for(int y = 0; y < Level.Blocks.GetLength(1); y++)
            {
                for(int z = 0; z < Level.Blocks.GetLength(2); z++)
                {
                    if (Level.Blocks[x, y, z] != null)
                    {
                        Instantiate(Level.Blocks[x, y, z].BlockObject, Level.Blocks[x, y, z].Pos, Quaternion.identity);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
