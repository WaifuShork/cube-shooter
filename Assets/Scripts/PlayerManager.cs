using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shork
{
    public class PlayerManager : MonoBehaviour
    {
        #region Player Postion & Physics Referencing
        public Rigidbody rb;
        public LayerMask groundMask;
        public Transform playerTransform;
        public GameObject player;
        public static float life = 1000;
        #endregion

        #region Movement Stats
        [Header("Movement Stats")]
        [HideInInspector]
        public float currentSpeed = 0f;
        public float baseSpeed = 10f;
        public float boostSpeed = 1.7f;
        float horizontalMove;
        float verticalMove;
        #endregion

        #region Jump Stats
        [Header("Jump Settings")]
        public Vector3 jump;
        public float jumpForce = 2.0f;
        public bool isGrounded = true;
        public bool canJump = true;
        public bool hadJumped;
        #endregion

        private void Start()
        {
            playerTransform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            PlayerInputs();
            PlayerRespawn();
        }

        // Handling physics in FixedUpdate for accuracy. Aka less jank because Update makes stuff fucking suck. 
        private void FixedUpdate()
        {

            Movement();
        }

        private void Movement()
        {
            jump = new Vector3(0.0f, jumpForce, 0.0f);

            Vector3 movement = transform.forward * verticalMove + transform.right * horizontalMove;
            movement = movement.normalized;

            rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + movement, currentSpeed * Time.deltaTime));


            // Handles getting sprint input and executing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = baseSpeed * boostSpeed;
            }
            else
            {
                currentSpeed = baseSpeed;
            }


            // Handles getting jump input and executing
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundMask);

            if (!hadJumped)
            {
                if (isGrounded && canJump)
                {
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                    isGrounded = false;
                    canJump = false;
                }
            }
            hadJumped = canJump;

        }

        private void PlayerRespawn()
        {
            if (playerTransform.position.y < -1)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

        private void PlayerInputs()
        {
            verticalMove = Input.GetAxisRaw("Vertical");
            horizontalMove = Input.GetAxisRaw("Horizontal");
            canJump = Input.GetKeyDown(KeyCode.Space);
        }
    }
}