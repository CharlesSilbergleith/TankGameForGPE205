using UnityEngine;

public class ControllerAI_Roamer : ControllerAi
{
   

    public override void MakeDecisions()
    {
        switch (currenState) {

            case AIState.Idel:
                //do Nothing 
                break;

            case AIState.Roam:
                //TODO: rotate towred our roam direction 
                //TODO: move forward 
                
                break;
            case AIState.ChooseRoamDirection:
                // TODO: choose new diretion 
                break;
            case AIState Attak:
                if (!CanMoveForward(5))
                {
                    ChangeState(AIState.Roam);
                }                
                break;

        
        
        
        
        
        }



    }


    public void DoIdeal() { 
    
    }

    public void DoRaom() { 
    
    }

    public void DoAttak() { 
        
    }
    public void DoChooseRoamDirection() { 
    
    }





}
