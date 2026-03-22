using UnityEngine;

public class ControllerAi_Runner : ControllerAi
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
