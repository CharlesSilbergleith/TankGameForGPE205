using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void Move(Vector2 moveDircetion);
    public abstract void Rotate(Vector2 rotateDircetion);
}
