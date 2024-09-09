using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelData/NewData")]
public class LevelGrid : ScriptableObject
{
    public BlockData[,,] Blocks = new BlockData[16,3,16];
}
