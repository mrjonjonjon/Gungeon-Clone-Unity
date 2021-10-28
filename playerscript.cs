using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    Color invin = new Color(0.14f, 0.8f, 0.8f, 1f);

    Color normal = new Color(1f, 1f, 1f, 1f);

    public Slider hpbar;

    public static playerscript sharedInstance = null;

    public bool isvulnerable = true;

    string animationState = "animationState";

    public float currenthp = 10;

    public float maxhp = 10;

    public float playerspeed = 0.05f;

    public float dashtimer = 0f;

    public float dashsize = 0.05f;

    public Vector2 currentDirection;

    Vector2 currentPosition;

    Animator animator;

    Vector2 movement = new Vector2();

    //bool isincutscene=false;
    enum CharStates
    {
        walkLeft = 0,
        walkUp = 1,
        walkRight = 2,
        walkDown = 3,
        idle = 4,
        rollLeft = 5,
        rollUp = 6,
        rollRight = 7,
        rollDown = 8
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this);


        // 2
        if (sharedInstance != null && sharedInstance != this)
        {
            // 3
            Destroy (gameObject);
        }
        else
        {
            // 4
            sharedInstance = this;
        }
    }

    private void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (dashtimer <= 0 && movement.x > 0)
            {
                dashtimer = 60f;
                animator.SetInteger(animationState, (int) CharStates.rollRight);
                StartCoroutine(dash(currentDirection));
            }
            else if (dashtimer <= 0 && movement.x < 0)
            {
                dashtimer = 60f;
                animator.SetInteger(animationState, (int) CharStates.rollLeft);
                StartCoroutine(dash(currentDirection));
            }
            else if (dashtimer <= 0 && movement.y > 0)
            {
                dashtimer = 60f;
                animator.SetInteger(animationState, (int) CharStates.rollUp);
                StartCoroutine(dash(currentDirection));
            }
            else if (dashtimer <= 0 && movement.y < 0)
            {
                dashtimer = 60f;
                animator.SetInteger(animationState, (int) CharStates.rollDown);
                StartCoroutine(dash(currentDirection));
            }
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int) CharStates.walkLeft);
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int) CharStates.walkUp);
        }
        else if (movement.x > 0)
        {
            animator.SetInteger(animationState, (int) CharStates.walkRight);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int) CharStates.walkDown);
        }
        else
        {
            animator.SetInteger(animationState, (int) CharStates.idle);
        }
    }

  
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(animationState, (int) CharStates.idle);
        currentPosition =
            new Vector2(transform.position.x, transform.position.y);
    }

    
    IEnumerator dash(Vector2 currentDirection)
    {
        this.GetComponent<AudioSource>().Play();

        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            sharedInstance
                .gameObject
                .GetComponent<Rigidbody2D>()
                .MovePosition(transform.position +
                new Vector3(currentDirection.x * dashsize * Time.fixedDeltaTime,
                    currentDirection.y * dashsize * Time.fixedDeltaTime,
                    0));

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

       
        hpbar.value = currenthp / maxhp;

        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Base.idledown"));
        if (
            animator.GetCurrentAnimatorStateInfo(0).IsName("Base.rolldown") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Base.rollup") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Base.rollleft") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Base.rollright")
        )
        {
            isvulnerable = false;
            sharedInstance.gameObject.GetComponent<SpriteRenderer>().color =
                invin;
        }
        else
        {
            isvulnerable = true;
            sharedInstance.gameObject.GetComponent<SpriteRenderer>().color =
                normal;
        }

        Vector2 previousPosition = currentPosition;
        currentPosition = transform.position;
        currentDirection = (currentPosition - previousPosition).normalized;

        //Debug.Log(currentDirection);
        // UpdateState();
        //Debug.Log(currentDirection);
        dashtimer -= 1;

        //Debug.Log(dashtimer);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateState();

        movement.Normalize();
        sharedInstance
            .gameObject
            .GetComponent<Rigidbody2D>()
            .MovePosition(transform.position +
            new Vector3(movement.x * Time.deltaTime * playerspeed,
                movement.y * Time.deltaTime * playerspeed,
                0));

        //transform.position +=  ;
        //UpdateState();
    }
}
