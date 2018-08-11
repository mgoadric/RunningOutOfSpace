using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPiece : MonoBehaviour {

    public int level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = transform;
        Debug.Log("hit something!");
    }
}
