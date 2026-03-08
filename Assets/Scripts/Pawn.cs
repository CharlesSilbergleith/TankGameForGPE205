using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Mover mover;
    public Shooter shooter;
    [HideInInspector] public Health health;
    [HideInInspector] public Controller controller;
    public abstract void Move(Vector3 directionToMove);
    public abstract void Rotate(Vector3 directionToRotate);
    public abstract void RotateTowards(Vector3 postition, float turnSpeed);
    public abstract void Shoot();
    public abstract void OnDestroy();
    public virtual void MoveSound() { 
    
    }

    public float moveSpeed;
    public float turnSpeed;


    public Controller GetController () { return controller; }

    public virtual void Start()
    {
        // Get the mover component
        mover = GetComponent<Mover>();
        shooter = GetComponentInChildren<Shooter>();

    }
}
