using UnityEngine;

public class ControllerAi_Runner : ControllerAi
{
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
                        ChangeState(AIState.Flee);
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
                        ChangeState(AIState.Flee);
                    break;

                case AIState.Flee:
                    DoFlee();
                    if (Fled()) ChangeState(AIState.Idle);
                    break;
            }
        }
    }
}