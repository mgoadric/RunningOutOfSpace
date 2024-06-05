using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour {

	int lastScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
        gt.SetText("Score:\n" + GameMaker.S.score);
		if (GameMaker.S.score != lastScore) {
            StopCoroutine("EmbiggenScore");
            StartCoroutine("EmbiggenScore");
		}
		lastScore = GameMaker.S.score;
	}

	   IEnumerator EmbiggenScore() {

        float scale = 1.1f;
        while (scale > 1) {
            transform.localScale = scale * Vector3.one;
            scale *= 0.99f;
            yield return new WaitForSeconds(0.02f);
        }
        transform.localScale = Vector3.one;

    }
}
