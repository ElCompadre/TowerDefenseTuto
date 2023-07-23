using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField]
    private int _mapWidht;
    [SerializeField]
    private int _mapHeight;

    private List<GameObject> _mapTiles = new List<GameObject>();
    private List<GameObject> _pathTiles = new List<GameObject>();

    private void Start()
    {
        generateMap();
    }

    private void generateMap()
    {
        for (int y = 0; y < _mapHeight; y++) 
        {
            for (int x = 0; x < _mapWidht; x++)
            {
                GameObject newTile = Instantiate(mapTile);

                _mapTiles.Add(newTile);

                newTile.transform.position = new Vector2(x, y);
            }
        }
    }
}
