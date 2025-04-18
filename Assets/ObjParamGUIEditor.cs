using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.Profiling;
using System.Collections.Generic;

[CustomEditor(typeof(StageObject))]
public class ObjParamGUIEditor : Editor
{
    Dictionary<string, SerializedProperty> property = new Dictionary<string, SerializedProperty>();

    private void OnEnable()
    {
        property.Add(nameof(StageObject.objectNumber), serializedObject.FindProperty(nameof(StageObject.objectNumber)));
        property.Add(nameof(StageObject.objectType), serializedObject.FindProperty(nameof(StageObject.objectType)));
        property.Add(nameof(StageObject.ConveyerSpeed), serializedObject.FindProperty(nameof(StageObject.ConveyerSpeed)));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        property[nameof(StageObject.objectNumber)].intValue = EditorGUILayout.IntField("オブジェクトの番号", property[nameof(StageObject.objectNumber)].intValue);
        property[nameof(StageObject.objectType)].enumValueIndex = EditorGUILayout.Popup(
            "オブジェクトの種類",
            property[nameof(StageObject.objectType)].enumValueIndex,
            property[nameof(StageObject.objectType)].enumNames
        );
 
        switch ((StageObject.ENUM_ObjTypes)property[nameof(StageObject.objectType)].enumValueIndex)
        {
            case StageObject.ENUM_ObjTypes.固定:
                break;
            case StageObject.ENUM_ObjTypes.ベルトコンベア:
                // ベルトコンベアのスピード
                property[nameof(StageObject.ConveyerSpeed)].floatValue = 
                    EditorGUILayout.FloatField("ベルトコンベアのスピード", property[nameof(StageObject.ConveyerSpeed)].floatValue);
                break;
            case StageObject.ENUM_ObjTypes.スイッチとドア:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}