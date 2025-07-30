using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCSV : MonoBehaviour
{
    private StreamWriter stageObjectDeta;

    void Start()
    {
        // 現在のsceneの名前でCSVファイルを作成する
        string senceName = SceneManager.GetActiveScene().name;
        stageObjectDeta = new StreamWriter(@senceName + @".csv", false, Encoding.GetEncoding("Shift_JIS"));
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveStageDeta();
        }
    }
    
    [ContextMenu("SaveStageDeta")]
    public void SaveStageDeta()
    {
        // 現在のsceneの名前でCSVファイルを作成する
        string senceName = SceneManager.GetActiveScene().name;
        stageObjectDeta = new StreamWriter(@senceName + @".csv", false, Encoding.GetEncoding("Shift_JIS"));

        Transform childTrans = gameObject.GetComponentInChildren<Transform>();

        // 子オブジェクトが１つもないなら終了
        if (childTrans.childCount == 0)
        {
            return;
        }

        // 子オブジェクトを全てみる
        foreach (Transform objTrans in childTrans)
        {
            // オブジェクトの番号とTransformのパラメータを取得する
            int objType = (int)objTrans.GetComponent<StageObject>().objectType;
            int objNum = objTrans.GetComponent<StageObject>().objectNumber;
            Vector3 objScale = objTrans.localScale;
            Quaternion objQuaternion = objTrans.rotation;
            Vector3 objPosition = objTrans.position;

            // パラメータを一行に書き込む
            List<string> objParamsList = new List<string>();
            // 種類
            objParamsList.Add(objType.ToString());
            // 番号
            objParamsList.Add(objNum.ToString());
            // サイズ
            objParamsList.Add(objScale.x.ToString());
            objParamsList.Add(objScale.y.ToString());
            objParamsList.Add(objScale.z.ToString());
            // 回転
            objParamsList.Add(objQuaternion.x.ToString());
            objParamsList.Add(objQuaternion.y.ToString());
            objParamsList.Add(objQuaternion.z.ToString());
            objParamsList.Add(objQuaternion.w.ToString());
            // 位置
            objParamsList.Add(objPosition.x.ToString());
            objParamsList.Add(objPosition.y.ToString());
            objParamsList.Add(objPosition.z.ToString());
            
            switch((StageObject.ENUM_ObjTypes)objType)
            {
                case StageObject.ENUM_ObjTypes.固定:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().VersionDeff.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.壁:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().InvisibleNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.敵:
                    break;
                case StageObject.ENUM_ObjTypes.ベルトコンベア:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().ConveyerSpeed.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().ConnectNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.スイッチとドア:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().ConnectNum.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().Return.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().InvisibleNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.扇風機:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().FanPower.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().WindDistance.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().ConnectNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.露出配線:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().ConnectNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.分割壁:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().IsCollapse.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.ムービーイベント判定:
                    int movieIvent = (int)objTrans.GetComponent<StageObject>().MovieIvent;
                    objParamsList.Add(movieIvent.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.柱:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().VersionDeff.ToString());
                    objParamsList.Add(objTrans.GetComponent<StageObject>().InvisibleNum.ToString());
                    break;
                case StageObject.ENUM_ObjTypes.看板:
                    objParamsList.Add(objTrans.GetComponent<StageObject>().SignboardData);
                    break;
            }

            // objParamsOneLineの全ての要素を「,」で連結
            string objParams = string.Join(",", objParamsList);

            // objParamsをcsvファイルに書き込む
            stageObjectDeta.WriteLine(objParams);
        }

        Debug.Log("プロジェクト直下にファイルを生成します");
        Debug.Log("ステージオブジェクトデータを保存しました");

        // ファイルを閉じる
        stageObjectDeta.Close();
    }
}

[CustomEditor(typeof(SaveCSV))]
public class StageSave : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveCSV saveCSV = target as SaveCSV;

        if (GUILayout.Button("ステージを保存"))
        {
            saveCSV.SaveStageDeta();
        }
    }
}
