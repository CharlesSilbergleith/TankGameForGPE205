using UnityEngine;


[RequireComponent (typeof(Collider))]
public class PickUp : MonoBehaviour
{
   public PowerUp powerUp;

    public virtual void  Start() {
        //set collider as a trigger
        Collider theCollider = GetComponent<Collider>();
        theCollider.isTrigger = true;

    }
    public virtual void  Update() { 
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

}
