using UnityEngine;

public enum AIState {Idle, Chase, Flee, ChaseAndShoot, Patrol, FindHealthPack,Rest}

public abstract class ControllerAi : Controller
{

    public Transform target;
    public float fleeDistance = 10;
    public float lastStateChange;
    public AIState currentState;
    public float hearingDistance = 1.0f;
    public float visionDistance = 10;
    public float fovAngle = 60;
    public float shootRange = 10;



    public override void MakeDecisions() { 
        target = GameManager.instance.tanks[0].transform;
        pawn = GetComponent<Pawn>();
    }

    public void ChangeState(AIState newState) {
        currentState = newState;
        lastStateChange= Time.time;

    }



    public void DoIdel() { 
        // Do Nothing 
        // TODO: late, add idel animations

    }
    public void Seek(Vector3 position) { 
     pawn.RotateTowards(position, pawn.turnSpeed);



        //move Forward

        pawn.Move(new Vector3(0, 1,0));
    }

    public void DoChase() {
        //turn twods what we chase
       Seek(target.position);

    }
    public void DoChaseAndShoot() { 
        DoChase();
        pawn.Shoot();
    
    }
    public void DoFlee() { 
        // pick point away form player
       Vector3 vectorToTarget = pawn.transform.position- target.position;

        float distanceToplayer = vectorToTarget.magnitude;


        //reverse
        vectorToTarget = -vectorToTarget;

        // find the distace to flee

        vectorToTarget.Normalize();

        float percentOfFleeDistance = distanceToplayer / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);

        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        float newFleeDistance = flippedPercentOfFleeDistance * fleeDistance;

        Vector3 TragetPosition = pawn.transform.position +  (vectorToTarget * newFleeDistance);

        Seek( TragetPosition );




    }


    public bool CanSee(GameObject target)
    {
        RaycastHit hit;

        // Direction to target
        Vector3 direction = target.transform.position - pawn.transform.position;



        // Find the vector from the agent to the target
       
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(direction, pawn.transform.forward);
        // if that angle is less than our field of view
        if (angleToTarget < fovAngle)
        {

         // Line of sight check
                if (Physics.Raycast(pawn.transform.position, direction, out hit, visionDistance))
                {
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }



        }
       






       

        return false;
    }
    public bool CanHear(GameObject target) {
        //check if target has noicemaker
        NoiseMaker targetNoisemaker = target.GetComponent<NoiseMaker>();

        if (targetNoisemaker == null) {
            return false;
        }
        if (targetNoisemaker.noiseVolume > 0) { 
            float totalDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
            if (totalDistance <= targetNoisemaker.noiseVolume + hearingDistance) {
                return true;
            }
        }

        //otherwise
        return false;
        //if so  are they makeing noicse 

        //if so is the  between the two centers smaller then the two radi add together




    }

    public bool InRange(GameObject target) {
        RaycastHit hit;

        if (CanSee(target)) {
            Vector3 direction = target.transform.position - pawn.transform.position;
            if (Physics.Raycast(pawn.transform.position, direction, out hit, shootRange))
            {
                if (hit.transform == target)
                {
                    return true;
                }
            }

        }
        return false;

    }












}
