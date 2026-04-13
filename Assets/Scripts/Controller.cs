using Unity.VisualScripting;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
     public Pawn pawn;
    [Header("Score")]
    public float Score = 0;
    [HideInInspector] public bool isPlayer2;
     public int Lives = 3;

    public virtual void Start() { 
        
    }
    public virtual void Update()
    {
        if (GameManager.instance.GameplayStateObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }

        MakeDecisions();
    }

    public abstract void MakeDecisions();



    public void Possess(Pawn pawnToPossess)
    {
        pawnToPossess.controller = this;
        this.pawn = pawnToPossess;
    }

    public void Unpossess ()
    {
        pawn.controller = null;
        pawn = null;
    }
    public void addToScore(float score)
    {
        Score += score;
    }
   
}
