using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    [Header("Up-to-date Lists")]
    public List<Pawn> tanks;
    public List<Controller> players;

    void Awake()
    {
        // Create our singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Create our up to date list objects (not just memory locations, but actual lists)
        tanks = new List<Pawn>();
        players = new List<Controller>();
    }

    void Start()
    {
        // Start the Game!
        StartGame();

    }

    public void StartGame()
    {
        // Do everything we need to start the game

        // Spawn the player
        SpawnPlayer();

    }

    public void SpawnPlayer()
    {
        // Spawn a tank pawn (and store it in tempTankPawn)
        Pawn tempTankPawn = SpawnTank(playerPawnPrefab);

        // Spawn a player controller (and store it in players)
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        // Have the player possess the pawn
        tempPlayerController.Possess(tempTankPawn);
    }

    public Pawn SpawnTank(GameObject prefab)
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();
    } 
    
    public Controller SpawnPlayerController (GameObject prefab)
    {
        GameObject tempPlayer = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPlayer.GetComponent<Controller>();
    }

}
