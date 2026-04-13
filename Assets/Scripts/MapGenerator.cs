using UnityEngine;
using System.Collections.Generic;
using System;

public enum RandomType { Random, Seeded, MapOfTheDay };

public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public int seed = 27;
    public RandomType randomType;

    [Header("Tile Data")]
    public List<Tile> availableTiles;
    public float TileWidth;
    public float TileLength;
    public int mapCols;
    public int mapRows;
    public Tile[,] grid;

    [Header("Special Tiles")]
    public Tile playerSpawnTilePrefab;
    public Tile enemySpawnTilePrefab;

    private bool hasPlayerSpawnerYet;
    private bool hasEnemySpawnerYet;

    

    public void StartMapGen()
    {
        InitializeRandom();
        GenMap();
    }

    public void InitializeRandom()
    {
        if (randomType == RandomType.Seeded)
        {
            UnityEngine.Random.InitState(seed);
        }
        else if (randomType == RandomType.Random)
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        }
        else if (randomType == RandomType.MapOfTheDay)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
        }
    }

    public int DateToInt(DateTime date)
    {
        return date.Year + date.Month + date.Day;
    }

    public void GenMap()
    {
        grid = new Tile[mapCols, mapRows];

        for (int currentRow = 0; currentRow < mapRows; currentRow++)
        {
            for (int currentCol = 0; currentCol < mapCols; currentCol++)
            {
                Tile tempTile = Instantiate(GetRandomTile());

                Vector3 pos = Vector3.zero;
                pos.z = currentRow * TileWidth;
                pos.x = currentCol * TileLength;
                tempTile.transform.position = pos;

                tempTile.name = "tile(" + currentCol + "," + currentRow + ")";

                WallShutOff(tempTile, currentCol, currentRow);

                grid[currentCol, currentRow] = tempTile;
                Debug.Log("made " + currentCol + ", " + currentRow);
            }
        }

        EnsurePlayerSpawn();
        EnsureEnemySpawn();
    }

    public Tile GetRandomTile()
    {
        List<Tile> validTiles = new List<Tile>();

        foreach (Tile tile in availableTiles)
        {
            if (tile == playerSpawnTilePrefab && hasPlayerSpawnerYet)
                continue;

            if (tile == enemySpawnTilePrefab && hasEnemySpawnerYet)
                continue;

            validTiles.Add(tile);
        }

        int index = UnityEngine.Random.Range(0, validTiles.Count);
        Tile chosen = validTiles[index];

        if (chosen == playerSpawnTilePrefab)
            hasPlayerSpawnerYet = true;

        if (chosen == enemySpawnTilePrefab)
            hasEnemySpawnerYet = true;

        return chosen;
    }

    void EnsurePlayerSpawn()
    {
        if (hasPlayerSpawnerYet)
            return;

        int randCol = UnityEngine.Random.Range(0, mapCols);
        int randRow = UnityEngine.Random.Range(0, mapRows);

        ReplaceTile(randCol, randRow, playerSpawnTilePrefab);

        hasPlayerSpawnerYet = true;

        Debug.Log("Forced PlayerSpawn at: (" + randCol + ", " + randRow + ")");
    }

    void EnsureEnemySpawn()
    {
        if (hasEnemySpawnerYet)
            return;

        int randCol;
        int randRow;

        // make sure we don’t overwrite player spawn
        do
        {
            randCol = UnityEngine.Random.Range(0, mapCols);
            randRow = UnityEngine.Random.Range(0, mapRows);
        }
        while (grid[randCol, randRow] == playerSpawnTilePrefab);

        ReplaceTile(randCol, randRow, enemySpawnTilePrefab);

        hasEnemySpawnerYet = true;

        Debug.Log("Forced EnemySpawn at: (" + randCol + ", " + randRow + ")");
    }

    void ReplaceTile(int col, int row, Tile prefab)
    {
        Tile oldTile = grid[col, row];

        Vector3 pos = oldTile.transform.position;
        Quaternion rot = oldTile.transform.rotation;

        Destroy(oldTile.gameObject);

        Tile newTile = Instantiate(prefab, pos, rot);

        newTile.name = "tile(" + col + "," + row + ")";

        WallShutOff(newTile, col, row);

        grid[col, row] = newTile;
    }

    void WallShutOff(Tile tile, int currentCol, int currentRow)
    {
        if (currentRow == 0)
            tile.doorNorth.SetActive(false);
        else if (currentRow == mapRows - 1)
            tile.doorSouth.SetActive(false);
        else
        {
            tile.doorNorth.SetActive(false);
            tile.doorSouth.SetActive(false);
        }

        if (currentCol == mapCols - 1)
            tile.doorWest.SetActive(false);
        else if (currentCol == 0)
            tile.doorEast.SetActive(false);
        else
        {
            tile.doorEast.SetActive(false);
            tile.doorWest.SetActive(false);
        }
    }

    public void resetMap()
    {
        if (grid == null) return;

        for (int r = 0; r < mapRows; r++)
        {
            for (int c = 0; c < mapCols; c++)
            {
                if (grid[c, r] != null)
                {
                    Destroy(grid[c, r].gameObject);
                    grid[c, r] = null;
                }
            }
        }

        hasPlayerSpawnerYet = false;
        hasEnemySpawnerYet = false;
    }
}