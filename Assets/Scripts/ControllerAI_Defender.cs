using UnityEngine;

public class ControllerAI_Defender : ControllerAi
{
    public Health health;

    public override void Start()
    {
        health = GetComponent<Health>();
    }

    public override void MakeDecisions()
    {
        base.MakeDecisions();

        if (GameManager.instance.isCoop)
        {
            switch (currentState)
            {
                case AIState.Idle:
                    DoIdle();
                    if (CanSee(target.gameObject) || CanHear(target.gameObject) ||
                        CanSee(target2.gameObject) || CanHear(target2.gameObject))
                        ChangeState(AIState.Chase);
                    break;

                case AIState.Chase:
                    if (CanSee(target.gameObject) || CanHear(target.gameObject))
                        DoChase(target);
                    else
                        DoChase(target2);

                    if (health.health < 5) ChangeState(AIState.Flee);

                    if (InRange(target.gameObject) || InRange(target2.gameObject))
                        ChangeState(AIState.ChaseAndShoot);

                    if (!(CanSee(target.gameObject) || CanHear(target.gameObject) ||
                          CanSee(target2.gameObject) || CanHear(target2.gameObject)))
                        ChangeState(AIState.Idle);
                    break;

                case AIState.ChaseAndShoot:
                    if (CanSee(target.gameObject) || CanHear(target.gameObject))
                        DoChaseAndShoot(target);
                    else
                        DoChaseAndShoot(target2);

                    if (!(CanSee(target.gameObject) || CanHear(target.gameObject) ||
                          CanSee(target2.gameObject) || CanHear(target2.gameObject)))
                        ChangeState(AIState.Idle);
                    break;

                case AIState.Flee:
                    DoFlee();
                    if (health.health == health.maxHealth)
                        ChangeState(AIState.Idle);
                    break;
            }
        }
        else
        {
            switch (currentState)
            {
                case AIState.Idle:
                    DoIdle();
                    if (CanSee(target.gameObject) || CanHear(target.gameObject))
                        ChangeState(AIState.Chase);
                    break;

                case AIState.Chase:
                    DoChase();
                    if (health.health < 5) ChangeState(AIState.Flee);
                    if (InRange(target.gameObject)) ChangeState(AIState.ChaseAndShoot);
                    if (!(CanSee(target.gameObject) || CanHear(target.gameObject)))
                        ChangeState(AIState.Idle);
                    break;

                case AIState.ChaseAndShoot:
                    DoChaseAndShoot();
                    if (!(CanSee(target.gameObject) || CanHear(target.gameObject)))
                        ChangeState(AIState.Idle);
                    break;

                case AIState.Flee:
                    DoFlee();
                    if (health.health == health.maxHealth)
                        ChangeState(AIState.Idle);
                    break;
            }
        }
    }
}