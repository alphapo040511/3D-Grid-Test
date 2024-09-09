using log4net.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelGrid))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()           //기존 OnInspectorGUI 함수를 재정의
    {
        var levelData = (LevelGrid)target;   //SequenceData 의 Editor를 타겟으로 수정한다.

        DrawDefaultInspector();                     //인스펙터 표시

        if (levelData != null)
        {
            EditorGUILayout.LabelField("BlockData", EditorStyles.boldLabel);
            if (levelData != null)
            {
                for (int x = 0; x < levelData.Blocks.GetLength(0); x++)
                {
                    for (int y = 0; y < levelData.Blocks.GetLength(1); y++)
                    {
                        for (int z = 0; z < levelData.Blocks.GetLength(2); z++)
                        {
                            if (levelData.Blocks[x, y, z] != null)
                            {
                                EditorGUILayout.LabelField($"Block ({x},{y},{z}) : [{string.Join(",", "1")}]");
                            }
                        }
                    }
                }
            }

        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(levelData);
        }
    }
}
#endif
