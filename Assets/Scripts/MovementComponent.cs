using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    //Movement Variables
    [SerializeField]
    float WalkSpeed = 5;
    [SerializeField]
    float RunSpeed = 10;
    [SerializeField]
    float JumpSpeed = 5;

    //Components
    PlayerController playerController;

    //Movement Refrences
    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMovement(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        print("InputVector:" + InputVector);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        playerController.isJumping = value.isPressed;
    }
}
