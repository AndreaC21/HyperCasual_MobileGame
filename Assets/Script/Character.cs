using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ysocorp.obstacle;
namespace ysocorp.character
{
    public class Character : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigibody;
        [Header("Options")]
        [SerializeField] private bool _isAlive = false;

        private bool _isGrounded, _isFalling;
        // Start is called before the first frame update

        void Awake()
        {
            _isGrounded = true;
            if (!_isAlive)
            {
                enabled = false;
            }
            else
            {
                StartLife();
            }
        }

        public void Build(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void StartLife()
        {
            _isAlive = true;
            _animator.SetBool("isAlive", true);
            enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isGrounded && !_isFalling && transform.localPosition.y < -1.5f)
            {
                _isFalling = true;
                _gameManager.KillCharacter(this);
                Kill();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            ObstacleInstanceUnity o = collision.transform.gameObject.GetComponent<ObstacleInstanceUnity>();
            if (o != null)
            {
                _gameManager.KillCharacter(this);
                _animator.SetInteger("DeathType", (int)DeathType.FallBack);
            }
            else if (collision.collider.tag == "Ground")
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.tag == "Ground")
            {
                _isGrounded = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EndLine")
            {
                _gameManager.PlayerWin(this);
                Win();
            }
        }

        public void Jump(float jumpForce)
        {
            if (!_isGrounded)
            {
                return;
            }
            _animator.Play("Jump");
            _rigibody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void RunSlide()
        {
            _animator.Play("RunSlide");
        }

        /// <summary>
        /// Call on Deaths Animations
        /// </summary>
        public void Kill()
        {
            Destroy(gameObject);
        }

        private void Win()
        {
            _animator.Play("Victory");
            _gameManager.KillCharacter(this);
            enabled = false;
        }

        public enum DeathType
        {
            FallFront = 0,
            FallBack = 1
        }
    }
}
