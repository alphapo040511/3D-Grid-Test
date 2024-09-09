using log4net.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelGrid))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()           //���� OnInspectorGUI �Լ��� ������
    {
        var levelData = (LevelGrid)target;   //SequenceData �� Editor�� Ÿ������ �����Ѵ�.

        DrawDefaultInspector();                     //�ν����� ǥ��

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
