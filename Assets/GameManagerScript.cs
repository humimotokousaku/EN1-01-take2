using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // �z��̐錾
    int[] map;


    // �N���X�̒��A���\�b�h�̊O�ɏ������Ƃɒ���
    void PrintArray()
    {
        // �ǉ��B������̐錾�Ə�����
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //// �v�f��������o��
            //Debug.Log(map[i] + ",");
            //  �ύX�B������Ɍ������Ă���
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    // �Ԃ�l�̌^�ɒ���
    int GetPlayerIndex()
    {
        for(int i = 0;i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    // 
    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if(moveTo < 0 || moveTo >= map.Length)
        {
            // �����Ȃ��������ɏ����A���^�[������B�������^�[��
            return false;
        }
        if (map[moveTo] == 2)
        {
            // �ǂ̕����ֈړ����邩�Z�o
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            if (!success) 
            {
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �z��̎��Ԃ̐����Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            // �ړ�����
           int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �ړ�����
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
