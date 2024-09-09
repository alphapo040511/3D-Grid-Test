using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockData", menuName = "BlockData/NewData")]
public class BlockData : ScriptableObject
{
    public Vector3 Pos;
    public GameObject BlockObject;
}
