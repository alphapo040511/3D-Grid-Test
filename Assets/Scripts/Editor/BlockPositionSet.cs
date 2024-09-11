using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Search;
using System.Drawing;

#if UNITY_EDITOR
public class BlockPositionSet : EditorWindow
{
    private LevelData levelData;
    private BlockIndex blockIndex;
    private int XSize = 16, YSize = 3, ZSize = 16;
    private int NowHeight = 0;
    private int[,,] map = new int[16, 3, 16];

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
        blockIndex = levelData.mapData.BlockIndexData;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(640));                                //GUI 레이아웃 시작 설정

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("LevelData 지정", GUILayout.Width(100), GUILayout.Height(50)))
        {
            levelData = FindAnyObjectByType<LevelData>();
            if (levelData == null)
            {
                GameObject level = new GameObject("LevelData");
                level.transform.position = Vector3.zero;
                level.AddComponent<LevelData>();
            }
            blockIndex = levelData.mapData.BlockIndexData;
        }

        GUILayout.Space(10);
        if (GUILayout.Button("데이터 불러오기", GUILayout.Width(100), GUILayout.Height(50)))
        {
            levelData.LoadLevelData();
            map = levelData.BlockArr;
        }

        GUILayout.Space(10);
        if (GUILayout.Button("블럭 전부 채우기", GUILayout.Width(100), GUILayout.Height(50)))
        {
            AllBlocksChange(true);
        }

        GUILayout.Space(10);
        if (GUILayout.Button("블럭 전부 지우기",GUILayout.Width(100), GUILayout.Height(50)))
        {
            AllBlocksChange(false);
        }

            GUILayout.Space(10);

        if (GUILayout.Button("블럭 생성", GUILayout.Width(100), GUILayout.Height(50)))
        {
            SetBlock();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("데이터 저장", GUILayout.Width(100), GUILayout.Height(50)))
        {
            SetBlock();
            levelData.SaveLevelData(map);
        }

        EditorGUILayout.EndHorizontal();

        XSize = EditorGUILayout.IntField("맵 가로 크기", XSize);
        ZSize = EditorGUILayout.IntField("맵 세로 크기", ZSize);
        YSize = EditorGUILayout.IntField("맵 최대 높이", YSize);
        if (XSize <= 0) XSize = 1;
        if(ZSize <= 0) ZSize = 1;

        if (XSize != map.GetLength(0) || YSize != map.GetLength(1) || ZSize != map.GetLength(2))
        {
            map = new int[XSize, YSize, ZSize];
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-")) NowHeight--;
            NowHeight = EditorGUILayout.IntField("현재 Y좌표", NowHeight);
        if (GUILayout.Button("+")) NowHeight++;
        if (NowHeight < 0) NowHeight = 0;
        if (NowHeight >= YSize) NowHeight = YSize - 1;
        GUILayout.EndHorizontal();

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

    private void SetBlock()
    {
        levelData.ResetMap(XSize, YSize, ZSize);

        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    if (map[x, y, z] != 0)
                    {
                        var newBlock = Instantiate(blockIndex.Blocks[map[x, y, z] - 1], new Vector3(x, y, z), Quaternion.identity);
                        newBlock.transform.parent = levelData.gameObject.transform;
                        BlockData temp = newBlock.GetComponent<BlockData>();
                        temp.Initialized(x, y, z);
                    }
                }
            }
        }
    }

    private void AllBlocksChange(bool Bool)
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                map[x,NowHeight,z] = Bool ? 1 : 0;        //해당 높이만 채워지도록 변경
                //for (int y = 0; y < YSize; y++)
                //{
                //    map[x, y, z] = Bool ? 1 : 0;
                //}
            }
        }
    }

    private void DrawBlock(int x, int z)
    {
        if (levelData == null || blockIndex == null) return;

        Rect rect = GUILayoutUtility.GetRect(640 / XSize, 640 / ZSize);

        UnityEngine.Color blockColor = UnityEngine.Color.gray;
        switch(map[x, NowHeight, z])
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
            if (Event.current.button == 0)        //마우스 왼쪽 클릭
            {
                map[x, NowHeight, z] = (map[x, NowHeight, z] + 1) % (blockIndex.Blocks.Count + 1);
            }
            else if (Event.current.button == 1)      //마우스 오른쪽 클릭
            {
                map[x, NowHeight, z] = 0;
            }
            Event.current.Use();
        }
    }
}
#endif