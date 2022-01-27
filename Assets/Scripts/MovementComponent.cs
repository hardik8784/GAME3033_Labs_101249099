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
    float JumpForce = 40;

    //Components
    PlayerController playerController;
    Rigidbody RigidBody;
    Animator Playeranimator;
    public GameObject FollowTransform;

    //Movement Refrences
    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    Vector2 LookInput = Vector2.zero;

    public float AimSensitivity = 1.0f;
    public float cameraMin = -70, cameraMax = 70;

    public readonly int MovementXHash = Animator.StringToHash("MovementX");
    public readonly int MovementYHash = Animator.StringToHash("MovementY");
    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        Playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ////Camera X-Axis Rotation
        //FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.x * AimSensitivity /** Time.deltaTime*/, Vector3.up);

        ////Camera Y-Axis Rotation
        //FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.y * AimSensitivity /** Time.deltaTime*/, Vector3.left);

        //var Angles = FollowTransform.transform.eulerAngles;
        //Angles.z = 0;

        //var Angle = FollowTransform.transform.localEulerAngles.x;

        //if(Angle > 180 && Angle <340)
        //{
        //    Angles.x = 340.0f;
        //}
        //else if(Angle <180 && Angle >40)
        //{
        //    Angles.x = 40.0f;
        //}

        //FollowTransform.transform.localEulerAngles = Angles;

        FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.x * AimSensitivity, Vector3.up);
        FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.y * AimSensitivity, Vector3.left);
        Vector3 angles = FollowTransform.transform.localEulerAngles;
        if (angles.x > cameraMax && angles.x < 180)
            angles.x = cameraMax;
        if (angles.x > 270 && angles.x < 360 + cameraMin)
            angles.x = 360 + cameraMin;
        transform.rotation = Quaternion.Euler(0, FollowTransform.transform.rotation.eulerAngles.y, 0);
        FollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        if (playerController.isJumping) return;
        if (!(InputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;
        float CurrentSpeed = playerController.isRunning ? RunSpeed : WalkSpeed;

        Vector3 MovementDirection = MoveDirection * (CurrentSpeed * Time.deltaTime);

        transform.position += MovementDirection;
    }

    public void OnMovement(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        Playeranimator.SetFloat(MovementXHash, InputVector.x);
        Playeranimator.SetFloat(MovementYHash, InputVector.y);
        //print("InputVector:" + InputVector);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        Playeranimator.SetBool(IsRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        //if (playerController.isJumping) return;

        playerController.isJumping = value.isPressed;
        RigidBody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        Playeranimator.SetBool(IsJumpingHash, playerController.isJumping);
    }

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        LookInput = value.Get<Vector2>();
        //If we Aim Up,Down,Adjust animations to have a mask that will let us properly animate aim
    }

    public void OnReLoad(InputValue value)
    {
        playerController.isReloading = value.isPressed;
    }

    public void OnFire(InputValue value)
    {
        playerController.isFiring = value.isPressed;

        //Set up Firing Animation

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;

        Playeranimator.SetBool(IsJumpingHash, false);
    }
}
