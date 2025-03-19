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
        // ���݂�scene�̖��O��CSV�t�@�C�����쐬����
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

        // �q�I�u�W�F�N�g���P���Ȃ��Ȃ�I��
        if (childTrans.childCount == 0)
        {
            return;
        }

        // �q�I�u�W�F�N�g��S�Ă݂�
        foreach (Transform objTrans in childTrans)
        {
            // �I�u�W�F�N�g�̔ԍ���Transform�̃p�����[�^���擾����
            int objNum = objTrans.GetComponent<StageObject>().GetObjcectNumber();
            Vector3 objScale = objTrans.localScale;
            Quaternion objQuaternion = objTrans.rotation;
            Vector3 objPosition = objTrans.position;

            // �p�����[�^����s�ɏ�������
            string[] objParamsOneLine = {
                // �ԍ�
                objNum.ToString(),
                // �T�C�Y
                objScale.x.ToString(),
                objScale.y.ToString(),
                objScale.z.ToString(),
                // ��]
                objQuaternion.x.ToString(),
                objQuaternion.y.ToString(),
                objQuaternion.z.ToString(),
                objQuaternion.w.ToString(),
                // �ʒu
                objPosition.x.ToString(), 
                objPosition.y.ToString(), 
                objPosition.z.ToString() 
            };

            // objParamsOneLine�̑S�Ă̗v�f���u,�v�ŘA��
            string objParams = string.Join(",", objParamsOneLine);

            // objParams��csv�t�@�C���ɏ�������
            stageObjectDeta.WriteLine(objParams);
        }

        Debug.Log("�v���W�F�N�g�����Ƀt�@�C���𐶐����܂�");
        Debug.Log("�X�e�[�W�I�u�W�F�N�g�f�[�^��ۑ����܂���");

        // �t�@�C�������
        stageObjectDeta.Close();
    }
}
