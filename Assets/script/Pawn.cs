using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector]public Controller controller;
    [SerializeField] protected Mover mover;
    public float moveSpeed;
    public float turnSpeed;
    public abstract void Move(Vector2 move);

    public abstract void Rotate(Vector2 rotate);

    public void Start() {
        mover = GetComponent<Mover>();
    }

}
