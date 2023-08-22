using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isRightSide = true;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float jumpOffset;
    [SerializeField] private LayerMask groundMask;

    [Header("Shooter")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private float fireSpeed;
    [SerializeField] private Transform firePoint;

    [Header("Health")]
    [SerializeField] private GameObject deathPanel;
    public float maxHealth;
    public float currentHealth;

    [Header("Gold")]
    public float goldPoint = 0;
    public float goldMax = 0;

    [Header("Nickname")]
    public Text nick;

    private float fireRate = 2;
    private float elapsedTime = 0;

    private Joystick joystick;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private JumpButton jumpButton;
    private FireButton fireButton;
    private GameEnd gameEnd;
    PhotonView view;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        view = GetComponent<PhotonView>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        jumpButton = GameObject.FindGameObjectWithTag("JumpButton").GetComponent<JumpButton>();
        fireButton = GameObject.FindGameObjectWithTag("FireButton").GetComponent<FireButton>();
        currentHealth = maxHealth;
        nick.text = view.Owner.NickName;

        if(view.Owner.IsLocal)
        {
            Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
        }
    }

    void Update()
    {
        float horizontalDirection = joystick.Horizontal;
        bool isJumpButtonPressed = jumpButton.isClikedJump;
        bool isFireButtonPressed = fireButton.isClikedFire;
        elapsedTime += Time.deltaTime;
        if (view.IsMine)
        {
            
            if(isFireButtonPressed == true)
            {
                if(elapsedTime > fireRate)
                {
                    Shoot();
                    animator.SetBool("isAttack", true);
                }
            }
            if(isFireButtonPressed == false)
            {
                animator.SetBool("isAttack", false);
            }

            if(currentHealth == 0)
            {
                deathPanel.SetActive(true);
                sr.enabled = false;
                //PhotonNetwork.Destroy(gameObject);       
            } 
            Move(-horizontalDirection, isJumpButtonPressed);  
        }      
    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }
    private void Move(float direction, bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed)
        {
            Jump();
        }

        if (Mathf.Abs(direction) > 0.01f)
        {
            HorizontalMovement(direction);
            animator.SetBool("isRunning", true);
        }
        else
            animator.SetBool("isRunning", false);

    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }
    }
    private void HorizontalMovement(float direction)
    {
        rb.velocity = new Vector2(curve.Evaluate(direction), rb.velocity.y);

        if ((direction > 0f && !isRightSide) || (direction < 0f && isRightSide))
        {
            Spin();
        }

    }
    private void Spin()
    {
        isRightSide = !isRightSide;
        sr.flipX = !isRightSide;
        firePoint.localPosition = new Vector2(firePoint.localPosition.x * -1, firePoint.localPosition.y);
    }

    private void Shoot()
    {
        elapsedTime = 0;
        var currentBullet = PhotonNetwork.Instantiate(arrow.name, firePoint.position, Quaternion.identity);
        Rigidbody2D currenBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = currentBullet.GetComponent<SpriteRenderer>();
        if (isRightSide)
        {
            currenBulletVelocity.velocity = new Vector2(fireSpeed * 1, currenBulletVelocity.velocity.y);
            spriteRenderer.flipX = false;
        }
        else
        {
            currenBulletVelocity.velocity = new Vector2(fireSpeed * -1, currenBulletVelocity.velocity.y);
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Gold"))
        {
            goldPoint++;
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }
}
