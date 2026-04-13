using UnityEngine;
[System.Serializable]


public class PowerUpPoints : PowerUp
{
    public float score;

    public override void Apply(Pawn target)
    {
        if (target.controller != null) {
            target.controller.addToScore(score);
        }

    }
    public override void Remove(Pawn target)
    {

        //TODO NOTHING QW DONT DO anything remvoing a health powerup

    }
}
