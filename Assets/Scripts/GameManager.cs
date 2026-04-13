using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InputActionAsset inputActions;

    public static GameManager instance;
    [Header("Prefabs")]
    public GameObject playerController;
    public GameObject player;
    [Header("Up-to-date Lists")]
    public List<Pawn> tanks;
    public List<Pawn> AITanks;
    public List<Controller> AITanksLeft;
    public List<Controller> players;
    public PawnTank player1;
    public PawnTank player2;
    public Controller player1Controller;
    public Controller player2Controller;
    [Header("Game States")]
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
    [Header("map Gen")]
    public MapGenerator mapGen;
    [HideInInspector] public bool isCoop=false;
    [HideInInspector] public bool p1Spawned = false;
    [HideInInspector] public bool p2Spawned = false;

    public Vector3 playerSpawner;
    public Vector3 player2Spawner;
    [Header("Volume")]
    public Slider slider;
    public float vol;
    // [Header("Score")]
    //float HighScore=0;

   

    public void SliderVol()
    {
        
    }

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
       // StartGame();
       DeactivateAllStates();
        ActiveTiteScreen();
        


    }


    void Update() {
        if (AITanksLeft.Count == 0 && GameplayStateObject.activeSelf == true) {
        }
        vol = slider.value;
    
    }

    public void StartGame()
    {
        // Do everything we need to start the game

        // Spawn the player
        //SpawnPlayer();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Score=0;
        }
    }

    public void SpawnPlayer()
    {
        if (p1Spawned) return;
        if (playerSpawner == null) return;

        GameObject tempTankObject = Instantiate(player, playerSpawner, Quaternion.identity);
        Pawn tempTankPawn = tempTankObject.GetComponent<Pawn>();

        GameObject tempPlayerObject = Instantiate(playerController, playerSpawner, Quaternion.identity);
        Controller tempPlayerController = tempPlayerObject.GetComponent<Controller>();

        tempPlayerController.Possess(tempTankPawn);
        tempTankPawn.Possess(tempPlayerController);

        p1Spawned = true;
    }
    public void SpawnPlayer2()
    {
        if (p2Spawned) return;
        if (player2Spawner == null) return;

        GameObject tempTankObject = Instantiate(player, player2Spawner, Quaternion.identity);
        Pawn tempTankPawn = tempTankObject.GetComponent<Pawn>();

        GameObject tempPlayerObject = Instantiate(playerController, player2Spawner, Quaternion.identity);
        Controller tempPlayerController = tempPlayerObject.GetComponent<Controller>();

        tempPlayerController.Possess(tempTankPawn);
        tempPlayerController.isPlayer2 = true;
        tempTankPawn.Possess(tempPlayerController);

        p2Spawned = true;
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


    // Active state objects

    private void DeactivateAllStates() {
        if (GameplayStateObject.activeSelf == true) {
            mapGen.resetMap();
        }
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
        
    }
    public void ActiveTiteScreen() {
        DeactivateAllStates();
        TitleScreenStateObject.SetActive(true);
    }
    public void ActiveMainMenuState()
    {
        DeactivateAllStates();
        MainMenuStateObject.SetActive(true);
    }
    public void ActiveOptionsScreenState()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }
    public void ActiveCreditsScreenState()
    {
        DeactivateAllStates();
        CreditsScreenStateObject.SetActive(true);
    }
    public void ActiveGameplayState()
    {
        DeactivateAllStates();
        GameplayStateObject.SetActive(true);

        p1Spawned = false;
        p2Spawned = false;
        playerSpawner = new Vector3(0,0,0);
        player2Spawner = new Vector3(0, 0, 0);

        mapGen.StartMapGen();
    }
    public void ActiveGameOverScreenState()
    {
        DeactivateAllStates();
        GameOverScreenStateObject.SetActive(true);
    }
    
  

    
   

}
