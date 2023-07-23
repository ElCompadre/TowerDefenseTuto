using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField]
    private int mapWidht;
    [SerializeField]
    private int mapHeight;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    private bool reachX = false;
    private bool reachY = false;

    private GameObject currentTile;
    private int currentTileIndex;
    private int nextTileIndex;

    public Color pathColor = Color.grey;
    public Color startColor = Color.green;
    public Color endColor = Color.red;

    private void Start()
    {
        generateMap();
    }

    private List<GameObject> getTopEdgeTiles()
    {
        var edgeTiles = new List<GameObject>();

        for (int i = mapWidht * (mapHeight - 1); i < mapWidht * mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private List<GameObject> getBottomEdgeTiles()
    {
        var edgeTiles = new List<GameObject>();

        for (int i = 0; i < mapWidht; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private void moveUp()
    {
        pathTiles.Add(currentTile);
        currentTileIndex = mapTiles.IndexOf(currentTile);
        nextTileIndex = currentTileIndex + mapWidht;
        currentTile = mapTiles[nextTileIndex];
    }
    private void moveDown() 
    {
        pathTiles.Add(currentTile);
        currentTileIndex = mapTiles.IndexOf(currentTile);
        nextTileIndex = currentTileIndex - mapWidht;
        currentTile = mapTiles[nextTileIndex];
    }
    private void moveLeft() 
    {
        pathTiles.Add(currentTile);
        currentTileIndex = mapTiles.IndexOf(currentTile);
        nextTileIndex = currentTileIndex - 1;
        currentTile = mapTiles[nextTileIndex];
    }
    private void moveRight() 
    {
        pathTiles.Add(currentTile);
        currentTileIndex = mapTiles.IndexOf(currentTile);
        nextTileIndex = currentTileIndex + 1;
        currentTile = mapTiles[nextTileIndex];
    }

    private void generateMap()
    {
        for (int y = 0; y < mapHeight; y++) 
        {
            for (int x = 0; x < mapWidht; x++)
            {
                GameObject newTile = Instantiate(mapTile);

                newTile.transform.position = new Vector2(x, y);

                mapTiles.Add(newTile);
            }
        }

        var topEdgeTiles = getTopEdgeTiles();
        var bottomEdgeTiles = getBottomEdgeTiles();

        int randStartTile = Random.Range(0, mapWidht);
        int randEndTile = Random.Range(0, mapWidht);

        startTile = topEdgeTiles[randStartTile];
        endTile = bottomEdgeTiles[randEndTile];

        currentTile = startTile;

        moveDown();

        int loopCount = 0;

        while (!reachX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                reachX = true;
            }
            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            } else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            } else
            {
                reachX = true;
            }
        }

        loopCount = 0;
        while (!reachY)
        {
            loopCount++;
            if (loopCount > 100)
            {
                reachY = true;
            }
            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else
            {
                reachY = true;
            }
        }

        pathTiles.Add(endTile);

        foreach (var pathTile in pathTiles)
        {
           pathTile.GetComponent<SpriteRenderer>().color = pathColor;
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }
}
