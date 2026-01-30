using UnityEngine;

public class DeathTank : Death
{
    public override void Die() { 
        Destroy(this);
    }
}
