using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    [Header("Input")]
    public InputActionAsset inputActions;
    [Header("Timer")]
    public float timer;

    protected virtual void Awake()
    {
        pawn = GetComponent<Pawn>();


    }

    public override void MakeDecisions()
    {
        if (pawn == null)
        {
            return;
        }

        // Write this function to make the decisions
        Vector2 movementVector = inputActions["MoveP1"].ReadValue<Vector2>();

        pawn.Move(new Vector2(0, movementVector.y));
        pawn.Rotate(new Vector2(movementVector.x, 0));
        if (movementVector != new Vector2(0, 0))
        {
            pawn.MoveSound();
        }
        if (inputActions["ShootP1"].triggered)
        {

            pawn.Shoot();
        }

    }
    
     public void MakeDecisionsP2()
    {
        if (pawn == null)
        {
            return;
        }

        Vector2 movementVector = inputActions["MoveP2"].ReadValue<Vector2>();

        pawn.Move(new Vector2(0, movementVector.y));
        pawn.Rotate(new Vector2(movementVector.x, 0));

        if (movementVector != Vector2.zero)
        {
            pawn.MoveSound();
        }

        if (inputActions["ShootP2"].triggered)
        {
            pawn.Shoot();
        }
    
     }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        // Enable my input actions
        inputActions.Enable();

        // Add this to the list of players
        GameManager.instance.players.Add(this);
        if (isPlayer2)
        {
            UIManager.Instance.Player2 = this;
        }
        else { 
          UIManager.Instance.Player1 = this;
        }
          
    }

    public void OnDestroy()
    {
        // Remove this to the list of players
        GameManager.instance.players.Remove(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isPlayer2)
        {

            MakeDecisionsP2();



        }
        else
        {
            base.Update();
        }



    }
}