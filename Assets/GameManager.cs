using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Vector3 offscreenHoldingArea;
    private Enemy modelEnemy;
    private BoundingWalls modelWall;
    private System.Collections.Generic.List<Enemy> ee;
    private System.Collections.Generic.List<BoundingWalls> walls;
    private int currentEnemy = 0;
    private int allEnemies = 10;
    private int currentWall = 0;
    private int allWalls = 4;
	// Use this for initialization
	void Start () {
        offscreenHoldingArea = new Vector3(-1000, -1000, 0);
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
    
    // Update is called once per frame
    void Update () {
	
	}
}
