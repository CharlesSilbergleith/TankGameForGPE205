using Unity.VisualScripting;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [HideInInspector] public Pawn pawn;

    public virtual void Update()
    {
        MakeDecisions();
    }
    public abstract void MakeDecisions();
    public void posses(Pawn pawnToPossess) {
        pawnToPossess.controller = this;
        this.pawn = pawnToPossess;
    }
    public void unpossess() {
        pawn.controller = null;
        
    }
}
