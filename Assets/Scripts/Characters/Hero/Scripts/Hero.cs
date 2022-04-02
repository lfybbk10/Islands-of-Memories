using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
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
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, _groundNormal);
            _turnAmount = Mathf.Atan2(move.x, move.z);
            _forwardAmount = move.z;
            _isHit = hit;
            _isRoll = roll;
            ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
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

            // send input and other state parameters to the animator
            UpdateAnimator(move);
        }


        void ScaleCapsuleForCrouching(bool crouch)
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

        void PreventStandingInLowHeadroom()
        {
            // prevent standing up in crouch-only zones
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
        void UpdateAnimator(Vector3 move)
        {
            // update the animator parameters
            _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
            _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
            _animator.SetBool("Crouch", _crouching);
            _animator.SetBool("OnGround", _isGrounded);
            if (!_isGrounded)
            {
                _animator.SetFloat("Jump", _rigidbody.velocity.y);
            }
            if (_isHit && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
                _animator.SetTrigger("Hit");
            if (_isRoll && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
                _animator.SetTrigger("Roll");
            
            // calculate which leg is behind, so as to leave that leg trailing in the jump animation
            // (This code is reliant on the specific run cycle offset in our animations,
            // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
            float runCycle =
                Mathf.Repeat(
                    _animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
            float jumpLeg = (runCycle < HALF ? 1 : -1) * _forwardAmount;
            if (_isGrounded)
            {
                _animator.SetFloat("JumpLeg", jumpLeg);
            }

            // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
            // which affects the movement speed because of the root motion.
            if (_isGrounded && move.magnitude > 0)
            {
                _animator.speed = m_AnimSpeedMultiplier;
            }
            else
            {
                // don't use that while airborne
                _animator.speed = 1;
            }
        }


        void HandleAirborneMovement()
        {
            // apply extra gravity from multiplier:
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            _rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = _rigidbody.velocity.y < 0 ? _origGroundCheckDistance : 0.01f;
        }


        void HandleGroundedMovement(bool crouch, bool jump)
        {
            // check whether conditions are right to allow a jump:
            if (jump && !crouch && _animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                // jump!
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, m_JumpPower, _rigidbody.velocity.z);
                _isGrounded = false;
                _animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, _forwardAmount);
            transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
        }
        
        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (_isGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (_animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                v.y = _rigidbody.velocity.y;
                _rigidbody.velocity = v;
            }
        }


        void CheckGroundStatus()
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
}