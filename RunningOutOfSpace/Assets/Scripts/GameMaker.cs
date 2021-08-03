using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaker : MonoBehaviour {

    public static GameMaker S;

    public bool playing;
    public int score;
    public int highscore;
    public GameObject luggagemaker;
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject startbutton;

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
        startbutton.SetActive(false);
        luggagemaker.GetComponent<LuggageMaker>().TurnOn();
        c1.GetComponent<SplineController>().KillBelt();
        c2.GetComponent<SplineController>().KillBelt();
        c3.GetComponent<SplineController>().KillBelt();
        c1.GetComponent<SplineController>().FollowSpline();
        c2.GetComponent<SplineController>().FollowSpline();
        c3.GetComponent<SplineController>().FollowSpline();
        playing = true;
        score = 0;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    public void StopGame() {
        startbutton.SetActive(true);
        playing = false;
        luggagemaker.GetComponent<LuggageMaker>().TurnOff();
        c1.GetComponent<SplineController>().DeactivateBelt();
        c2.GetComponent<SplineController>().DeactivateBelt();
        c3.GetComponent<SplineController>().DeactivateBelt();
        GetComponent<AudioSource>().Stop();
        if (score > highscore)
        {
            highscore = score;
        }

    }
	
    public void IncScore(int x) {
        lock (this) {
            if (playing)
            {
                score += x;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
