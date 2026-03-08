using UnityEngine;

public class PickUpHealth : PickUp
{
    public PowerUpHealth PowerUp;

    public override void OnTriggerEnter(Collider other) { 
    //check if the other obj has a poweupmanger
    PowerUpManger othermanger = other.GetComponent<PowerUpManger>();


        if (othermanger != null)
        {
            othermanger.Add(PowerUp);


            Destroy(gameObject);
        }
        base.OnTriggerEnter(other);
       
    
    }
}
