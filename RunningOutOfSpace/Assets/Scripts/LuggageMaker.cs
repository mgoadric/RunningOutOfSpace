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
    public AudioClip luggageDrop;
    public AudioClip gameover;
    public AudioSource maudio;
    public GameObject firstBelt;

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
                
                //GetComponent<AudioSource>().Play();
                maudio.PlayOneShot(luggageDrop, 0.7F);
                if (firstBelt.GetComponent<SplineController>().Full())
                {
                    GameMaker.S.StopGame();
                    maudio.PlayOneShot(gameover, 0.7F);
                }
                else {
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            yield return new WaitForSeconds(speed);
        }
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
            //Debug.Log("Turning on.");
            bp.active = true;
            bp.mostRecent = unclaimed;
            if (unclaimed) {
                bp.TransferLuggage();
                GetComponent<BoxCollider2D>().enabled = false;

            }
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
