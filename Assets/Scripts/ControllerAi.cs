using UnityEngine;

public enum AIState {ChooseRoamDirection,Roam,Attack,TurnAndShoot, Idel}

public abstract class ControllerAi : Controller
{

    private Quaternion roamDirection= Quaternion.identity;
    private float transitionChangetime;
    protected AIState currenState = AIState.Roam;

    public override void Start() {
        //save our transtion time as now
        transitionChangetime = Time.deltaTime;
    }

    public void ChangeState(AIState newState) {
        currenState = newState;
        //save time this was done at
        transitionChangetime= Time.time;
    }





    public bool CanMoveForward(float distance) {
        //TODO: raycast Forward for was is passed in 
        // TODO:if hit return fasle else return true 

        return true;

    }
    public bool IsObjectInRange(Transform objectToCheck, float range) {
        // find dis between pawsn and obj chekcing 
        if (Vector3.Distance(objectToCheck.position, pawn.transform.position) < range) {

            return true;
        }
        return false;

    }

    public bool isRoamDirectionChose() {


        if (roamDirection != Quaternion.identity) {
            
            return true; 
        
        }
        return false;



        
    }

    public bool HasTimeElapsied(float seconds) {

        // if the current time minues the last is > the time we are waiting 
        if (Time.time - transitionChangetime >= seconds) {
            return true;
        }
        // otherwise the time hasent passed
        return false;
    }











}
