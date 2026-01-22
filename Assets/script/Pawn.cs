using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;

    public abstract void Move(Vector2 move);

    public abstract void Rotate(Vector2 rotate);
    


}
