using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevel", menuName = "CustomData/LevelData")]       //���� ���� �޴��� �߰� �����ش�.
public class MapData : ScriptableObject
{
    public int[,,] BlockArr = new int[,,] { };
}
