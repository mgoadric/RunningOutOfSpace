﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuggageMaker : MonoBehaviour
{

    public GameObject[] shapes;
    public GameObject unclaimed;
    public float speed = 1F;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine("MakeLuggage");
    }

    public void TurnOn() {
        StartCoroutine("MakeLuggage");       
    }

    public void TurnOff() {
        StopCoroutine("MakeLuggage");
    }

    IEnumerator MakeLuggage()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            int which = Random.Range(0, shapes.Length);
            if (!unclaimed.GetComponent<BeltPiece>().luggage)
            {
                unclaimed.GetComponent<BeltPiece>().luggage = Instantiate<GameObject>(shapes[which]);
                unclaimed.GetComponent<BeltPiece>().luggage.GetComponent<Luggage>().NewBelt(unclaimed);
                GameMaker.S.time = 10;
                StopCoroutine("Countdown");

                StartCoroutine("Countdown");
            }

        }
    }

    IEnumerator Countdown() {
        while (GameMaker.S.time > 0) 
        {
            yield return new WaitForSeconds(1);
            GameMaker.S.time -= 1;

        }
        GameMaker.S.StopGame(); 
    }

    // Update is called once per frame
    void Update()
    {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1 && !bp.luggage)
        {
            Debug.Log("Turning on.");
            bp.active = true;
            bp.mostRecent = unclaimed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1 && bp.active)
        {
            Debug.Log("Turning off.");
            bp.active = false;
            bp.mostRecent = null;
        }
    }
}
