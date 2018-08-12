using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape {
    CIRCLE, HEART, STAR, TRIANGLE, MED
}

public class Luggage : MonoBehaviour {

    public GameObject ShapeSprite;
    public GameObject beltPiece;
    public Shape shape;
    public int level;

    public Vector3 lugStart;
    public float startTime;
    // Using https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

    // Movement speed in units/sec.
    public float speed = 1.0F;
    public bool settled = true;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (beltPiece)
        {

            if (!settled && Vector3.Distance(beltPiece.transform.position, transform.position) > 0.01)
            {
                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

                float journeyLength = Vector3.Distance(lugStart, beltPiece.transform.position);

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;
                Debug.Log(distCovered + ":" + fracJourney);
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(lugStart, beltPiece.transform.position, fracJourney);
            }
            else
            {
                transform.position = beltPiece.transform.position;
                settled = true;
            }
        }
	}

    public void NewBelt(GameObject belt) {
        if (beltPiece) {
            beltPiece.GetComponent<BeltPiece>().WipeOut();
        }
        beltPiece = belt;
        settled = false;
        startTime = Time.time;
        level = beltPiece.GetComponent<BeltPiece>().level;
        lugStart = new Vector3() + transform.position;
    }
}
