using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public float enemySpeed = .2f;
    public Vector3 offscreenHoldingArea = new Vector3(-100, -100, 0);
    public int direction = 1; //1 = right, -1 = left
    public static int enemiesAlive;
    public Vector3 scaling = new Vector3(1f, 1f, 1f);
    public int enemyHealth;
    private Player player;
    private bool enemyAlive = false;
    private bool moving = false;
    // Use this for initialization
    void Start()
    {
        player = (Player)GameObject.Find("Player").GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        Point();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        if (moving == true)
        {
            if (player.grounded == true)
            {
                transform.Translate(direction * enemySpeed * 2, 0, 0);
            }
            else
            {
                transform.Translate(direction * enemySpeed, 0, 0);
            }
        }
        if(enemyHealth < 0 && enemyAlive)
        {
            transform.position = offscreenHoldingArea;
            enemiesAlive--;
            enemyAlive = false;
            moving = false;
            Debug.Log("Enemy Died");
        }

    }

    void Point()
    {

        transform.localScale = new Vector3(scaling.x * direction, scaling.y, scaling.z);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Enemy Hit Something");
        if (coll.gameObject.name.Contains("Wall"))
        {
            direction *= -1;

        }
    }

    void OnBecameVisible()
    {

        moving = true;
        enemyAlive = true;
        enemiesAlive++;
        Debug.Log("Enemy Spawned" + enemiesAlive);
    }
}