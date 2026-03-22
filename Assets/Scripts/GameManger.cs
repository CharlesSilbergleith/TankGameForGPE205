using UnityEngine;
using System.Collections.Generic;

public class GameManger : MonoBehaviour
{
    public static GameManger instatnce;
    public Controller palyerController;
    public Pawn pawn;
    public List<Pawn> tanks;
    public List<Controller> players;

    void Awake()
    {
        if (instatnce == null)
        {
            instatnce = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
