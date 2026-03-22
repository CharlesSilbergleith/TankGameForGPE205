using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    
    public Pawn Enemy;
    public GameObject EnemyController;
    public MapGenerator mapGen;
    private MeshRenderer Cube;

    void Start() { 
        Cube = GetComponent<MeshRenderer> ();
        Cube.enabled = false;
        Enemy = GetRandomEnemy ();
        SpawnEnemy (Enemy);
    }
   
    public Pawn GetRandomEnemy() {
        int randomEnemy = Random.Range(0, GameManager.instance.AITanks.Count);
        return GameManager.instance.AITanks[randomEnemy];
    }

    public void SpawnEnemy(Pawn Enemy) {
        Instantiate(Enemy, transform.position, transform.rotation);
    }
}

