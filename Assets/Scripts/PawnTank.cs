using UnityEngine;

public class PawnTank : Pawn
{
    
    private float nextShootTime;
    public float firerate;
    public NoiseMaker noiseMaker;
    public float noiceDistance;
   
    

    public override void Start()
    {
        // Save my tank in my GameManager
        GameManager.instance.tanks.Add(this);
        noiseMaker = GetComponent<NoiseMaker>();
        // Do what all pawns do
        base.Start();
    }

    public override void OnDestroy()
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

        if (Time.time>=nextShootTime)
        {
            shooter.Shoot();
           nextShootTime = Time.time+1/firerate;
        }
        
    }
    void Update() {
        noiseMaker.makeNoise(0f);
    }

    public override void RotateTowards(Vector3 postition,float turnSpeed) {
        mover.RotateTowards(postition, turnSpeed);
    
    }

    public override void MoveSound() {
        noiseMaker.makeNoise(noiceDistance);
    }
   
}
