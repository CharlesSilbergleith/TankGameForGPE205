using UnityEngine;

public enum AIState
{
    Idle,
    Chase,
    Flee,
    ChaseAndShoot,
    Patrol,
    FindHealthPack,
    Rest,
    Shoot
}

public abstract class ControllerAi : Controller
{
    public Transform target;

    public float fleeDistance = 10f;
    public float hearingDistance = 1.0f;
    public float visionDistance = 10f;
    public float fovAngle = 60f;
    public float shootRange = 10f;

    public float lastStateChange;
    public AIState currentState;

    protected Vector3 targetPosition;
    protected Vector3 nextPos;
    private bool isTurning = false;
    private Quaternion targetRotation;
    private int patrolStep = 0;
    private bool patrolInitialized = false;

    public override void MakeDecisions()
    {
        pawn = GetComponent<Pawn>();

        if (GameManager.instance != null && GameManager.instance.tanks.Count > 0)
        {
            target = GameManager.instance.tanks[0].transform;
        }
    }

    public void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChange = Time.time;
    }

    // =========================
    // BASIC ACTIONS
    // =========================

    public void DoIdle()
    {
        // Do nothing
    }

    public void Seek(Vector3 position)
    {
        if (isTurning) return;
        Vector3 direction = position - pawn.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle(direction, pawn.transform.forward);

        // Always rotate
        pawn.RotateTowards(position, pawn.turnSpeed);

        // ONLY move when mostly facing target
        if (angle < 10f)
        {
            pawn.Move(Vector2.up);
        }
    }

    public void DoChase()
    {
        if (target != null)
            Seek(target.position);
    }

    public void DoChaseAndShoot()
    {
        DoChase();
        pawn.Shoot();
    }

    public void DoShoot()
    {
        if (target != null)
        {
            pawn.RotateTowards(target.position, pawn.turnSpeed);
            pawn.Shoot();
        }
    }


    public void DoPatrol(float distance)
    {
        // FIRST TIME SETUP
        if (!patrolInitialized)
        {
            patrolInitialized = true;

            // Snap starting position
            Vector3 pos = pawn.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);
            pawn.transform.position = pos;

            // Start by moving forward (NO TURN)
            nextPos = pawn.transform.position + pawn.transform.forward * distance;
            return;
        }

        // TURNING LOGIC
        if (isTurning)
        {
            pawn.transform.rotation = Quaternion.RotateTowards(
                pawn.transform.rotation,
                targetRotation,
                pawn.turnSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(pawn.transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
                nextPos = pawn.transform.position + pawn.transform.forward * distance;
            }

            return;
        }

        // REACHED CORNER START TURN
        if (AtLocation())
        {
            // Snap to grid
            Vector3 pos = pawn.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);
            pawn.transform.position = pos;

            patrolStep++;

            // Rotate RELATIVE (fixes 180 bug)
            targetRotation = pawn.transform.rotation * Quaternion.Euler(0, 90f, 0);

            isTurning = true;
            return;
        }

        // NORMAL MOVE
        Seek(nextPos);
    }
    public void DoFlee()
    {
        if (target == null) return;

        Vector3 directionAway = pawn.transform.position - target.position;
        directionAway.Normalize();

        targetPosition = pawn.transform.position + directionAway * fleeDistance;
        Seek(targetPosition);
    }

    // =========================
    // SENSORS
    // =========================

    public bool CanSee(GameObject targetObj)
    {
        if (targetObj == null) return false;

        Vector3 direction = targetObj.transform.position - pawn.transform.position;

        if (direction.magnitude > visionDistance)
            return false;

        float angle = Vector3.Angle(direction, pawn.transform.forward);

        if (angle > fovAngle)
            return false;

        RaycastHit hit;
        if (Physics.Raycast(pawn.transform.position, direction, out hit, visionDistance))
        {
            return hit.transform.gameObject == targetObj;
        }

        return false;
    }

    public bool CanHear(GameObject targetObj)
    {
        
        if (targetObj == null) return false;

        NoiseMaker noise = targetObj.GetComponent<NoiseMaker>();
        if (noise == null) return false;

        if (noise.noiseVolume <= 0) return false;

        float distance = Vector3.Distance(targetObj.transform.position, pawn.transform.position);
        Debug.Log(distance <= (noise.noiseVolume + hearingDistance));
        return distance <= (noise.noiseVolume + hearingDistance);
    }

    public bool InRange(GameObject targetObj)
    {
        if (!CanSee(targetObj)) return false;

        float distance = Vector3.Distance(targetObj.transform.position, pawn.transform.position);
        return distance <= shootRange;
    }

    // =========================
    // STATE CHECKS
    // =========================

    public bool AtLocation()
    {
        return Vector3.Distance(pawn.transform.position, nextPos) < 0.5f;
    }

    public bool Fled()
    {
        return Vector3.Distance(pawn.transform.position, targetPosition) < 0.5f;
    }

    
}