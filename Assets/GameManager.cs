using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Vector3 offscreenHoldingArea;
    private Enemy modelEnemy;
    private Turret modelTurret;
    private BoundingWalls modelWall;
    private System.Collections.Generic.List<Enemy> ee;
    private System.Collections.Generic.List<BoundingWalls> walls;
    private System.Collections.Generic.List<Turret> turrets;
    private int currentEnemy = 0;
    private int currentTurret = 0;
    private int allEnemies = 5;
    private int currentWall = 0;
    private int allWalls = 4;
	// Use this for initialization
	void Start () {
        offscreenHoldingArea = new Vector3(1000, -1000, 0);
        Debug.Log("Modifying game environment");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        //Building Enemy Pools
        buildEnemyPools();
        buildWallPools();
        buildFirstLevel();

    }

    void buildEnemyPools()
    {
        modelEnemy = (Enemy)GameObject.Find("modelEnemy").GetComponent<Enemy>();
        ee = new System.Collections.Generic.List<Enemy>();
        for (int i = 0; i < allEnemies; i++)
        {
            GameObject newy = Instantiate(modelEnemy.gameObject);
            newy.transform.parent = GameObject.Find("Enemies").transform;
            newy.name = "ee" + i;
            ee.Add((Enemy)(newy.GetComponent("Enemy")));
        }
        modelTurret = (Turret)GameObject.Find("modelTurret").GetComponent<Turret>();
        turrets = new System.Collections.Generic.List<Turret>();
        for(int i = 0; i < allEnemies; i++)
        {
            GameObject newy = Instantiate(modelTurret.gameObject);
            newy.transform.parent = GameObject.Find("Turrets").transform;
            newy.name = "tt" + i;
            turrets.Add((Turret)newy.GetComponent("Turret"));
        }

    }

    void buildWallPools()
    {
        modelWall = (BoundingWalls)GameObject.Find("modelWall").GetComponent<BoundingWalls>();
        walls = new System.Collections.Generic.List<BoundingWalls>();
        for (int i = 0; i < allWalls; i++)
        {
            GameObject newy = Instantiate(modelWall.gameObject);
            newy.transform.parent = GameObject.Find("Walls").transform;
            newy.name = "Wall" + i;
            walls.Add((BoundingWalls)(newy.GetComponent("BoundingWalls")));
        }
    }

    void buildFirstLevel()
    {
        currentEnemy++;
        if(currentEnemy > allEnemies)
        {
            currentEnemy = 0;
        }
        ee[currentEnemy].transform.position = new Vector3(3, -3, 0);
        ee[currentEnemy].enemyHealth = 20;
        ee[currentEnemy].enemyAlive = true;
        ee[currentEnemy].moving = true;
        Enemy.enemiesAlive++;
        currentTurret++;
        if(currentTurret > allEnemies)
        {
            currentTurret = 0;
        }
        turrets[currentTurret].transform.position = new Vector3(4, 1f, 0);
        turrets[currentTurret].enemyHealth = 20;
        turrets[currentTurret].facingRight = false;
        turrets[currentTurret].enemyAlive = true;
        Enemy.enemiesAlive++;
        currentWall++;
        if(currentWall > allWalls)
        {
            currentWall = 0;
        }
        walls[currentWall].transform.position = new Vector3(-8, 0, 0);
        currentWall++;
        if(currentWall > allWalls)
        {
            currentWall = 0;
        }
        walls[currentWall].transform.position = new Vector3(8, 0, 0);
    }

    public void buildSecondLevel()
    {
        currentEnemy++;
        if (currentEnemy >= allEnemies)
        {
            currentEnemy = 0;
        }
        ee[currentEnemy].transform.position = new Vector3(20, -3, 0);
        ee[currentEnemy].enemyHealth = 20;
        ee[currentEnemy].enemyAlive = true;
        ee[currentEnemy].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        currentWall++;
        if (currentWall > allWalls)
        {
            currentWall = 0;
        }
        walls[currentWall].transform.position = new Vector3(8, 0, 0);
        currentWall++;
        if (currentWall > allWalls)
        {
            currentWall = 0;
        }
        walls[currentWall].transform.position = new Vector3(24, 0, 0);
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
