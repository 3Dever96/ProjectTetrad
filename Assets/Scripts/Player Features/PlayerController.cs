using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTetrad.PlayerFeatures
{
    [RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        CharacterController controller;
        PlayerInput input;
        CameraTarget cam;

        [Header("Basic Movement")]
        [SerializeField] float moveSpeed = 2.5f;
        [SerializeField] float turnSpeed = 500f;
        [SerializeField] float stickForce = -5f;
        [SerializeField] float gravity = -14.875f;

        float currentSpeed;
        float verticalSpeed;

        bool isGrounded;

        Vector2 move;

        Vector3 lookDirection;

        bool canMove;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
            cam = FindFirstObjectByType<CameraTarget>();

            lookDirection = -Vector3.forward;
        }

        void Update()
        {
            canMove = !IsSpawned || IsOwner;

            if (canMove)
            {
                isGrounded = verticalSpeed <= 0f && Physics.CheckSphere(transform.position + Vector3.up * (controller.radius - 0.02f), controller.radius - 0.01f, LayerMask.GetMask("Solid"));
            }
        }

        void FixedUpdate()
        {
            if (canMove)
            {
                Vector3 direction = Vector3.right * move.x + Vector3.forward * move.y;
                direction.y = 0f;
                direction = direction.normalized;

                if (move != Vector2.zero)
                {
                    currentSpeed = moveSpeed;
                    lookDirection = direction;
                }
                else
                {
                    currentSpeed = 0f;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed * Time.deltaTime);

                if (isGrounded)
                {
                    verticalSpeed = stickForce;
                }
                else
                {
                    verticalSpeed += gravity * Time.deltaTime;
                }

                Vector3 velocity = currentSpeed * lookDirection;
                velocity.y = verticalSpeed;

                controller.Move(velocity * Time.deltaTime);
            }
        }

        void OnEnable()
        {
            if (!IsSpawned)
            {
                if (input != null)
                {
                    input.onActionTriggered += OnAction;
                }

                if (cam != null)
                {
                    cam.SetTarget(transform);
                }
            }
        }

        private void OnDisable()
        {
            if (!IsSpawned)
            {
                input.onActionTriggered -= OnAction;
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                if (input != null)
                {
                    input.onActionTriggered += OnAction;
                }

                if (cam != null)
                {
                    cam.SetTarget(transform);
                }
            }
            else
            {
                enabled = false;
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsOwner)
            {
                input.onActionTriggered -= OnAction;
            }
        }

        void OnAction(InputAction.CallbackContext context)
        {
            if (canMove)
            {
                if (context.action.name == "Move")
                {
                    move = context.ReadValue<Vector2>();
                }
            }
        }
    }
}
