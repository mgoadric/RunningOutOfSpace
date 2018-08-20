using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuggageMaker : MonoBehaviour
{

    public GameObject[] shapes;
    public bool[] chosen;
    public GameObject unclaimed;
    public float speed = 1F;
    public static int TIMELEFT = 5;
    public AudioClip luggageDrop;
    public AudioClip timer;
    public AudioClip gameover;
    public AudioSource maudio;

    // Use this for initialization
    void Start()
    {
        chosen = new bool[shapes.Length];
        //StartCoroutine("MakeLuggage");
        maudio = GetComponent<AudioSource>();
    }

    public void TurnOn() {
        StartCoroutine("MakeLuggage");       
    }

    public void TurnOff() {
        StopCoroutine("MakeLuggage");
        var lug = unclaimed.GetComponent<BeltPiece>().luggage;
        if (lug) {
            Destroy(lug.gameObject);
            unclaimed.GetComponent<BeltPiece>().luggage = null;
        }
    }

    IEnumerator MakeLuggage()
    {
        GameMaker.S.time = TIMELEFT;

        yield return new WaitForSeconds(2f);
        while (true)
        {
            bool all = true;
            for (int i = 0; i < chosen.Length; i++) {
                if (!chosen[i]){
                    all = false;
                    break;
                }
            }
            if (all) {
                chosen = new bool[shapes.Length];
            }
            int which = 0;
            while (chosen[which])
            {
                which = Random.Range(0, shapes.Length);
            }
            if (!unclaimed.GetComponent<BeltPiece>().luggage)
            {
                chosen[which] = true;

                unclaimed.GetComponent<BeltPiece>().luggage = Instantiate<GameObject>(shapes[which]);
                unclaimed.GetComponent<BeltPiece>().luggage.GetComponent<Luggage>().NewBelt(unclaimed);
                GameMaker.S.time = TIMELEFT;
                StopCoroutine("Countdown");

                StartCoroutine("Countdown");
                //GetComponent<AudioSource>().Play();
                maudio.PlayOneShot(luggageDrop, 0.7F);
            }
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator Countdown() {
        while (GameMaker.S.time > 0) 
        {
            yield return new WaitForSeconds(1);
            GameMaker.S.time -= 1;
            maudio.PlayOneShot(timer, 0.7F);

        }
        GameMaker.S.StopGame(); 
        maudio.PlayOneShot(gameover, 0.7F);
    }

    // Update is called once per frame
    void Update()
    {
        if (!unclaimed.GetComponent<BeltPiece>().luggage)
        {
            StopCoroutine("Countdown");

        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1 && !bp.luggage)
        {
            //Debug.Log("Turning on.");
            bp.active = true;
            bp.mostRecent = unclaimed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var bp = collision.gameObject.GetComponent<BeltPiece>();
        if (bp && bp.level == 1 && bp.active)
        {
            //Debug.Log("Turning off.");
            bp.active = false;
            bp.mostRecent = null;
        }
    }
}
