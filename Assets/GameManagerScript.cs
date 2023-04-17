using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // 配列の宣言
    int[] map;


    // クラスの中、メソッドの外に書くことに注意
    void PrintArray()
    {
        // 追加。文字列の宣言と初期化
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //// 要素数を一つずつ出力
            //Debug.Log(map[i] + ",");
            //  変更。文字列に結合していく
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    // 返り値の型に注意
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
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (map[moveTo] == 2)
        {
            // どの方向へ移動するか算出
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
        // 配列の実態の生成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            // 移動処理
           int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 移動処理
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
