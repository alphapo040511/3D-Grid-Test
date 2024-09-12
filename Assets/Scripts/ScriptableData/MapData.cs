using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "NewLevel", menuName = "CustomData/LevelData")]       //���� ���� �޴��� �߰� �����ش�.
public class MapData : ScriptableObject
{
    public BlockIndex BlockIndexData;
    public int[,,] BlockArr = new int[,,] { };
    public Dictionary<Vector3Int, int> BlockDataDictionary = new Dictionary<Vector3Int, int> { };
    public TextAsset mapJsonFile;

#if UNITY_EDITOR
    public void SaveToJson()
    {
        if(mapJsonFile == null)
        {
            Debug.Log("�� ������ �����ϴ�.");
            return;
        }

        var data = JsonConvert.SerializeObject(new
        {
            BlockArr,
            BlockDataDictionary
        }, Formatting.Indented);        //JSON ��ȯ �� ���� ���� ����

        System.IO.File.WriteAllText(AssetDatabase.GetAssetPath(mapJsonFile), data);       //���Ͽ� JSON�� ����.
        AssetDatabase.Refresh();                                                            //�Ϸ��� ��������    
    }

    public void LoadFormJson()
    {
        if(mapJsonFile == null)
        {
            Debug.Log("�� ������ �����ϴ�.");
            return;
        }

        var data = JsonConvert.DeserializeAnonymousType(mapJsonFile.text, new
        {
            BlockArr = new int[,,] { },
            BlockDataDictionary = new Dictionary<Vector3Int, int> { }
        });

        BlockArr = data.BlockArr;
        BlockDataDictionary = data.BlockDataDictionary;
    }

#endif
}
