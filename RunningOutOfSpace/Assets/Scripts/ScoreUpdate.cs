using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
        gt.SetText("Score: " + GameMaker.S.score);		
	}
}
