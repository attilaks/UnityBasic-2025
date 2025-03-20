using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Breakout3D.Scripts
{
    public class PlayerPlatformController : MonoBehaviour
    {
        [Header("Input references")]
        [SerializeField] private InputAction moveLeftAction;
        [SerializeField] private InputAction moveRightAction;
        [SerializeField] private InputAction jumpAction;
        
        [Header("Object references")]
        [SerializeField] private Transform ballSpawnPoint;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform platform;
        [SerializeField] private Transform leftWall;
        [SerializeField] private Transform rightWall;
        [SerializeField] private BottomWall bottomWall;
        [SerializeField] private BricksController bricksController;
        
        [Header("Settings")]
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float ballMass = 1f;
        [SerializeField] private float ballForce = 30f;
        [SerializeField] private float maxStartAngle = 30f;
        
        private Rigidbody _ballRigidbody;
        private float _minXPosition;
        private float _maxXPosition;

        private bool BallStandsOnPlatform => _ballRigidbody.transform.parent == transform;
        
        private void Awake()
        {
            moveLeftAction.Enable();
            moveRightAction.Enable();
            jumpAction.Enable();

            jumpAction.performed += OnJumpActionPerformed;
            bottomWall.OnBallHitBottomWall += OnBallHitBottomWallPerformed;
            bricksController.OnBricksCreated += OnBricksCreatedPerformed;
        }

        private void OnBricksCreatedPerformed()
        {
            SpawnBall();
        }

        private void OnBallHitBottomWallPerformed()
        {
            SpawnBall();
        }


        private void Start()
        {
            var platformLength = platform.GetComponent<BoxCollider>().size.x * platform.localScale.x;
            var leftWallLength = leftWall.GetComponent<BoxCollider>().size.x * leftWall.localScale.x;
            _minXPosition = leftWall.position.x + leftWallLength / 2 + platformLength / 2;
            var rightWallLength = rightWall.GetComponent<BoxCollider>().size.x * rightWall.localScale.x;
            _maxXPosition = rightWall.position.x - rightWallLength / 2 - platformLength / 2;
        }

        private void Update()
        {
            var direction = 0f;
            if (moveLeftAction.IsPressed())
            {
                direction += Vector3.left.x;
            }

            if (moveRightAction.IsPressed())
            {
                direction += Vector3.right.x;
            }

            if (direction != 0f)
            {
                var deltaX = direction * movementSpeed * Time.deltaTime;
                var newX = Math.Clamp(deltaX + transform.position.x, _minXPosition, _maxXPosition) ;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }

        private void OnDestroy()
        {
            jumpAction.performed -= OnJumpActionPerformed;
            bottomWall.OnBallHitBottomWall -= OnBallHitBottomWallPerformed;
            bricksController.OnBricksCreated -= OnBricksCreatedPerformed;
            
            moveLeftAction.Disable();
            moveRightAction.Disable();
            jumpAction.Disable();
        }
        
        private void OnJumpActionPerformed(InputAction.CallbackContext obj)
        {
            PushBall();
        }
        
        private void SpawnBall()
        {
            if (_ballRigidbody)
            {
                Destroy(_ballRigidbody.gameObject);
            }
            
            var ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
            _ballRigidbody = ball.gameObject.TryGetComponent<Rigidbody>(out var rb) ? 
                rb : ball.gameObject.AddComponent<Rigidbody>();
            
            _ballRigidbody.mass = ballMass;
            _ballRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            _ballRigidbody.drag = 0f;
            _ballRigidbody.transform.parent = transform;
        }

        private void PushBall()
        {
            if (!BallStandsOnPlatform) return;
            
            var angle = Quaternion.Euler(0, 0, Random.Range(-maxStartAngle, maxStartAngle));
            var direction = angle * Vector3.up;
            _ballRigidbody.transform.parent = null;
            _ballRigidbody.AddForce(direction * ballForce, ForceMode.Impulse);
        }
    }
}
