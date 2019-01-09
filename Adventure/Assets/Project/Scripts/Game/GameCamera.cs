using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {


    public Player player;
    public Vector3 offset;
    public float focusSpeed = 1f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
              transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * focusSpeed);
            if (player.JustTeleported)
            {
                transform.position = player.transform.position + offset;
            }
        }
        
	}
}
