using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    
    private void SaveStageDeta()
    {
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
            int objNum = objTrans.GetComponent<StageObject>().GetObjcectNumber();
            Vector3 objScale = objTrans.localScale;
            Quaternion objQuaternion = objTrans.rotation;
            Vector3 objPosition = objTrans.position;

            // パラメータを一行に書き込む
            string[] objParamsOneLine = {
                // 番号
                objNum.ToString(),
                // サイズ
                objScale.x.ToString(),
                objScale.y.ToString(),
                objScale.z.ToString(),
                // 回転
                objQuaternion.x.ToString(),
                objQuaternion.y.ToString(),
                objQuaternion.z.ToString(),
                objQuaternion.w.ToString(),
                // 位置
                objPosition.x.ToString(), 
                objPosition.y.ToString(), 
                objPosition.z.ToString() 
            };

            // objParamsOneLineの全ての要素を「,」で連結
            string objParams = string.Join(",", objParamsOneLine);

            // objParamsをcsvファイルに書き込む
            stageObjectDeta.WriteLine(objParams);
        }

        Debug.Log("プロジェクト直下にファイルを生成します");
        Debug.Log("ステージオブジェクトデータを保存しました");

        // ファイルを閉じる
        stageObjectDeta.Close();
    }
}
