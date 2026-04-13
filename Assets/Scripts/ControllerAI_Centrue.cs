using UnityEngine;

public class ControllerAI_Centrue : ControllerAi
{
    public Health health;
    public float currentHealth;

    public override void Start()
    {
        health = GetComponent<Health>();
        currentHealth = health.health;
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
                        ChangeState(AIState.Shoot);
                    break;

                case AIState.Shoot:
                    if (CanSee(target.gameObject) || CanHear(target.gameObject))
                        DoShoot(target);
                    else
                        DoShoot(target2);

                    if (currentHealth > health.health)
                    {
                        currentHealth = health.health;
                        ChangeState(AIState.Flee);
                    }
                    break;

                case AIState.Flee:
                    DoFlee();
                    if (Fled()) ChangeState(AIState.Idle);
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
                        ChangeState(AIState.Shoot);
                    break;

                case AIState.Shoot:
                    DoShoot();
                    if (currentHealth > health.health)
                    {
                        currentHealth = health.health;
                        ChangeState(AIState.Flee);
                    }
                    break;

                case AIState.Flee:
                    DoFlee();
                    if (Fled()) ChangeState(AIState.Idle);
                    break;
            }
        }
    }
}