using UnityEngine;
using System.Collections;

public class BoundingWalls : MonoBehaviour
{
    private Vector3 offscreenHoldingArea;
    private Collider2D thisColl;
    private int deathCounter = 0;
    // Use this for initialization
    void Start()
    {
        offscreenHoldingArea = new Vector3(-1000, 0, 0);
        thisColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Enemy.enemiesAlive == 0 && deathCounter > 4)
        {
            this.transform.position = offscreenHoldingArea;
        }
        deathCounter++;
    }

    void OnBecameVisible()
    {
        deathCounter = 0;
    }


}
