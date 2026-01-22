using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerPlayer : Controller
{
    public InputActionAsset inputAction;
    public override void MakeDecisions()
    {
        Vector2 moveVector = inputAction["Move"].ReadValue<Vector2>();
        pawn.Move(new Vector2(0, moveVector.y));
        pawn.Rotate(new Vector2(0, moveVector.x));

    }

    public override void Update()
    {
        MakeDecisions();
    }




}
