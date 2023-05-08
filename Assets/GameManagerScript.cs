using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // 配列の宣言
    public GameObject playerPrefab;
    int[,] map; // レベルデザイン用の配列
    GameObject[,] field; // ゲーム管理用の配列
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {

        // 配列の実態の生成と初期化
        map = new int[,]
        {
            {0,0,0,0,0 },
            {0,0,1,0,0 },
            {0,0,0,0,0 }
        };
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];

        // string debugText = "";
        // 変更。二重for分で二次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    //// player
                    //GameObject instance = Instantiate(
                    //    playerPrefab,
                    //    new Vector3(x - 2, y, 0),
                    //    Quaternion.identity
                    //);
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                   
                }
            }
        }
    }

    // 返り値の型に注意
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
                if (field[y, x] == null)
                {
                    continue;
                }
                else
                {
                    if (field[y,x].tag == "Player")
                    {
                        return new Vector2Int(x, y);
                    }
                }
        }
        return new Vector2Int(-1, -1);
    }

    // 
    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveFrom.x < 0 || moveTo.x >= map.Length)
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (moveFrom.y < 0 || moveTo.y >= map.Length)
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (field[moveTo.y,moveTo.x] == null && field[moveTo.y, moveTo.x] == "Box")
        {
            Vector2Int velosity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velosity);
            if (!success) 
            {
                return false;      
            }

            // GameObjectの座標（position）を移動させてからインデックスの入れ替え
            field[moveFrom.y, moveFrom.x].transform.position = IndexToPosition(moveTo);
            field[moveTo.y, moveTo.x]   = field[moveFrom.y, moveFrom.x];
            field[moveFrom.y, moveFrom.x] = null;
            return true;
        }
            //if (field[moveTo] == 2)
            //{
            //    // どの方向へ移動するか算出
            //    int velocity = moveTo - moveFrom;
            //    bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //    if (!success)
            //    {
            //        return false;
            //    }
            //}

            field[moveTo] = tag;
        field[moveFrom] = 0;
        return true;
    }

    // Update is called once per frame
    //    void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //        {
    //            // 移動処理
    //            int playerIndex = GetPlayerIndex();

    //            MoveNumber(1, playerIndex, playerIndex + 1);
    //            PrintArray();
    //        }

    //        if (Input.GetKeyDown(KeyCode.LeftArrow))
    //        {
    //            // 移動処理
    //            int playerIndex = GetPlayerIndex();

    //            MoveNumber(1, playerIndex, playerIndex - 1);
    //            PrintArray();
    //        }
    //    }
}
