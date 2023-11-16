using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{
    [SerializeField]
    Tilemap FloorMap;
    [SerializeField]
    Tilemap WallMap;

    public int LevelWidth;
    public int LevelHeight;

    public TileType[] TileTypes;

    public int[] TileIDS;

    public void Refresh()
    {
        for(int i = 0; i < LevelWidth * LevelHeight; i++)
        {
            Vector3Int Pos = new Vector3Int(i % LevelWidth, -i / LevelWidth);

            if (TileTypes[TileIDS[i]].IsFloor)
            {
                FloorMap.SetTile(Pos, TileTypes[TileIDS[i]].Tile);
                WallMap.SetTile(Pos, null);
            }
            else
            {
                WallMap.SetTile(Pos, TileTypes[TileIDS[i]].Tile);
                FloorMap.SetTile(Pos, null);
            }
        }
    }


    void Start()
    {
        TileIDS = new int[LevelWidth * LevelHeight];

        for(int i = 0; i < LevelWidth * LevelHeight; i++)
        {
            if(i - LevelWidth < 0 ||
                i % LevelWidth == 0 ||
                i % LevelWidth == LevelWidth - 1 ||
                i >= LevelWidth * LevelHeight - LevelWidth)
            {
                TileIDS[i] = 1;
            }
            else
            {
                
            }
        }
        Refresh();
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class TileType
{
    public bool IsFloor;
    public RuleTile Tile;
}