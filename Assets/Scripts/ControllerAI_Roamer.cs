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

        if (GameManager.instance.isCoop)
        {
            if (target2 == null) return;

            bool p1 = CanSee(target.gameObject) || CanHear(target.gameObject);
            bool p2 = CanSee(target2.gameObject) || CanHear(target2.gameObject);

            switch (currentState)
            {
                case AIState.Idle:
                    DoIdle();
                    if (Time.time >= nextRoamTime)
                    {
                        ChangeState(AIState.Patrol);
                        nextRoamTime = Time.time + timeInBetweenRoam;
                    }
                    if (p1 || p2) ChangeState(AIState.Shoot);
                    break;

                case AIState.Patrol:
                    DoPatrol(distance);
                    if (p1 || p2) ChangeState(AIState.Shoot);
                    break;

                case AIState.Shoot:
                    if (p1) DoShoot(target);
                    else DoShoot(target2);

                    if (!p1 && !p2) ChangeState(AIState.Idle);
                    break;
            }
        }
        else
        {
            bool seen = CanSee(target.gameObject) || CanHear(target.gameObject);

            switch (currentState)
            {
                case AIState.Idle:
                    DoIdle();
                    if (Time.time >= nextRoamTime)
                    {
                        ChangeState(AIState.Patrol);
                        nextRoamTime = Time.time + timeInBetweenRoam;
                    }
                    if (seen) ChangeState(AIState.Shoot);
                    break;

                case AIState.Patrol:
                    DoPatrol(distance);
                    if (seen) ChangeState(AIState.Shoot);
                    break;

                case AIState.Shoot:
                    DoShoot();
                    if (!seen) ChangeState(AIState.Idle);
                    break;
            }
        }
    }
}