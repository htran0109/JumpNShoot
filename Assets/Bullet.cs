using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private bool moving = false;
    public float bulletSpeed = 1f;
    public Vector3 offscreenHoldingArea = new Vector3(-100, -100, 0);
    public int direction = 1;
    public Vector3 scaling = new Vector3(0.1f, 0.1f, 1f);
    // Use this for initialization
    void Start()
    {
        Debug.Log(GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        Point();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        if (moving)
        {
            transform.Translate(direction * bulletSpeed, 0, 0);
        }

    }

    void Point()
    {

        transform.localScale = direction * scaling;
    }
    void OnBecameInvisible()
    {
        transform.position = offscreenHoldingArea;
        moving = false;
    }

    void OnBecameVisible()
    {
        moving = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("ee"))
        {
            
            transform.position = offscreenHoldingArea;
            ((Enemy)coll.gameObject.GetComponent<Enemy>()).enemyHealth--;
        }
    }
}
