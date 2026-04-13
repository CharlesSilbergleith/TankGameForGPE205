using UnityEngine;

public class PawnAi : Pawn
{
    private float nextShootTime;
    public float firerate;
    public override void Start()
    {
        // Save  tank in  GameManager
        GameManager.instance.AITanks.Add(this);

        // Do what all pawns do
        base.Start();
    }
    public override void OnDestroy()
    {
        // Remove my tank from the GameManager list
        GameManager.instance.AITanks.Remove(this);
    }


    public override void Move(Vector3 directionToMove) {
        mover.Move(directionToMove, moveSpeed);



    }
    public override void Rotate(Vector3 directionToRotate) {
        mover.Rotate(directionToRotate, turnSpeed);

    }
    public override void RotateTowards(Vector3 postition, float turnSpeed) {
        mover.RotateTowards(postition, turnSpeed);
    }
    public override void Shoot() {

        if (Time.time >= nextShootTime)
        {

            shooter.Shoot();
            nextShootTime = Time.time + 1 / firerate;
        }
    }
}
