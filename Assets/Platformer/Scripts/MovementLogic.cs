using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementLogic : MonoBehaviour



{
    CharacterController characterController;
    Vector2 _velocity;
    [SerializeField]
    private float walkSpeed;
    //[SerializeField]
    // private float runSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float groundAccel;
    [SerializeField]
    private float turnDecel;
    [SerializeField]
    private float groundDecel;
    [SerializeField]
    private float airAccel;
    [SerializeField]
    private float airDecel;
    private float maxFallSpeed = 20f;
    float prevYVelocity;

    [SerializeField]
    private float apexHeight;
    [SerializeField]
    private float apexTime;
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private Material questionUsed;
    [SerializeField]
    private GameObject coin;
    public TextMeshProUGUI Mario;
    public TextMeshProUGUI Coins;
    private int marioScore = 0;
    private int coinsScore = 0;
    Animator animator;

    Quaternion facingRight;
    Quaternion facingLeft;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        facingRight = Quaternion.Euler(0f,90f,0f);
        facingLeft = Quaternion.Euler(0f, 270f, 0f);
        animator = GetComponent<Animator>();
         
    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0f;

        if (Keyboard.current.dKey.isPressed) direction += 1f;
        if (Keyboard.current.aKey.isPressed) direction -= 1f;

        float targetSpeed = direction * walkSpeed;
        bool jumpPressedthisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool jumpHeld = Keyboard.current.spaceKey.isPressed;
        bool grounded = characterController.isGrounded;
        bool isReverse = (Mathf.Sign(direction) != Mathf.Sign(_velocity.x) && Mathf.Abs(_velocity.x) > 0.01f);


        float accelToUse = grounded ? groundAccel : airAccel;
        float decelToUse = grounded ? groundDecel : airDecel;
        float gravityMod = 1;
        float prevY = prevYVelocity;
        
        

        
        
                if (direction != 0f)
                {
                    if (isReverse)
                     {
                        accelToUse = turnDecel;
                     }
                    _velocity.x = Mathf.MoveTowards(_velocity.x,targetSpeed, accelToUse * Time.deltaTime);
                    transform.rotation = (direction > 0f) ? facingRight : facingLeft;
                }
           
               else
                {
                    _velocity.x = Mathf.MoveTowards(  _velocity.x, 0f, decelToUse * Time.deltaTime);
                }

        if (characterController.isGrounded)
        {

            if (jumpPressedthisFrame)
            {
                _velocity.y = 2f * apexHeight / apexTime;
            }


        }
            if (!jumpHeld && _velocity.y > 0f)

            {
                 gravityMod = 2f;
            }

            
        
            


            float gravity = 2f * apexHeight / (apexTime * apexTime);

        if (prevY > 0f && _velocity.y <= 0f)
        {
            gravity = 0.1f;
        }
        prevYVelocity = _velocity.y;
        

        _velocity.y -= gravity * gravityMod * Time.deltaTime;
            _velocity.y = Mathf.Max( _velocity.y, -maxFallSpeed);

            float deltaX = _velocity.x * walkSpeed * Time.deltaTime;
            float deltaY = _velocity.y * Time.deltaTime;
            Vector3 directionDelta = new Vector3(deltaX, deltaY, 0f);
        gravity = 2f * apexHeight / (apexTime * apexTime);


        CollisionFlags collisions = characterController.Move(directionDelta);
        if ((collisions & CollisionFlags.CollidedAbove) != 0)
        {
            _velocity.y = 0f;
        }
        if ((collisions & CollisionFlags.CollidedSides) != 0)
        {
            _velocity.x = 0f;
        }

        animator.SetFloat("Speed",Mathf.Abs(_velocity.x));
        animator.SetFloat("Jump", Mathf.Abs(_velocity.y));
      
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if we hit something above
        if (Vector3.Dot(hit.normal, Vector3.down) > 0.5f)
        {

            if (hit.gameObject.CompareTag("Brick"))

            {
                Instantiate(particlePrefab, hit.point, Quaternion.identity);
                Destroy(hit.gameObject);
                marioScore += 100;
                Mario.text = "Mario: " + marioScore.ToString();
            }

            else if (hit.gameObject.CompareTag("Question"))
            {
                BlockScript block = hit.gameObject.GetComponent<BlockScript>();

                
                if (!block.used)
                {
                    block.used = true;
                    Renderer renderer = block.gameObject.GetComponent<Renderer>();
                    renderer.material = questionUsed;
                    coinsScore += 1;
                    marioScore += 100;
                    Mario.text = "Mario: " + marioScore.ToString();
                    Coins.text = "Coins: " + coinsScore.ToString();
                    Instantiate(coin, hit.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                   
                }
            }
        }
    }
}

