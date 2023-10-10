using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        public bool facingRight = true;
        public bool lockMovement = false;

        public AudioClip coinSound;
        public float coinVolume = 0.1f;

        public AudioClip deathSound;
        public float deathVolume = 0.1f;

        private float moveInput;
        private Vector2 movement;

        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D _rigidbody;
        private Animator animator;
        private GameManager gameManager;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void FixedUpdate()
        {
            if (lockMovement)
                return;

            Vector2 resultVelocity = movement * Time.fixedDeltaTime;
            resultVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = resultVelocity;

            CheckGround();
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) 
            {
                moveInput = Input.GetAxisRaw("Horizontal");
                movement = transform.right * moveInput * movingSpeed;
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                movement = Vector2.zero;
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded)animator.SetInteger("playerState", 2); // Turn on jump animation

            if(facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                GameManager.Instance.PlaySound(deathSound, deathVolume);
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                GameManager.Instance.PlaySound(coinSound, coinVolume);
                gameManager.coinsCounter += 1;
                Destroy(other.gameObject);
            }
        }
    }
}
