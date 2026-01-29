using UnityEngine;

public class MoverTank : Mover
{
    [SerializeField] private Pawn pawn;
    public void Start() { 
        pawn = GetComponent<Pawn>();
    }
    public override void Move(Vector2 moveDircetion)
    {
        transform.position += new Vector3(moveDircetion.x,0,moveDircetion.y) * (pawn.moveSpeed * Time.deltaTime);
    }

    public override void Rotate(Vector2 rotateDircetion)
    {
        float rotationAmount = rotateDircetion.x;
        rotationAmount *= (pawn.turnSpeed);
        rotationAmount *= Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
}
