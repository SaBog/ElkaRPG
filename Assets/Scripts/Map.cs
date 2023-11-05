using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int[,] interior;
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    TileBase[] tilelib;

    public void GenerateDungeon() //генерация подземелья
    {
        interior = new int[7,7];
        int length = interior.GetLength(0);
        int width = interior.GetLength(1);
        for (int i=0; i< length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                interior[i, j] = 0;
                if (i == 0 || j==0 || i==length-1 || j == width - 1)
                {
                    interior[i, j] = 1;
                }
            }
        }
    }

    private void VisualizeDungeon() //отрисовка подземелья
    {
        tilemap.SetTiles(this.ArrayToPositions(), this.ArrayToTilemap());
    }

    private TileBase[] ArrayToTilemap() //привести массив в тайлы
    {
        int length = interior.GetLength(0);
        int width = interior.GetLength(1);
        TileBase[] result = new TileBase[length * width];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                result[j + i * length] = tilelib[interior[i,j]];
            }
        }
        return result;//
    }
    private Vector3Int[] ArrayToPositions() //привести массив в тайлы
    {
        int length = interior.GetLength(0);
        int width = interior.GetLength(1);
        Vector3Int[] result = new Vector3Int[length * width];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                result[j + i * length] = new Vector3Int(i, j);
            }
        }
        return result;//
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
        VisualizeDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
