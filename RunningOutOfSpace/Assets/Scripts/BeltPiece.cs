using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPiece : MonoBehaviour {

    public int level;
    public GameObject luggage;
    public bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        		
	}

    public void WipeOut() {
        luggage = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && !luggage)
        {
            Debug.Log("Looking for luggage");
            var lug = collision.gameObject.GetComponent<Luggage>();
            if (lug && lug.level < level)
            {
                collision.gameObject.transform.parent = transform;
                luggage = collision.gameObject;
                lug.NewBelt(this.gameObject);
                Debug.Log("hit luggage!");
            }
        }
    }
}
