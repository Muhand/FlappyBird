using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public static float offsetX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
                MoveTheCamera();
        }
	}

    /// <summary>
    /// Move the camera to follow the player by getting the player's X postion
    /// </summary>
    void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX();
        transform.position = temp;
    }
}
