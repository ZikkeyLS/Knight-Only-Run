using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public Vector2 limitsX = new Vector2(-2f, 2f);

        private Rigidbody2D _rigidbody;


        void Start()
        {
            limitsX = new Vector2(transform.position.x + limitsX.x, transform.position.x + limitsX.y);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _rigidbody.velocity = new Vector2(moveSpeed, _rigidbody.velocity.y);
        }

        void FixedUpdate()
        {
            bool outLeft = _rigidbody.position.x < limitsX.x && moveSpeed < 0;
            bool outRight = _rigidbody.position.x > limitsX.y && moveSpeed > 0;
            if (outLeft || outRight)
            {
                Flip();
            }
        }
        
        private void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            moveSpeed *= -1;
        }
    }
}
