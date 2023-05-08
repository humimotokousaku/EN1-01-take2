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

        // 変更。二重for分で二次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
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
            {
                if (field[y, x] == null){ continue; }
                if (field[y, x].tag == "Player") { return new Vector2Int(x, y); }
            }
        }
        return new Vector2Int(-1, -1);
    }

    // 
    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.y < 0 || moveTo.x >= field.GetLength(0))
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (moveTo.y < 0 || moveTo.x >= field.GetLength(1))
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }

        if (moveTo.x < 0 || moveTo.y >= field.GetLength(1))
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (moveTo.x < 0 || moveTo.y >= field.GetLength(0))
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velosity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velosity);
            if (!success)
            {
                return false;
            }
        }

        // GameObjectの座標（position）を移動させてからインデックスの入れ替え
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerIndex = GetPlayerIndex();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x + 1, playerIndex.y));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x - 1, playerIndex.y));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x, playerIndex.y - 1));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x, playerIndex.y + 1));
        }
    }
}
