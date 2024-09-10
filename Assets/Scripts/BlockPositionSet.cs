using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Search;
using System.Drawing;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelData))]        //SequenceData�� �����͸� �����ϰڴ�.     
public class BlockPositionSet : EditorWindow
{
    private LevelData levelData;
    private GameObject blockPrefab;
    private int XSize = 16, YSize = 3, ZSize = 16;
    private int[,] map = new int[16,16];

    [MenuItem("Tool/LevelEditor")]
    private static void ShowWindow()
    {
        var winodw = GetWindow<BlockPositionSet>();
        winodw.titleContent = new GUIContent("LevelData");
        winodw.Show();
    }

    private void OnEnable()
    {
        levelData = FindAnyObjectByType<LevelData>();
        if(levelData == null)
        {
            GameObject level = new GameObject("LevelData");
            level.transform.position = Vector3.zero;
            level.AddComponent<LevelData>();
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(640));                                //GUI ���̾ƿ� ���� ����

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("LevelData ����", GUILayout.Width(100), GUILayout.Height(50)))
        {
            levelData = FindAnyObjectByType<LevelData>();
            if (levelData == null)
            {
                GameObject level = new GameObject("LevelData");
                level.transform.position = Vector3.zero;
                level.AddComponent<LevelData>();
            }
        }

        GUILayout.Space(20);
        if (GUILayout.Button("�� ���� ä���", GUILayout.Width(100), GUILayout.Height(50)))
        {
            AllBlocksChange(true);
        }

        GUILayout.Space(20);
        if (GUILayout.Button("�� ���� �����",GUILayout.Width(100), GUILayout.Height(50)))
        {
            AllBlocksChange(false);
        }

            GUILayout.Space(20);

        if (GUILayout.Button("�� ����", GUILayout.Width(100), GUILayout.Height(50)))
        {
            levelData.ResetMap();

            for (int x = 0; x < XSize; x++)
            {
                for (int z = 0; z < ZSize; z++)
                {
                    if (map[x, z] != 0)
                    {
                        for (int y = 0; y < map[x, z]; y++)
                        {
                            var newBlock = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity);     //y�� ���� �κ��� map[x,z] �� �ٲٸ� �Ʒ� ������ �� ���� ������
                            newBlock.transform.parent = levelData.gameObject.transform;
                            BlockData temp = newBlock.GetComponent<BlockData>();
                            temp.XPos = x;
                            temp.ZPos = z;
                            temp.YPos = y;
                            levelData.AddBlock(x, z, temp);
                        }
                    }
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        blockPrefab = EditorGUILayout.ObjectField("����� �� ������", blockPrefab, typeof(GameObject), false) as GameObject;
        XSize = EditorGUILayout.IntField("�� ���� ũ��", XSize);
        ZSize = EditorGUILayout.IntField("�� ���� ũ��", ZSize);
        YSize = EditorGUILayout.IntField("�� �ִ� ����", YSize);
        if (XSize <= 0) XSize = 1;
        if(ZSize <= 0) ZSize = 1;

        if (XSize != map.GetLength(0) || ZSize != map.GetLength(1))
        {
            map = new int[XSize, ZSize];
        }

        

        EditorGUILayout.BeginHorizontal();

        for (int x = 0; x < XSize; x++)
        {
            EditorGUILayout.BeginVertical();
            for (int z = ZSize - 1; z >= 0; z--)
            {
                DrawBlock(x,z);
                EditorGUILayout.Space(1);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(1);
        }

        

        EditorGUILayout.EndHorizontal();

        


        EditorGUILayout.EndVertical();
    }

    private void AllBlocksChange(bool Bool)
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                map[x, z] = Bool ? 1 : 0;
            }
        }
    }

    private void DrawBlock(int x, int z)
    {
        if (levelData == null || blockPrefab == null) return;

        Rect rect = GUILayoutUtility.GetRect(640 / XSize, 640 / ZSize);

        UnityEngine.Color blockColor = UnityEngine.Color.gray;
        switch(map[x, z])
        {
            case 0:
                blockColor = UnityEngine.Color.gray;
                break;
            case 1:
                blockColor = UnityEngine.Color.green;
                break;
            case 2:
                blockColor = UnityEngine.Color.yellow;
                break;
            case 3:
                blockColor = UnityEngine.Color.red;
                break;
        }

       

        EditorGUI.DrawRect(rect, blockColor);

        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            if (Event.current.button == 0)        //���콺 ���� Ŭ��
            {
                map[x, z] = (map[x, z] + 1) % (YSize + 1);
            }
            else if (Event.current.button == 1)      //���콺 ������ Ŭ��
            {
                map[x, z] = 0;
            }
            Event.current.Use();
        }
    }
}
#endif