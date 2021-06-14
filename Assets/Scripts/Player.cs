using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask dashLayerMask;
    Rigidbody2D rb;
    private MeleeAttackController meleeAttackController;
    private Vector3 dir;
    private string currentState;
    private Scene scene;
    private bool isJumpDown;
    public float TimeAttack;
    private bool isDashDown;
    private bool isAttackPressed;
    private bool isAttack;
    private float timer;
    private float timerAttack;
    public float timeDash = 1f;
    public float speed;
    public float jumpHeight;
    public Transform GroundCheck;
    bool isGrounded;
    public Animator anim;
    public float forceDash;
    public Main main;
    public Player player;

    public GameObject trail;


    const string IDLE = "IDLE";
    const string DASH = "DASH";
    const string JUMP = "JUMP";
    const string RUN = "RUN";
    const string DOWN = "DOWN";
    const string ATTACK = "ATTACKn";

    void Start()
    {
        
        meleeAttackController = gameObject.GetComponent<MeleeAttackController>();
        TimeAttack = meleeAttackController.TimeAttack;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {        
        CheckGround();
        Fall();
        
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        { 
            isJumpDown = true;
        }

        //DASH
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashDown = true;
                //ChangeAnimationState(DASH);
                Trail();
                
            }    
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttackPressed = true;
        }

        if (timerAttack <= 0)
        {
            if (isAttackPressed)
            {
                isAttackPressed = false;
                if (!isAttack)
                {
                    isAttack = true;

                    if (isGrounded)
                        ChangeAnimationState(ATTACK);
                }
                Invoke("AttackComplete", TimeAttack);
                timerAttack = TimeAttack;
            }
        } else
            timerAttack -= Time.deltaTime;
        

        //RUN&IDLE
        if (!isAttack)
        {
            Flip();
            if (isGrounded && Input.GetAxis("Horizontal") != 0)
            {
                ChangeAnimationState(RUN);
            }
            else
            if (isGrounded)
            {
                ChangeAnimationState(IDLE);
            }
        }

    }

    private void FixedUpdate()
    {
        //Run
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y);


        //Jump
        if (isJumpDown) {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            isJumpDown = false;
        }

        //Dash
        if (isDashDown)
        {
            dir = new Vector3(Input.GetAxis("Horizontal"), 0).normalized;
            Vector3 dashPosition = transform.position + dir * forceDash;

            RaycastHit2D rc = Physics2D.Raycast(transform.position, dir, forceDash, dashLayerMask);
            if (rc.collider != null) {
                dashPosition = rc.point;
            }

            rb.MovePosition(dashPosition);
            isDashDown = false;
            timer = timeDash;

        }

    }

    void Flip() {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, 0.3f);
        isGrounded = colliders.Length > 2;
        //anim.SetBool("IsJamping", !isGrounded);

        if (!isGrounded && rb.velocity.y > 0)
            ChangeAnimationState("JUMP");

        if (!isGrounded && rb.velocity.y < 0)
            ChangeAnimationState("DOWN");

    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void AttackComplete() 
    {
        isAttack = false;
    }


    public void Fall() 
    {
        if (player.transform.position.y <= -10f)
        {
            Invoke("Lose", 2f);
        }
    }

    void Lose()
    {
        main.GetComponent<Main>().Losing();
    }

    void Trail()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0).normalized;
        if (dir.x > 0) 
        { 
        GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(instance, 8f);
        }

        if (dir.x < 0) 
        {
            GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.Euler(0, 180, 0));
            Destroy(instance, 8f);
        }
    }

}
