using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class LevelEditorWindow : EditorWindow
{
    public LevelGrid level;
    public GameObject block;
    public int SetXPos;
    public int SetYPos;
    public int SetZPos;


    [MenuItem("Tools/LevelEditor")]
    private static void Init()
    {
        // 현재 활성화된 윈도우 가져오며, 없으면 새로 생성
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));

        // 윈도우 타이틀 지정
        window.titleContent.text = "LevelGrid";

        window.Show();



        // 최소, 최대 크기 지정
        window.minSize = new Vector2(340f, 150f);
        window.maxSize = new Vector2(320f, 640f);
    }

    private void OnEnable()
    {

    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        level = EditorGUILayout.ObjectField("설정할 블럭 데이터", level, typeof(LevelGrid), false) as LevelGrid;

        block = EditorGUILayout.ObjectField("설정할 블럭 데이터", block, typeof(GameObject), true) as GameObject;

        //GUILayout.BeginHorizontal();
        SetXPos = EditorGUILayout.IntField("X", SetXPos);
        SetYPos = EditorGUILayout.IntField("Y", SetYPos);
        SetZPos = EditorGUILayout.IntField("Z", SetZPos);
        //GUILayout.EndHorizontal();


        if (GUILayout.Button("SetBlock"))
        {
            if (block != null)
            {
                level.Blocks[SetXPos, SetYPos, SetZPos] = new BlockData();
                level.Blocks[SetXPos, SetYPos, SetZPos].Pos = new Vector3(SetXPos, SetYPos, SetZPos);
                level.Blocks[SetXPos, SetYPos, SetZPos].BlockObject = block;
                Debug.Log(level.Blocks[SetXPos, SetYPos, SetZPos].Pos);
            }
            else
            {
                Debug.LogError("데이터가 없습니다.");
            }
        }

        GUILayout.EndVertical();
    }
}
#endif
