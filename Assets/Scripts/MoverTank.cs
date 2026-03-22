using UnityEngine;

public class MoverTank : Mover
{
    private Rigidbody rb;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector2 moveDirection, float moveSpeed)
    {
        Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y);
        moveVector = transform.TransformDirection(moveVector);

        //transform.position += moveVector * (pawn.moveSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + (moveVector * (moveSpeed * Time.deltaTime)));
    }

    public override void Rotate(Vector2 rotateDirection, float turnSpeed)
    {
        float rotationAmount = rotateDirection.x;
        rotationAmount *= (turnSpeed);
        rotationAmount *= Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
    public override void RotateTowards(Vector3 postition, float turnSpeed) { 
            //tragert vector
        Vector3 vectorToTrage = postition - transform.position;

        // find the quaterunine 
        Quaternion lookRotation = Quaternion.LookRotation(vectorToTrage);

        // roatate just a little
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed* Time.deltaTime);
    }






}
