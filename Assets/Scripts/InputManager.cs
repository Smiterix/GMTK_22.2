using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MasterInput mi;
    public static InputManager inst;
    public bool forwardDown = false;
    public bool leftDown = false;
    public bool rightDown = false;
    public bool backwardDown = false;
    public bool rightClickDown = false;
    public bool leftClickDown = false;
    public Vector2 mousePosition = Vector2.zero;


    // Start is called before the first frame update

    void Awake()
    {
        inst = this;

        mi = new MasterInput();
        mi.Enable();

        mi.Movement.BackwardUp.performed += ctx => { backwardDown = false; };
        mi.Movement.BackwardDown.performed += ctx => { backwardDown = true; };

        mi.Movement.ForwardUp.performed += ctx => { forwardDown = false; };
        mi.Movement.ForwardDown.performed += ctx => { forwardDown = true; };

        mi.Movement.LeftUp.performed += ctx => { leftDown = false; };
        mi.Movement.LeftDown.performed += ctx => { leftDown = true; };

        mi.Movement.RightUp.performed += ctx => { rightDown = false; };
        mi.Movement.RightDown.performed += ctx => { rightDown = true; };

        mi.Mouse.leftClickDown.performed += ctx => { leftClickDown = true; };
        mi.Mouse.leftClickUp.performed += ctx => { leftClickDown = false; };

        mi.Mouse.rightClickDown.performed += ctx => { rightClickDown = true; };
        mi.Mouse.rightClickUp.performed += ctx => { rightClickDown = false; };

        mi.Mouse.position.performed += ctx => { mousePosition = ctx.ReadValue<Vector2>(); };
    }

}
