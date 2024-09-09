using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExSetObject : EditorWindow
{
    public GameObject SetObject = null;
    public GameObject LevelGroup;
    public Vector3 SetPos;


    [MenuItem("Tools/SetObject")]
    private static void Init()
    {
        // 현재 활성화된 윈도우 가져오며, 없으면 새로 생성
        ExSetObject window = (ExSetObject)GetWindow(typeof(ExSetObject));
        window.Show();

        // 윈도우 타이틀 지정
        window.titleContent.text = "SetObject";

        // 최소, 최대 크기 지정
        window.minSize = new Vector2(340f, 150f);
        window.maxSize = new Vector2(320f, 640f);
    }

    private void OnEnable()
    {
        LevelGroup = GameObject.Find("Level");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        SetObject = EditorGUILayout.ObjectField("설치할 오브젝트", SetObject, typeof(GameObject), true) as GameObject;

        SetPos = EditorGUILayout.Vector3Field("설치할 위치",SetPos);


        if (GUILayout.Button("SetBlock"))
        {
            if(SetObject != null)
            {
                GameObject temp = new GameObject(SetPos.ToString());
                temp.transform.SetParent(LevelGroup.transform);
                temp.transform.position = SetPos;
            }
            else
            {
                Debug.LogWarning("오브젝트가 없습니다.");
            }
        }

        GUILayout.EndVertical();
    }
}
