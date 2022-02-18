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
    public readonly int VerticalAimHash = Animator.StringToHash("VerticalAim");
    //public readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    //public readonly int IsReloadingHash = Animator.StringToHash("IsReloading");

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

        //if (Angle > 180 && Angle < 340)
        //{
        //    Angles.x = 340.0f;
        //}
        //else if (Angle < 180 && Angle > 40)
        //{
        //    Angles.x = 40.0f;
        //}

        //*********************
        //FollowTransform.transform.localEulerAngles = Angles;

        //FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.x * AimSensitivity, Vector3.up);

        //FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.y * AimSensitivity, Vector3.left);

        //var angles = FollowTransform.transform.localEulerAngles;
        //angles.z = 0;

        //var angle = FollowTransform.transform.localEulerAngles.x;

        //float min = -60;
        //float max = 70.0f;
        //float range = max - min;
        //float offsetToZero = 0 - min;
        //float aimAngle = FollowTransform.transform.localEulerAngles.x;
        //aimAngle = (aimAngle > 180) ? aimAngle - 360 : aimAngle;
        //float val = (aimAngle + offsetToZero) / (range);
        ////print(val);
        ////playerAnimator.SetFloat(verticalAimHash, val);


        //if (angle > 180 && angle < 300)
        //{
        //    angles.x = 300;
        //}
        //else if (angle < 180 && angle > 70)
        //{
        //    angles.x = 70;
        //}
        //**************************************************

        //***********************************************
        FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.x * AimSensitivity, Vector3.up);
        FollowTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.y * AimSensitivity, Vector3.left);
        transform.rotation = Quaternion.Euler(0, FollowTransform.transform.rotation.eulerAngles.y, 0);


        Vector3 angles = FollowTransform.transform.localEulerAngles;
        if (angles.x > cameraMax && angles.x < 180)
            angles.x = cameraMax;
        if (angles.x > 270 && angles.x < 360 + cameraMin)
            angles.x = 360 + cameraMin;
        transform.rotation = Quaternion.Euler(0, FollowTransform.transform.rotation.eulerAngles.y, 0);
        FollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        // Firing angles
        float min = -70;
        float max = 70;
        float range = max - min;
        float offsetToZero = 0 - min;
        float aimAngle = FollowTransform.transform.localEulerAngles.x;
        aimAngle = (aimAngle > 180) ? aimAngle - 360 : aimAngle;
        float verticalAim = (aimAngle + offsetToZero) / (range);
        //print(verticalAim);
        Playeranimator.SetFloat(VerticalAimHash, verticalAim);


        if (playerController.isJumping) return;
        if (!(InputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;
        float CurrentSpeed = playerController.isRunning ? RunSpeed : WalkSpeed;

        Vector3 MovementDirection = MoveDirection * (CurrentSpeed * Time.deltaTime);

        transform.position += MovementDirection;
        //*****************************************************
    }

    //private void FixedUpdate()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(transform.position, -Vector3.up,out hit, 100.0f, LayerMask.GetMask("Ground")))
    //    {

    //    }
    //}

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
        if (playerController.isJumping)
        {
            return;
        }

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

    private bool IsGroundCollision(ContactPoint[] contacts)
    {
        for (int i = 0; i < contacts.Length; i++)
        {
            if (1 - contacts[i].normal.y < 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        if (IsGroundCollision(collision.contacts))
        {
            playerController.isJumping = false;

            Playeranimator.SetBool(IsJumpingHash, false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping || RigidBody.velocity.y > 0) return;

        if (IsGroundCollision(collision.contacts))
        {
            playerController.isJumping = false;

            Playeranimator.SetBool(IsJumpingHash, false);
        }
    }
}
