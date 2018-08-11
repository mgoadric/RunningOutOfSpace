using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageMaker : MonoBehaviour
{

    public GameObject[] shapes;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        while (true)
        {
            int which = Random.Range(0, shapes.Length);
            GameObject lug = Instantiate<GameObject>(shapes[which]);

            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
	}
}
