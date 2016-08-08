using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public int cameraShift = 1;
    public int cameraMove = 0;
    public const int RIGHT_ONE_SCREEN = 16;
    public const int LEFT_ONE_SCREEN = -16;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(cameraMove > 0)
        {
            transform.Translate(cameraShift, 0, 0);
            cameraMove--;
        }
        if(cameraMove < 0)
        {
            transform.Translate(-cameraShift, 0, 0);
            cameraMove++;
        }
	}

    public void screenTransition(bool exitRight)
    {
        if (exitRight && Enemy.enemiesAlive <= 0)
        {
            cameraMove = RIGHT_ONE_SCREEN;
        } 
        else if(Enemy.enemiesAlive <= 0)
        {
            cameraMove = LEFT_ONE_SCREEN;
        }
    }
}
