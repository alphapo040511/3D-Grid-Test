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
        // ���� Ȱ��ȭ�� ������ ��������, ������ ���� ����
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));

        // ������ Ÿ��Ʋ ����
        window.titleContent.text = "LevelGrid";

        window.Show();



        // �ּ�, �ִ� ũ�� ����
        window.minSize = new Vector2(340f, 150f);
        window.maxSize = new Vector2(320f, 640f);
    }

    private void OnEnable()
    {

    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        level = EditorGUILayout.ObjectField("������ �� ������", level, typeof(LevelGrid), false) as LevelGrid;

        block = EditorGUILayout.ObjectField("������ �� ������", block, typeof(GameObject), true) as GameObject;

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
                Debug.LogError("�����Ͱ� �����ϴ�.");
            }
        }

        GUILayout.EndVertical();
    }
}
#endif
