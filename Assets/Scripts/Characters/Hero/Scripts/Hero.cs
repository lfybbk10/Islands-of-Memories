using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float m_MovingTurnSpeed = 360;
    [SerializeField] private float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;
    [Range(1f, 4f)] [SerializeField] private float m_GravityMultiplier = 2f;
    [SerializeField] private float m_RunCycleLegOffset = 0.2f;
    [SerializeField] private float m_MoveSpeedMultiplier = 1f;
    [SerializeField] private float m_AnimSpeedMultiplier = 1f;
    [SerializeField] private float m_GroundCheckDistance = 0.1f;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isGrounded;
    private float _origGroundCheckDistance;
    private const float HALF = 0.5f;
    private float _turnAmount;
    private float _forwardAmount;
    private Vector3 _groundNormal;
    private float _capsuleHeight;
    private Vector3 _capsuleCenter;
    private CapsuleCollider _capsule;
    private bool _crouching;
    private bool _isHit;
    private bool _isRoll;

    private void Awake()
    {
        RuntimeContext.Instance.hero = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsule = GetComponent<CapsuleCollider>();
        _capsuleHeight = _capsule.height;
        _capsuleCenter = _capsule.center;
        
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ;
        _origGroundCheckDistance = m_GroundCheckDistance;
    }
    
    public void Move(Vector3 move, bool crouch, bool jump, bool hit, bool roll)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, _groundNormal);
        _turnAmount = Mathf.Atan2(move.x, move.z);
        _forwardAmount = move.z;
        _isHit = hit;
        _isRoll = roll;
        ApplyExtraTurnRotation();
        if (_isGrounded)
        {
            HandleGroundedMovement(crouch, jump);
        }
        else
        {
            HandleAirborneMovement();
        }

        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadroom();
        UpdateAnimator(move);
    }


    private void ScaleCapsuleForCrouching(bool crouch)
    {
        if (_isGrounded && crouch)
        {
            if (_crouching) return;
            _capsule.height = _capsule.height / 2f;
            _capsule.center = _capsule.center / 2f;
            _crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(_rigidbody.position + Vector3.up * _capsule.radius * HALF, Vector3.up);
            float crouchRayLength = _capsuleHeight - _capsule.radius * HALF;
            if (Physics.SphereCast(crouchRay, _capsule.radius * HALF, crouchRayLength, Physics.AllLayers,
                QueryTriggerInteraction.Ignore))
            {
                _crouching = true;
                return;
            }

            _capsule.height = _capsuleHeight;
            _capsule.center = _capsuleCenter;
            _crouching = false;
        }
    }

    private void PreventStandingInLowHeadroom()
    {
        if (!_crouching)
        {
            Ray crouchRay = new Ray(_rigidbody.position + Vector3.up * _capsule.radius * HALF, Vector3.up);
            float crouchRayLength = _capsuleHeight - _capsule.radius * HALF;
            if (Physics.SphereCast(crouchRay, _capsule.radius * HALF, crouchRayLength, Physics.AllLayers,
                QueryTriggerInteraction.Ignore))
            {
                _crouching = true;
            }
        }
    }

    private void UpdateAnimator(Vector3 move)
    {
        _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
        _animator.SetBool("Crouch", _crouching);
        _animator.SetBool("OnGround", _isGrounded);
        if (!_isGrounded)
        {
            _animator.SetFloat("Jump", _rigidbody.velocity.y);
        }

        if (_isHit && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            _animator.SetTrigger("Hit");
        if (_isRoll && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            _animator.SetTrigger("Roll");
        float runCycle =
            Mathf.Repeat(
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < HALF ? 1 : -1) * _forwardAmount;
        if (_isGrounded)
        {
            _animator.SetFloat("JumpLeg", jumpLeg);
        }
        
        if (_isGrounded && move.magnitude > 0)
        {
            _animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            _animator.speed = 1;
        }
    }


    void HandleAirborneMovement()
    {
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        _rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = _rigidbody.velocity.y < 0 ? _origGroundCheckDistance : 0.01f;
    }


    void HandleGroundedMovement(bool crouch, bool jump)
    {
        if (jump && !crouch && _animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, m_JumpPower, _rigidbody.velocity.z);
            _isGrounded = false;
            _animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.1f;
        }
    }

    void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, _forwardAmount);
        transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        if (_isGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (_animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
            v.y = _rigidbody.velocity.y;
            _rigidbody.velocity = v;
        }
    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo,
            m_GroundCheckDistance))
        {
            _groundNormal = hitInfo.normal;
            _isGrounded = true;
            _animator.applyRootMotion = true;
        }
        else
        {
            _isGrounded = false;
            _groundNormal = Vector3.up;
            _animator.applyRootMotion = false;
        }
    }
}