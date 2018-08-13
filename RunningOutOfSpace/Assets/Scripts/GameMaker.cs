using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaker : MonoBehaviour {

    public static GameMaker S;

    public bool playing;
    public int score;
    public GameObject luggagemaker;
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;

    // Use this for initialization
    void Awake()
    {
        S = this;
        playing = false;

    }

    // Use this for initialization
	void Start () {
	}

    public void GoGame() {
        StartCoroutine("PlayTheGame");
        playing = true;
        score = 0;
    }

    public void StopGame() {
        StopCoroutine("PlayTheGame");
        playing = false;
        luggagemaker.GetComponent<LuggageMaker>().TurnOff();
        c1.GetComponent<SplineController>().KillBelt();
        c2.GetComponent<SplineController>().KillBelt();
        c3.GetComponent<SplineController>().KillBelt();
    }
	
    public void IncScore() {
        lock (this) {
            score += 1;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PlayTheGame() {
        yield return new WaitForSeconds(2);
        luggagemaker.GetComponent<LuggageMaker>().TurnOn();
        c1.GetComponent<SplineController>().FollowSpline();
        c2.GetComponent<SplineController>().FollowSpline();
        c3.GetComponent<SplineController>().FollowSpline();
    }
}
