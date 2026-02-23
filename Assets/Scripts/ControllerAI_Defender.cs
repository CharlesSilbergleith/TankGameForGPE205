using UnityEngine;

public class ControllerAI_Defender : ControllerAi
{
    public Health health;
   

    void Start() {
        health = GetComponent<Health>();
    }

    public override void MakeDecisions()
    {
        base.MakeDecisions();
        //look at state 
        switch (currentState) {

            case AIState.Idle:
                DoIdel();
                //check for trans
                if (CanSee( target.gameObject ) || CanHear(target.gameObject) ){
                    ChangeState(AIState.Chase);
                }

                break;
            case AIState.Chase:
                DoChase();

                if (health.health < 5) {
                    ChangeState(AIState.Flee);
                }
                if (InRange(target.gameObject) ){
                    ChangeState(AIState.ChaseAndShoot);
                }
               



                break;
            case AIState.ChaseAndShoot:
                if (!CanSee(target.gameObject) || !CanHear(target.gameObject))
                {
                    ChangeState(AIState.Idle);
                }


                break;
            case AIState.Flee:
                DoFlee();
                if (health.health == health.maxHealth) {
                    ChangeState(AIState.Idle);
                }
                break ;

    
        }





    }
}
