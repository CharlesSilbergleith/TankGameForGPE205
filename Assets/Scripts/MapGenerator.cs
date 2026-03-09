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

    void Start() {
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

                if (currentRow == 0) { 
                    tempTile.doorNorth.SetActive(false);
                }
                else if(currentRow == mapRows - 1)
                {
                    tempTile.doorSouth.SetActive(false);
                }
                else {
                    tempTile.doorNorth.SetActive(false);
                    tempTile.doorSouth.SetActive(false);
                }



               if(currentCol == mapCols - 1)
                            {
                                tempTile.doorWest.SetActive(false);
                            }
                else if (currentCol == 0)
                {
                    tempTile.doorEast.SetActive(false);
                }
              
                else {
                    tempTile.doorEast.SetActive(false);
                    tempTile.doorWest.SetActive(false);

                }
                    //save grid
                    grid[currentCol ,currentRow ] = tempTile;



            }//end col loop
        
        }// end row loop
    }

    public Tile GetRandomTile() {
        int tileNumber = UnityEngine.Random.Range(0, availableTiles.Count);
        return availableTiles [tileNumber];
    }




}
