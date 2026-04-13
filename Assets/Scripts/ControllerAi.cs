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
    public Transform target2;

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
    private bool patrolInitialized = false;

    public override void Start()
    {
        GameManager.instance.AITanksLeft.Add(this);
        base.Start();
    }

    public override void MakeDecisions()
    {
        pawn = GetComponent<Pawn>();

        if (GameManager.instance == null) return;

        if (GameManager.instance.player1 != null)
            target = GameManager.instance.player1.transform;

        if (GameManager.instance.isCoop && GameManager.instance.player2 != null)
            target2 = GameManager.instance.player2.transform;
    }

    public void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChange = Time.time;
    }

    public void DoIdle() { }

    public void Seek(Vector3 position)
    {
        if (isTurning) return;

        Vector3 direction = position - pawn.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle(direction, pawn.transform.forward);

        pawn.RotateTowards(position, pawn.turnSpeed);

        if (angle < 10f)
            pawn.Move(Vector2.up);
    }

    public void DoChase()
    {
        if (target != null)
            Seek(target.position);
    }

    public void DoChase(Transform t)
    {
        if (t != null)
            Seek(t.position);
    }

    public void DoShoot()
    {
        if (target != null)
        {
            pawn.RotateTowards(target.position, pawn.turnSpeed);
            pawn.Shoot();
        }
    }

    public void DoShoot(Transform t)
    {
        if (t != null)
        {
            pawn.RotateTowards(t.position, pawn.turnSpeed);
            pawn.Shoot();
        }
    }

    public void DoChaseAndShoot()
    {
        DoChase();
        pawn.Shoot();
    }

    public void DoChaseAndShoot(Transform t)
    {
        DoChase(t);
        pawn.Shoot();
    }

    public void DoPatrol(float distance)
    {
        if (!patrolInitialized)
        {
            patrolInitialized = true;

            Vector3 pos = pawn.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = 2.6f;
            pos.z = Mathf.Round(pos.z);
            pawn.transform.position = pos;

            nextPos = pawn.transform.position + pawn.transform.forward * distance;
            return;
        }

        if (AtLocation())
        {
            Vector3 pos = pawn.transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);
            pawn.transform.position = pos;

            targetRotation = pawn.transform.rotation * Quaternion.Euler(0, 90f, 0);
            isTurning = true;
            return;
        }

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

    public bool CanSee(GameObject targetObj)
    {
        if (targetObj == null) return false;

        Vector3 direction = targetObj.transform.position - pawn.transform.position;

        if (direction.magnitude > visionDistance) return false;

        float angle = Vector3.Angle(direction, pawn.transform.forward);
        if (angle > fovAngle) return false;

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
        return distance <= (noise.noiseVolume + hearingDistance);
    }

    public bool InRange(GameObject targetObj)
    {
        if (!CanSee(targetObj)) return false;

        float distance = Vector3.Distance(targetObj.transform.position, pawn.transform.position);
        return distance <= shootRange;
    }

    public bool AtLocation()
    {
        return Vector3.Distance(pawn.transform.position, nextPos) < 0.5f;
    }

    public bool Fled()
    {
        return Vector3.Distance(pawn.transform.position, targetPosition) < 0.5f;
    }
}