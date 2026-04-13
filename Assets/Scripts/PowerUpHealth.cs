using UnityEngine;
[System.Serializable]
public class PowerUpHealth : PowerUp
{
    public float amountToHeal;



    public override void Apply(Pawn target) {
        //TODO: Heal The Pawn in traget
        if (target.health != null && target.health.health != target.health.maxHealth) { 
            target.health.Heal(amountToHeal);
        }
    
    }
    public override void Remove(Pawn target) { 
    
        //TODO NOTHING QW DONT DO anything remvoing a health powerup

    }
}
