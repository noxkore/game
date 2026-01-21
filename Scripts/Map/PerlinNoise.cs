using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width;
    public int height;
    public int intensity;
    public int seed;

    private float offsetX;
    private float offsetY;

    void GenerateSeed()
    {
        System.Random prng = new System.Random(seed);
        offsetX = prng.Next(-100000, 100000);
        offsetY = prng.Next(-100000, 100000);
    }


    Texture2D CreateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = GeneratePerlinColor(x, y);
                texture.SetPixel(x , y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color GeneratePerlinColor(int x, int y)
    {
        float perlinX = ((float)x / width * intensity) + offsetX;
        float perlinY = ((float)y / height * intensity) + offsetY;

        float value = Mathf.PerlinNoise(perlinX, perlinY);
        return new Color(value, value, value);
    }

    private void ChangeTexture()
    {
        GenerateSeed();
        Texture2D texture = CreateTexture();

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f)
        );

        GetComponent<SpriteRenderer>().sprite = sprite;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ChangeTexture();
        }
    }
}