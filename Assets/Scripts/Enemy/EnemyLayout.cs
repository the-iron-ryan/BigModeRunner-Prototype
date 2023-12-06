using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyLayout : MonoBehaviour
{
    public Grid Grid;
    public Tilemap layer0;
    public Tilemap layer1;
    public Tilemap layer2;

    public BaseEnemyCharacter this[int x, int y, int z]
    {
        get
        {
            // First grab the tilemap that the enemy is on
            Tilemap currentTilemap = null;
            if (y == 0)
            {
                currentTilemap = layer0;
            }
            else if (y == 1)
            {
                currentTilemap = layer1;
            }
            else if (y == 2)
            {
                currentTilemap = layer2;
            }
            else
            {
                return null;
            }

            // Next grab the tile at the position - if it exists
            Tile tile = currentTilemap.GetTile<Tile>(new Vector3Int(x, 0, z));
            if (tile != null && tile.gameObject != null)
            {
                return tile.gameObject.GetComponent<BaseEnemyCharacter>();
            }
            else
            {
                return null;
            }
        }
        set
        {
            // First grab the tilemap that the enemy is on
            Tilemap currentTilemap = null;
            if (y == 0)
            {
                currentTilemap = layer0;
            }
            else if (y == 1)
            {
                currentTilemap = layer1;
            }
            else if (y == 2)
            {
                currentTilemap = layer2;
            }
            else
            {
                return;
            }
            
            Vector3Int tilePos = new Vector3Int(x, 0, z);
            Tile tile = currentTilemap.GetTile<Tile>(tilePos);
            tile.gameObject = value.gameObject;
            currentTilemap.SetTile(tilePos, tile);
        }
    }
}
