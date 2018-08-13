using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeltPiece : MonoBehaviour {

    public int level;
    public GameObject luggage;
    public GameObject shapesprite;
    public TextMeshPro score;
    public bool active = false;
    public GameObject mostRecent;
    public Shape shape;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (shapesprite)
        {
            if (active && mostRecent && mostRecent.gameObject.GetComponent<BeltPiece>().luggage) { 
                shapesprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                LineRenderer line = GetComponent<LineRenderer>();
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, mostRecent.gameObject.GetComponent<BeltPiece>().luggage.transform.position);
            }
            else
            {
                shapesprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
                LineRenderer line = GetComponent<LineRenderer>();
                line.enabled = false;
            }
        }    		
	}

    private void OnMouseDown()
    {
        if (active && mostRecent)
        {
            var lug = mostRecent.gameObject.GetComponent<BeltPiece>().luggage;
            if (lug)
            {
                lug.transform.parent = transform;
                luggage = lug;
                luggage.GetComponent<Luggage>().NewBelt(this.gameObject);
                Debug.Log("hit luggage!");
                active = false;
                mostRecent = null;
            }
        }
    }

    public void WipeOut() {
        luggage = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var obp = collision.gameObject.GetComponent<BeltPiece>();
        if (!luggage && obp && obp.level < level &&
            obp.luggage && obp.luggage.GetComponent<Luggage>().shape == shape)
        {
            active = true;
            mostRecent = collision.gameObject;
        }

        //if (active)
        //{
        //    if (!luggage)
        //    {
        //        Debug.Log("Looking for luggage");
        //        var obp = collision.gameObject.GetComponent<BeltPiece>();
        //        if (!luggage && obp && obp.level < level &&
        //            obp.luggage && obp.luggage.GetComponent<Luggage>().shape == shape)
        //        {
        //            collision.gameObject.transform.parent = transform;
        //            luggage = collision.gameObject;
        //            obp.luggage.GetComponent<Luggage>().NewBelt(this.gameObject);
        //            Debug.Log("hit luggage!");
        //        }
        //    }
        //} else {
        //    var obp = collision.gameObject.GetComponent<BeltPiece>();
        //    if (!luggage && obp && obp.level < level && 
        //        obp.luggage && obp.luggage.GetComponent<Luggage>().shape == shape) {
        //            active = true;

        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obp = collision.gameObject.GetComponent<BeltPiece>();
        if (collision.gameObject == mostRecent)
        {
            active = false;
            mostRecent = null;
        }
    }

}
