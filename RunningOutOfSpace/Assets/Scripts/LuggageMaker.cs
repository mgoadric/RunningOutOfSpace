using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageMaker : MonoBehaviour
{

    public GameObject[] shapes;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MakeLuggage());
    }

    IEnumerator MakeLuggage()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
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
        if (bp && bp.level == 0)
        {
            Debug.Log("Turning on.");
            bp.active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 0)
        {
            Debug.Log("Turning off.");
            bp.active = false;
        }
    }
}
