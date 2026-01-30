using UnityEngine;

public class PawnTank : Pawn
{
    
    private float timer;
    public float reloadTime;
    public override void Start()
    {
        // Save my tank in my GameManager
        GameManager.instance.tanks.Add(this);
      
        // Do what all pawns do
        base.Start();
    }

    public void OnDestroy()
    {
        // Remove my tank from the GameManager list
        GameManager.instance.tanks.Remove(this);
    }

    public override void Move(Vector3 directionToMove)
    {
        // Tell the mover to move
        mover.Move(directionToMove, moveSpeed);
    }

    public override void Rotate(Vector3 directionToRotate)
    {
        // Tell the mover to rotate
        mover.Rotate(directionToRotate, turnSpeed);
    }

    public override void Shoot()
    {

        if (timer >= reloadTime)
        {
           
            shooter.Shoot();
            timer = 0;
        }
        
    }
    void Update() {
        timer += Time.deltaTime;
    }
}
