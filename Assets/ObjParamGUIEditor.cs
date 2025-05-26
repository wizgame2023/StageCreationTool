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
        property.Add(nameof(StageObject.VersionDeff), serializedObject.FindProperty(nameof(StageObject.VersionDeff)));
        property.Add(nameof(StageObject.InvisibleNum), serializedObject.FindProperty(nameof(StageObject.InvisibleNum)));
        property.Add(nameof(StageObject.ConveyerSpeed), serializedObject.FindProperty(nameof(StageObject.ConveyerSpeed)));
        property.Add(nameof(StageObject.ConnectNum), serializedObject.FindProperty(nameof(StageObject.ConnectNum)));
        property.Add(nameof(StageObject.Return), serializedObject.FindProperty(nameof(StageObject.Return)));
        property.Add(nameof(StageObject.FanPower), serializedObject.FindProperty(nameof(StageObject.FanPower)));
        property.Add(nameof(StageObject.WindDistance), serializedObject.FindProperty(nameof(StageObject.WindDistance)));
        property.Add(nameof(StageObject.IsCollapse), serializedObject.FindProperty(nameof(StageObject.IsCollapse)));
        property.Add(nameof(StageObject.MovieIvent), serializedObject.FindProperty(nameof(StageObject.MovieIvent)));
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
                // 接続番号
                property[nameof(StageObject.VersionDeff)].intValue =
                    EditorGUILayout.IntField("モデル違い", property[nameof(StageObject.VersionDeff)].intValue);
                break;
            case StageObject.ENUM_ObjTypes.壁:
                // 接続番号
                property[nameof(StageObject.InvisibleNum)].intValue =
                    EditorGUILayout.IntField("透過番号", property[nameof(StageObject.InvisibleNum)].intValue);
                break;
            case StageObject.ENUM_ObjTypes.敵:
                break;
            case StageObject.ENUM_ObjTypes.ベルトコンベア:
                // ベルトコンベアのスピード
                property[nameof(StageObject.ConveyerSpeed)].floatValue = 
                    EditorGUILayout.FloatField("ベルトコンベアのスピード", property[nameof(StageObject.ConveyerSpeed)].floatValue);
                // 接続番号
                property[nameof(StageObject.ConnectNum)].intValue =
                    EditorGUILayout.IntField("接続番号", property[nameof(StageObject.ConnectNum)].intValue);
                break;
            case StageObject.ENUM_ObjTypes.スイッチとドア:
                // 接続番号
                property[nameof(StageObject.ConnectNum)].intValue =
                    EditorGUILayout.IntField("接続番号", property[nameof(StageObject.ConnectNum)].intValue);
                // 軌道し続けるか
                property[nameof(StageObject.Return)].boolValue =
                    EditorGUILayout.Toggle("起動し続けるか", property[nameof(StageObject.Return)].boolValue);
                break;
            case StageObject.ENUM_ObjTypes.扇風機:
                property[nameof(StageObject.FanPower)].floatValue =
                    EditorGUILayout.FloatField("風力", property[nameof(StageObject.FanPower)].floatValue);
                property[nameof(StageObject.WindDistance)].floatValue =
                    EditorGUILayout.FloatField("風の飛距離", property[nameof(StageObject.WindDistance)].floatValue);                
                // 接続番号
                property[nameof(StageObject.ConnectNum)].intValue =
                    EditorGUILayout.IntField("接続番号", property[nameof(StageObject.ConnectNum)].intValue);
                break;
            case StageObject.ENUM_ObjTypes.露出配線:
                // 接続番号
                property[nameof(StageObject.ConnectNum)].intValue =
                    EditorGUILayout.IntField("接続番号", property[nameof(StageObject.ConnectNum)].intValue);
                break;
            case StageObject.ENUM_ObjTypes.分割壁:
                property[nameof(StageObject.IsCollapse)].boolValue =
                    EditorGUILayout.Toggle("破壊可能", property[nameof(StageObject.IsCollapse)].boolValue);
                break;
            case StageObject.ENUM_ObjTypes.ムービーイベント判定:
                property[nameof(StageObject.MovieIvent)].enumValueIndex =
                    EditorGUILayout.Popup("ムービーイベントの種類", property[nameof(StageObject.MovieIvent)].enumValueIndex, property[nameof(StageObject.MovieIvent)].enumNames);
                break;
            case StageObject.ENUM_ObjTypes.柱:
                property[nameof(StageObject.VersionDeff)].intValue =
                    EditorGUILayout.IntField("モデル違い", property[nameof(StageObject.VersionDeff)].intValue);
                property[nameof(StageObject.InvisibleNum)].intValue =
                    EditorGUILayout.IntField("透過番号", property[nameof(StageObject.InvisibleNum)].intValue);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}