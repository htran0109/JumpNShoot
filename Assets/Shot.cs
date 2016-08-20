using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
    public bool facingRight = false;
    public bool moving = false;
    private Vector3 offscreenHoldingArea = new Vector3(-1000, -1000, 1);
    private Vector3 scaling = new Vector3(.2f, .2f, 1);
    public float shotSpeed = .05f;

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
        Point();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        if (moving)
        {
            if (facingRight)
            {
                transform.Translate(shotSpeed, 0, 0);
            }
            else
            {
                transform.Translate(-shotSpeed, 0, 0);
            }
        }
    }
    void Point()
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(scaling.x * -1, scaling.y, scaling.z);
        }
        else
        {
            transform.localScale = new Vector3(scaling.x, scaling.y, scaling.z);
        }

    }
    void onBecameVisible()
    {
        moving = true;

    }
}
