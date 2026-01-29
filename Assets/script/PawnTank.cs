using UnityEngine;
using UnityEngine.EventSystems;

public class PawnTank : Pawn
{
   
    public override void Move(Vector2 moveDirection)
    {
        mover.Move(moveDirection);
    }

   

    public override void Rotate(Vector2 rotateDirection)
    {
        mover.Rotate(rotateDirection);
    }
}
