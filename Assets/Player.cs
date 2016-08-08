using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {



    private Bullet modelBullet;
    private new GameCamera camera;
    private System.Collections.Generic.List<Bullet> bb;
    private int bbCount;
    private static int current = 0;
    public Vector3 offscreenHoldingArea = new Vector3(-100, -100, 0);
    public int bulletTimer = 0;
    public int bulletTimerThreshold = 20;
    public int secondJumpMinTime;
    public int rollInvincThresh; //How long before the player is vulnerable again
    public int rollCoolTime; //How long before roll is reusable
    public int rollSpeedThresh; //How long the speed from roll stays up
    private int rollTimer = 0;
   

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool secondJump = true;
    [HideInInspector]
    public int jumpTimer = 0;
    public float moveForce = 365f;
    public int rollForce;
    public float maxSpeed = 5f;
    public float jumpForce = 100f;
    public Transform groundCheck;
    [HideInInspector]
    public bool grounded = false;
    private Rigidbody2D thisBody;
    // Use this for initialization
    void Start()
    {
        _loadBullets();
        thisBody = (Rigidbody2D)GameObject.Find("Player").GetComponent<Rigidbody2D>();
        camera = (GameCamera)GameObject.Find("Main Camera").GetComponent<GameCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if(grounded)
        {
            secondJump = true;
        }
        if(rollTimer < rollInvincThresh)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        }
        readKeys();
        bulletTimer++;
        rollTimer--;

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if(h * thisBody.velocity.x < maxSpeed)
        {
            thisBody.AddForce(Vector2.right * h * moveForce);
        }
        if(Mathf.Abs(thisBody.velocity.x) > maxSpeed && rollTimer < rollSpeedThresh)
        {
            thisBody.velocity = new Vector2(.2f * (Mathf.Sign(thisBody.velocity.x) * maxSpeed), thisBody.velocity.y);
        }
        if(h > 0 && !facingRight)
        {
            Flip();
        }
        else if(h < 0 && facingRight)
        {
            Flip();
        }
        if(jump)
        {
            thisBody.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            jumpTimer = 0;
        }
        jumpTimer++;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void _loadBullets()
    {
        modelBullet = (Bullet)GameObject.Find("modelBullet").GetComponent("Bullet");
        modelBullet.transform.position = offscreenHoldingArea;
        bb = new System.Collections.Generic.List<Bullet>();

        for (int i = 0; i < 50; i++)
        {
            GameObject newy = Instantiate(modelBullet.gameObject);
            newy.transform.parent = GameObject.Find("Bullets").transform;
            newy.name = "bb" + i;
            CircleCollider2D cc = newy.AddComponent<CircleCollider2D>();
            Rigidbody2D rb = newy.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.mass = 0;
            rb.angularDrag = 0;
            rb.isKinematic = false;
            bb.Add((Bullet)newy.GetComponent("Bullet"));
        }
        bbCount = bb.Count;
    }

    void readKeys()
    {
        if(Input.GetKeyDown(KeyCode.X) && grounded)
        {
            jump = true;
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            if (jumpTimer > secondJumpMinTime && secondJump)
            {
                thisBody.velocity = new Vector2(thisBody.velocity.x, 0);
                thisBody.AddForce(new Vector2(0f, jumpForce));
                secondJump = false;

            }
        }

        //Roll input: makes character invincible for a while and quickly move
        if(Input.GetKeyDown(KeyCode.C) && rollTimer < 0)
        {
            thisBody.velocity = new Vector2(0, thisBody.velocity.y);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            if (facingRight)
            {
                thisBody.AddForce(Vector2.right * rollForce);
            }
            else
            {
                thisBody.AddForce(Vector2.left * rollForce);
            }
            rollTimer = rollCoolTime;
        }
        if (Input.GetKeyDown(KeyCode.Z) && bulletTimer > bulletTimerThreshold)
        {
            if (facingRight)
            {
                makeBulletHere(transform.position +
                    new Vector3(.3f, 0, 0), facingRight);
            }
            else
            {
                makeBulletHere(transform.position +
                    new Vector3(-.3f, 0, 0), facingRight);
            }
            bulletTimer = 0;
        }
    }


    void makeBulletHere(Vector3 startPos, bool aimingRight)
    {
        current++;
        if (current >= bbCount)
        {
            current = 0;
        }
        bb[current].transform.position = startPos;
        bb[current].transform.rotation = new Quaternion(0, 0, 0, 0);
        bb[current].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (aimingRight)
        {
            bb[current].direction = 1;
        }
        else
        {
            bb[current].direction = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("ee") && rollTimer < rollInvincThresh)
        {
            transform.position = offscreenHoldingArea;
        }
    }

    void OnBecameInvisible()
    {
        camera.screenTransition(thisBody.velocity.x > 0);
    }



}
