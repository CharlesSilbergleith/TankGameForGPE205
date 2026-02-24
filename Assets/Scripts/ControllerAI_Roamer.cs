using UnityEngine;
public class ControllerAI_Roamer : ControllerAi
{
    public float timeInBetweenRoam = 2f;
    private float nextRoamTime;
    public float distance;


    public override void MakeDecisions()
    {
        base.MakeDecisions();

        switch (currentState)
        {
            case AIState.Idle:
                DoIdle();

                if (Time.time >= nextRoamTime)
                {
                    ChangeState(AIState.Patrol);
                    nextRoamTime = Time.time + timeInBetweenRoam;
                }

                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    ChangeState(AIState.Shoot);
                }

                break;

            case AIState.Patrol:
                DoPatrol(distance);

               

                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    ChangeState(AIState.Shoot);
                }

                break;

            case AIState.Shoot:
                DoShoot();

                if (!CanSee(target.gameObject) || !CanHear(target.gameObject))
                {
                    ChangeState(AIState.Idle);
                }

                break;
        }
    }
}