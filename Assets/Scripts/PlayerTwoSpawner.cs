using UnityEngine;

public class PlayerTwoSpawner : MonoBehaviour
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

        if (GameManager.instance.isCoop && !GameManager.instance.p2Spawned)
        {
            GameManager.instance.SpawnPlayer2();
        }
    }
}