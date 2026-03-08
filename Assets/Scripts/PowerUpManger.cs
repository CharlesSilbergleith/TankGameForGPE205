using UnityEngine;
using System.Collections.Generic;

public class PowerUpManger : MonoBehaviour
{

    public List<PowerUp> powerupsList= new List<PowerUp>();
    private Pawn pawn;
    public void Start() { 
     pawn = GetComponent<Pawn>();
   
}

    public void Update() {
        //TODO: check for expired Powerupos and remove them
        //TODO TODO: waylater - this is where you would check for apply over time;

        //update time(lifespace)
        updatePowerUpLifeSpan();
        checkForExpiredPowerUps();

    }
    public void checkForExpiredPowerUps() {
        //make a list of the expired
        List<PowerUp> powerUpsToRemove = new List<PowerUp> ();
        //check for expired 
        foreach (PowerUp powerup in powerupsList) {
            if (powerup.lifeSpan <= 0) {
                powerUpsToRemove.Add(powerup);
            }
        }
        //remove from main list
        foreach (PowerUp powerup in powerUpsToRemove) { 
            Remove(powerup);
        }
    }
    public void updatePowerUpLifeSpan() {
        foreach (var powerup in powerupsList) { 
            powerup.lifeSpan -= Time.deltaTime;
        }
    
    }


    public void Add(PowerUp powerup){
        powerup.Apply(pawn);
        //add to list 
        if (powerup.lifeSpan >= 0)
        {
            powerupsList.Add(powerup);
        }
    }

    public void Remove(PowerUp powerup) {
        //remove power UP effect 
        powerup.Remove(pawn);

        powerupsList.Remove(powerup);
    }
}
