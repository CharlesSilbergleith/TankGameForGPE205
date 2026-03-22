using UnityEngine;
public class ControllerAI_Roamer : ControllerAi
{
    public float timeInBetweenRoam = 2f;
    private float nextRoamTime;
    public float distance;


    public override void MakeDecisions()
    {

    
        base.MakeDecisions();

        if (target == null) return;

        bool canSee = CanSee(target.gameObject);
        bool canHear = CanHear(target.gameObject);

        switch (currentState)
        {
            case AIState.Idle:
                DoIdle();

                // Transition to patrol on timer
                if (Time.time >= nextRoamTime)
                {
                    ChangeState(AIState.Patrol);
                    nextRoamTime = Time.time + timeInBetweenRoam;
                }

                // Immediate reaction to player
                if (canSee || canHear)
                {
                    ChangeState(AIState.Shoot);
                }

                break;

            case AIState.Patrol:
                DoPatrol(distance);

                // Engage player if detected
                if (canSee || canHear)
                {
                    ChangeState(AIState.Shoot);
                }

                break;

            case AIState.Shoot:
                DoShoot();

                // Only leave when completely unaware of player
                if (!canSee && !canHear)
                {
                    ChangeState(AIState.Idle);
                }

                break;
        }
    }
}
