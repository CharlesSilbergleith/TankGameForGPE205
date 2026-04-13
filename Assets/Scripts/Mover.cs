using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public AudioSource moveAudio;
    public abstract void Move(Vector2 moveDirection, float moveSpeed);
    public abstract void Rotate(Vector2 rotateDirection, float turnSpeed);
    public abstract void RotateTowards(Vector3 postition, float turnSpeed);

    public virtual void Start()
    {
        moveAudio = GetComponent<AudioSource>();
    }
 
}
