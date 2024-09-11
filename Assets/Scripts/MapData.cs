using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevel", menuName = "CustomData/LevelData")]       //생성 파일 메뉴에 추가 시켜준다.
public class MapData : ScriptableObject
{
    public int[,,] BlockArr = new int[,,] { };
}
