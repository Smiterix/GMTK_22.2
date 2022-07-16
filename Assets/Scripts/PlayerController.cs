using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using Animancer;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [Header("Animation")]
    public float locomotionSpeed = 1f;
    public MixerTransition2D locomotion;
    [Header("Settings")]
    public float acceleration = 10f;
    public float maxVelocity = 5f;
    public float drag = 1f;
    public float g = 1f;
    public LayerMask whatIsGround;
    public float rotationSpeed = 100f;
    float rotation = 0f;

    [Header("Stats")]
    public Vector3 velocity = Vector3.zero;
    public Vector3 movementVector = Vector3.zero;
    public bool grounded = false;

    [Header("References")]
    public Camera cam;
    public KinematicCharacterMotor motor;
    public Transform cameraAnchor;
    public Transform looker;
    public AnimancerComponent animancer;
    public static PlayerController inst;
    public Vector3 lookerRotation;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        motor.CharacterController = this;
    }

    // Update is called once per frame
    void Update()
    {
        createMovementVector();
        handleCharacterAnimation();

        Vector3 alteredMovementVector = 1.21f * velocity.normalized * (velocity.magnitude / maxVelocity);
        alteredMovementVector = Quaternion.Euler(0, 360f - rotation, 0) * alteredMovementVector;
        locomotion.State.Parameter = new Vector2(alteredMovementVector.x, alteredMovementVector.z);
    }

    void handleCharacterAnimation()
    {

        AnimancerState state = animancer.States.GetOrCreate(locomotion);
        state.Speed = locomotionSpeed;
        animancer.Play(state);


    }

    void createMovementVector()
    {
        movementVector = Vector3.zero;

        if (InputManager.inst.forwardDown) movementVector += cameraAnchor.forward;
        if (InputManager.inst.backwardDown) movementVector += -cameraAnchor.forward;
        if (InputManager.inst.leftDown) movementVector += -cameraAnchor.right;
        if (InputManager.inst.rightDown) movementVector += cameraAnchor.right;

        movementVector = Vector3.ClampMagnitude(movementVector, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + movementVector * 3);
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(new Vector3(InputManager.inst.mousePosition.x, InputManager.inst.mousePosition.y, 0)), out hit, 100f, whatIsGround))
        {
            looker.LookAt(hit.point + hit.normal);
            lookerRotation = looker.rotation.eulerAngles;
            currentRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, looker.rotation.eulerAngles.y, 0f), deltaTime * rotationSpeed);
            rotation = currentRotation.eulerAngles.y;
        }
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        //updating velocity
        velocity += movementVector * acceleration * deltaTime;

        //drag
        if (movementVector == Vector3.zero) velocity *= (1f / (1f + (drag * deltaTime)));

        //clamp
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

        if (!grounded) velocity += -transform.up * g * deltaTime;


        currentVelocity = velocity;
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        // throw new System.NotImplementedException();
    }

    public void PostGroundingUpdate(float deltaTime)
    {
        if (motor.GroundingStatus.IsStableOnGround && !motor.LastGroundingStatus.IsStableOnGround)
            grounded = true;
        else if (!motor.GroundingStatus.IsStableOnGround && motor.LastGroundingStatus.IsStableOnGround)
            grounded = false;
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        // throw new System.NotImplementedException();
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        // throw new System.NotImplementedException();
        return true;
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        // throw new System.NotImplementedException();
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        // throw new System.NotImplementedException();
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        // throw new System.NotImplementedException();
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
        // throw new System.NotImplementedException();
    }
}
