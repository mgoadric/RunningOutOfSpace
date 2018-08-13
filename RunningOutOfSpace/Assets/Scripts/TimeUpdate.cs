using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextMeshPro gt = this.GetComponent<TextMeshPro>();
        gt.SetText("" + GameMaker.S.time);      
		
	}
}
