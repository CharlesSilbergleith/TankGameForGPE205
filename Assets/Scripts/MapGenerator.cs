using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
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
    public Tile playerSpawnTilePrefab;
    private bool hasSpawnerYet;


    void Awake() {
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
        else if(randomType == RandomType.MapOfTheDay)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
        }
        
    }
    public int DateToInt(DateTime date) { 
        return date.Year + date.Month+ date.Day;
    }
    public void GenMap() { 
        //nested forloops
        //create grid array 
        grid = new Tile[mapCols, mapRows];

        //itrate through
        // create map tile
        // put in pos
        //open the correct doors 
        // same it to the grid

        for (int currentRow = 0; currentRow < mapRows ; currentRow++) {
            for (int currentCol = 0; currentCol <mapCols ; currentCol++) { 
                Tile tempTile = Instantiate<Tile> (GetRandomTile()) as Tile;

                Vector3 CorrectPos = Vector3.zero;
                CorrectPos.z = currentRow * TileWidth;
                CorrectPos.x = currentCol * TileLength ;
                tempTile.transform.position = CorrectPos;
                //name the tile
                tempTile.name = "tile(" + currentCol + "," + currentRow + ")";
                WallShutOff(tempTile, currentCol, currentRow);

                //save grid
                grid[currentCol ,currentRow ] = tempTile;



            }//end col loop
        
        }// end row loop
        EnsurePlayerSpawn();
    }

    public Tile GetRandomTile()
    {
        // If we still need a spawn tile, allow it
        if (!hasSpawnerYet)
        {
            int index = UnityEngine.Random.Range(0, availableTiles.Count);
            Tile chosen = availableTiles[index];

            if (chosen.tag == "PlayerSpawn")
            {
                hasSpawnerYet = true;
                return chosen;
            }

            return chosen;
        }
        else
        {
            // Spawn already used  avoid PlayerSpawn tiles
            List<Tile> nonSpawnTiles = availableTiles.FindAll(t => t.tag != "PlayerSpawn");

            int index = UnityEngine.Random.Range(0, nonSpawnTiles.Count);
            return nonSpawnTiles[index];
        }
    }
    void EnsurePlayerSpawn()
    {
        // If we already have one, do nothing
        if (hasSpawnerYet)
            return;

        // Pick random grid position
        int randCol = UnityEngine.Random.Range(0, mapCols);
        int randRow = UnityEngine.Random.Range(0, mapRows);

        Tile oldTile = grid[randCol, randRow];

        // Store position & rotation
        Vector3 pos = oldTile.transform.position;
        Quaternion rot = oldTile.transform.rotation;

        // Destroy old tile
        Destroy(oldTile.gameObject);

        // Spawn PlayerSpawn tile
        Tile newTile = Instantiate(playerSpawnTilePrefab, pos, rot);
        WallShutOff(newTile, randCol, randRow);
        // Rename to match your system
        newTile.name = "tile(" + randCol + "," + randRow + ")";

        // Replace in grid
        grid[randCol, randRow] = newTile;

        hasSpawnerYet = true;

        Debug.Log("Forced PlayerSpawn tile at: (" + randCol + ", " + randRow + ")");
    }
    void WallShutOff(Tile tile, int currentCol, int currentRow)
    {
        if (currentRow == 0)
        {
            tile.doorNorth.SetActive(false);
        }
        else if (currentRow == mapRows - 1)
        {
            tile.doorSouth.SetActive(false);
        }
        else
        {
            tile.doorNorth.SetActive(false);
            tile.doorSouth.SetActive(false);
        }

        if (currentCol == mapCols - 1)
        {
            tile.doorWest.SetActive(false);
        }
        else if (currentCol == 0)
        {
            tile.doorEast.SetActive(false);
        }
        else
        {
            tile.doorEast.SetActive(false);
            tile.doorWest.SetActive(false);
        }
    }

}
