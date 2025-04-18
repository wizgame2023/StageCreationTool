using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public enum ENUM_ObjTypes
    {
        固定,
        敵,
        ベルトコンベア,
        スイッチとドア,
        扇風機,
        露出配線
    }

    // オブジェクトの番号
    public int objectNumber;

    // オブジェクトのタイプ
    public ENUM_ObjTypes objectType;

    //-ベルトコンベアのパラメータ------------------------------
    public float ConveyerSpeed;
    //---------------------------------------------------------

    //-スイッチとドアのパラメータ------------------------------
    //-扇風機のパラメータ--------------------------------------
    //-露出配線のパラメータ------------------------------------
    public int ConnectNum;

    //-スイッチとドアのパラメータ------------------------------
    public bool Return;
    //---------------------------------------------------------

    //-扇風機のパラメータ--------------------------------------
    public float FanPower;
    public float WindDistance;
    //---------------------------------------------------------
}