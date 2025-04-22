using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _moveDirection;
    public GameObject retryButton;

    
    private bool _isGrounded;
    private bool _isJumpRequested;  
    
    private int _count;
    
    public float speed; 
    public float maxSpeed;
    public float jumpForce;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    public InputSystem_Actions PlayerControls;
    
    private InputAction _move;
    private InputAction _jump;

    private void Awake()
    {
        PlayerControls = new InputSystem_Actions();
    }
    
    private void OnEnable()
    {
        _move = PlayerControls.Player.Move;
        _move.Enable();
        
        _jump = PlayerControls.Player.Jump;
        _jump.Enable();

        _jump.performed += OnJumpPerformed;
    }

    private void Start()
    {
        retryButton.SetActive(false);

        _rb = GetComponent<Rigidbody>();
        SetCountText();
        
        winTextObject.SetActive(false);
    }
    
    private void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(_moveDirection.x * speed, 0.0f, _moveDirection.y * speed));
        
        LimitSpeed();
        
        if (_isJumpRequested && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isJumpRequested = false;
        }
    }

    private void OnDisable()
    {
        _move.Disable();
        _jump.Disable();
        _jump.performed -= OnJumpPerformed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PickUp")) return;
        
        other.gameObject.SetActive(false);
        _count++;
        SetCountText();

        if (_count >= 10)
        {
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
            winTextObject.SetActive(true);
            retryButton.SetActive(true);
    
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _isGrounded = true;
        
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        Destroy(gameObject);
            
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        winTextObject.SetActive(true);
        retryButton.SetActive(true);
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _isGrounded = false;
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext context) => _isJumpRequested = _isGrounded;

    private void SetCountText() => countText.text = "Count: " + _count;
    
    private void LimitSpeed()
    {
         var velocity = _rb.linearVelocity;
         
        if (velocity.magnitude > maxSpeed)
        {
            _rb.linearVelocity = velocity.normalized * maxSpeed;
        }
    }
}
