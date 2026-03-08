using UnityEngine;
[System.Serializable]

public class PowerUpMoveSpeed : PowerUp
{
    public float SpeedBoostAMount;
   
    public override void Apply(Pawn target) {
        // increase the pawns movespeed
        target.moveSpeed += SpeedBoostAMount;

    }
    public override void Remove(Pawn target) {
        target.moveSpeed -= SpeedBoostAMount;

    }


}
