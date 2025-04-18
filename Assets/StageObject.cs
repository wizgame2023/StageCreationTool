using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public enum ENUM_ObjTypes
    {
        固定,
        ベルトコンベア,
        スイッチとドア
    }

    // オブジェクトの番号
    public int objectNumber;

    // オブジェクトのタイプ
    public ENUM_ObjTypes objectType;

    //-ベルトコンベアのパラメータ------------------------------
    public float ConveyerSpeed;
    //---------------------------------------------------------
}