using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    public enum TT
    {
        E,
        W,
        B,
        NULL,
        START,
        ENEMY
    }

    public readonly TT[,] _board =
    {
        {TT.NULL, TT.NULL, TT.NULL, TT.NULL, TT.NULL},
        {TT.NULL, TT.E, TT.START, TT.W, TT.NULL},
        {TT.NULL, TT.E, TT.E, TT.E, TT.NULL},
        {TT.B, TT.E, TT.E, TT.E, TT.B},
        {TT.NULL, TT.W, TT.E, TT.E, TT.NULL},
        {TT.NULL, TT.W, TT.ENEMY, TT.E, TT.NULL},
        {TT.NULL, TT.NULL, TT.B, TT.NULL, TT.NULL},
    };

    private int _height;
    private int _width;

    public GameObject waterPrefab;
    public GameObject earthPrefab;
    public GameObject bridgePrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public void buildBoard()
    {
        _height = _board.GetLength(0);
        _width = _board.GetLength(1);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var type = _board[y, x];

                switch (type)
                {
                    case TT.E:
                        PlaceTile(y, x, earthPrefab);
                        break;
                    case TT.W:
                        PlaceTile(y, x, waterPrefab);
                        break;
                    case TT.B:
                        PlaceTile(y, x, bridgePrefab);
                        break;
                    case TT.NULL:
                        break;
                    case TT.START:
                        PlaceTile(y, x, earthPrefab);
                        PlacePlayer(y, x, playerPrefab);
                        break;
                    case TT.ENEMY:
                        PlaceTile(y, x, earthPrefab);
                        PlacePlayer(y, x, enemyPrefab);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    private void PlaceTile(int y, int x, GameObject prefab)
    {
        var initialScale = prefab.transform.localScale;
        var initialPosition = prefab.transform.position;

        var r = initialScale.z;
        var u = initialScale.x;

        var globalZ = y * r + r / 2f;
        var globalX = x * u + u / 2f;

        var newBlock =Instantiate(prefab, initialPosition + new Vector3(globalX, 0, globalZ), Quaternion.identity);
        newBlock.transform.SetParent( this.transform);
    }

    private  void PlacePlayer(int y, int x, GameObject prefab)
    {
        var initialScale = earthPrefab.transform.localScale;
        var initialPosition = prefab.transform.position;

        var r = initialScale.z;
        var u = initialScale.x;
        var up = initialScale.y;

        var globalZ = y * r + r / 2f;
        var globalX = x * u + u / 2f;

        var player = Instantiate(prefab, initialPosition + new Vector3(globalX, 0, globalZ), prefab.transform.localRotation);
        player.transform.SetParent(this.transform);
    }
}