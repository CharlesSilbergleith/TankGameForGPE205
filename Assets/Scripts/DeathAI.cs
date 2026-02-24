using UnityEngine;

public class DeathAI : Death
{
    public override void Die() {
        Destroy(gameObject);
    }
}
