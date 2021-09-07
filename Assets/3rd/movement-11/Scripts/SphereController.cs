using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CommandMoving
{
    public Vector3 direction;
    public bool jump;
    public bool clamp;
}

public class SphereController : MonoBehaviour
{
    public MovingSphere target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        Vector3 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");
        playerInput.y = Input.GetAxis("UpDown");
//        playerInput = Vector3.ClampMagnitude(playerInput, 1f);
//        target.Move(playerInput);
//        target.Jump(Input.GetButtonDown("Jump"));
//        target.Climb(!Input.GetButton("Climb"));
//        target.UpdateInput();
        target.SetCommand(new CommandMoving
        {
            direction = playerInput,
            jump = Input.GetButtonDown("Jump"),
            clamp = Input.GetButton("Climb"),
        });
    }
}
