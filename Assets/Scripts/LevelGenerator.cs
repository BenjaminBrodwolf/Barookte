using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject earthPrefab;

    public Texture2D mapTexture;
    public PixelToObject[] pixelColorMappings;
    private Color pixelColor;
    

    public void GenerateLevel()
    {
        // Debug.Log("GenerateLevel");

        // Scan whole Texture and get positions of objects
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
        // Debug.Log("GenerateObject");

        // Read pixel color
        pixelColor = mapTexture.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            // Do nothing
            return;
        }


        foreach (PixelToObject pixelColorMapping in pixelColorMappings)
        {
            // Scan pixelColorMappings Array for matching color maping
            if (pixelColorMapping.pixelColor.Equals(pixelColor))
            {
                if (pixelColorMapping.prefab.name.Equals("Player") ||
                    pixelColorMapping.prefab.name.Equals("Enemy"))
                {
                    PlaceTile(y, x, earthPrefab);
                    PlacePlayer(y, x, pixelColorMapping.prefab);
                }
                else
                {
                    PlaceTile(y, x, pixelColorMapping.prefab);

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


        var newBlock = Instantiate(prefab, initialPosition + new Vector3(globalX, 0, globalZ), Quaternion.identity);
        newBlock.transform.SetParent(this.transform);


        // Debug.Log(prefab.GetType());
        Debug.Log(prefab.name);
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