using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private Shot modelShot;
    public Vector3 offscreenHoldingArea = new Vector3(-1000, -1000, 0);
    private int shotTimer = 0;
    private const int shotThreshold = 200;
    public int enemyHealth = 0;
    public bool facingRight = true;
    public bool enemyAlive = true;
    public Vector3 scaling = new Vector3(.4f, .4f, 1);
    private System.Collections.Generic.List<Shot> ss;
    private int ssCount;
    private int nextShot;
    // Use this for initialization
    void Start () {
        _loadAmmo();
	}
	
	// Update is called once per frame
	void Update () {
        Point();
	    if(enemyHealth < 0 && enemyAlive)
        {
            transform.position = offscreenHoldingArea;
            Enemy.enemiesAlive--;
            Debug.Log("Enemies Alive: " + Enemy.enemiesAlive);
            enemyAlive = false;
        }
        if(shotTimer > shotThreshold)
        {
            if (facingRight)
            {
                makeShotHere(transform.position +
                    new Vector3(1f, 0, 0), facingRight);
            }
            else
            {
                makeShotHere(transform.position +
                    new Vector3(-1f, 0, 0), facingRight);
            }
            shotTimer = 0;
        }
        shotTimer++;
	}

    void makeShotHere(Vector3 spawnLocation, bool facingRight)
    {
        nextShot++;
        if(nextShot >= ssCount)
        {
            nextShot = 0;
        }
        ss[nextShot].transform.position = spawnLocation;
        ss[nextShot].transform.rotation = new Quaternion(0, 0, 0, 0);
        ss[nextShot].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ss[nextShot].facingRight = facingRight;
        ss[nextShot].moving = true;
    }

    void Point()
    {
        if(facingRight)
        {
            transform.localScale = new Vector3(scaling.x * -1, scaling.y, scaling.z);
        }
        else
        {
            transform.localScale = new Vector3(scaling.x, scaling.y, scaling.z);
        }
    }

    void _loadAmmo()
    {
        modelShot = (Shot)GameObject.Find("modelEnemyShot").GetComponent("Shot");
        modelShot.transform.position = offscreenHoldingArea;
        ss = new System.Collections.Generic.List<Shot>();

        for (int i = 0; i < 5; i++)
        {
            GameObject newy = Instantiate(modelShot.gameObject);
            newy.transform.parent = GameObject.Find("Shots").transform;
            newy.name = "ss" + i;
            CircleCollider2D cc = newy.AddComponent<CircleCollider2D>();
            cc.isTrigger = true;
            Rigidbody2D rb = newy.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.mass = 0;
            rb.angularDrag = 0;
            rb.isKinematic = false;
            ss.Add((Shot)newy.GetComponent("Shot"));
        }
        ssCount = ss.Count;
    }

    void onBecameVisible()
    {
    }
}
