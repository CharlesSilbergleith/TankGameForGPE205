using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private MeshRenderer cube;

    void Awake()
    {
        cube = GetComponent<MeshRenderer>();

        if (cube != null)
        {
            cube.enabled = false;
        }
    }

    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager.instance is null");
            return;
        }

        if (!GameManager.instance.p1Spawned)
        {
            GameManager.instance.SpawnPlayer();
        }
    }
}