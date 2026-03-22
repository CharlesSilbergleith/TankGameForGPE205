using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject playerController;
    public MapGenerator mapGen;
    private MeshRenderer Cube;
    private Pawn playerobj;
    private Controller playerControllerobj;
    void Awake()
    {
        Cube = GetComponent<MeshRenderer>();
        Cube.enabled=false;
        mapGen = GetComponent<MapGenerator>();
        player = GameManager.instance.player;
        playerController = GameManager.instance.playerController;
        
    }
    void Start() {
    SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        // Spawn a tank pawn (and store it in tempTankPawn)
        Pawn tempTankPawn = SpawnTank(player);

        // Spawn a player controller (and store it in players)
        Controller tempPlayerController = SpawnPlayerController(playerController);

        // Have the player possess the pawn
        tempPlayerController.Possess(tempTankPawn);
    }

    public Pawn SpawnTank(GameObject prefab)
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab,transform.position, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();
    }

    public Controller SpawnPlayerController(GameObject prefab)
    {
        GameObject tempPlayer = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPlayer.GetComponent<Controller>();
    }

}
