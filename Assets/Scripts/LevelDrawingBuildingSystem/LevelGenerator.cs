using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject earthPrefab;

    public Texture2D mapTexture;
    public PixelToObject[] pixelColorMappings;
    private Color pixelColor;

    private int Earth = 8;
    private int OnEarth = 9;
    private int MoveAbleItem = 10;

    public void GenerateLevel()
    {
        for (int i = 0; i < mapTexture.width; i++)
        {
            for (int j = 0; j < mapTexture.height; j++)
            {
                GenerateObject(i, j);
            }
        }
    }

    void GenerateObject(int x, int y)
    {
        pixelColor = mapTexture.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (PixelToObject pixelColorMapping in pixelColorMappings)
        {
            if (colorEquals(pixelColorMapping.pixelColor, pixelColor))
            {
                if (pixelColorMapping.prefab.layer == OnEarth || pixelColorMapping.prefab.layer == MoveAbleItem)
                {
                    PlaceTile(y, x, earthPrefab);
                    PlacePlayer(y, x, pixelColorMapping.prefab);
                }
                else if (pixelColorMapping.prefab.layer == Earth)
                {
                    PlaceTile(y, x, pixelColorMapping.prefab);
                }
            }
        }
    }

    private bool colorEquals(Color c0, Color c1)
    {
        var eps = 0.001;

        return Math.Abs(c0.a - c1.a) < eps
               && Math.Abs(c0.b - c1.b) < eps
               && Math.Abs(c0.g - c1.g) < eps 
               && Math.Abs(c0.r - c1.r) < eps;
    }


    private void PlaceTile(int y, int x, GameObject prefab)
    {
        var initialScale = prefab.transform.localScale;
        var initialPosition = prefab.transform.position;

        var r = initialScale.z;
        var u = initialScale.x;

        var globalZ = y * r + r / 2f;
        var globalX = x * u + u / 2f;


        var newBlock = Instantiate(prefab, initialPosition + new Vector3(globalX, 0, globalZ), Quaternion.identity);
        newBlock.transform.SetParent(this.transform);
    }

    private void PlacePlayer(int y, int x, GameObject prefab)
    {
        var initialScale = earthPrefab.transform.localScale;
        var initialPosition = prefab.transform.position;

        var r = initialScale.z;
        var u = initialScale.x;
        var up = initialScale.y;

        var globalZ = y * r + r / 2f;
        var globalX = x * u + u / 2f;

        var player = Instantiate(prefab, initialPosition + new Vector3(globalX, 0, globalZ),
            prefab.transform.localRotation);
        player.transform.SetParent(this.transform);
    }
}