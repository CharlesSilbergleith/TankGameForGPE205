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
        //look at state 
        switch (currentState)
        {

            case AIState.Idle:
                DoIdle();
                //check for trans
                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    ChangeState(AIState.Shoot);
                }

                break;
            case AIState.Shoot:
                DoShoot();

                if (currentHealth > health.health)
                {
                    currentHealth=health.health;
                    ChangeState(AIState.Flee);
                }
               




                break;
           

                
            case AIState.Flee:
                DoFlee();
                if (Fled())
                {
                    ChangeState(AIState.Idle);
                }
                break;


        }





    }


    

}

