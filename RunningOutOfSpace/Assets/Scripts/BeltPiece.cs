using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPiece : MonoBehaviour {

    public int level;
    public GameObject luggage;
    public GameObject shapesprite;
    public bool active = false;
    public Shape shape;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (shapesprite)
        {
            if (active) { 
                shapesprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                shapesprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            }
        }    		
	}

    public void WipeOut() {
        luggage = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (!luggage)
            {
                Debug.Log("Looking for luggage");
                var lug = collision.gameObject.GetComponent<Luggage>();
                if (lug && lug.level < level && (shape == Shape.RING || shape == lug.shape))
                {
                    collision.gameObject.transform.parent = transform;
                    luggage = collision.gameObject;
                    lug.NewBelt(this.gameObject);
                    Debug.Log("hit luggage!");
                }
            }
        } else {
            var obp = collision.gameObject.GetComponent<BeltPiece>();
            if (obp && obp.level < level && 
                obp.luggage && obp.luggage.GetComponent<Luggage>().shape == shape) {
                    active = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active) {
            var obp = collision.gameObject.GetComponent<BeltPiece>();
            if (obp && obp.level < level &&
                obp.luggage && obp.luggage.GetComponent<Luggage>().shape == shape)
            {
                active = false;
            }

        }
    }

}
