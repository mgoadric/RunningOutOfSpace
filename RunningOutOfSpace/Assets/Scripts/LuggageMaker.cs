using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageMaker : MonoBehaviour
{

    public GameObject[] shapes;
    public int speed = 3;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(MakeLuggage());
    }

    IEnumerator MakeLuggage()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            int which = Random.Range(0, shapes.Length);
            GameObject lug = Instantiate<GameObject>(shapes[which]);

        }
    }

    // Update is called once per frame
    void Update()
    {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1)
        {
            Debug.Log("Turning on.");
            bp.active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1)
        {
            Debug.Log("Turning off.");
            bp.active = false;
        }
    }
}
