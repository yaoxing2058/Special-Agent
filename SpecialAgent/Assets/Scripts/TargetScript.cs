using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetScript : MonoBehaviour
{


    private TextMeshProUGUI textCanvas;

    public GameObject childCanvas;

    private int numberOfHits = 0;
    // Start is called before the first frame update
    void Start()
    {
        textCanvas = childCanvas.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onHit(){
        numberOfHits += 1;
        textCanvas.SetText("Hits: {0}", numberOfHits);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "FlareBullet") {
            Debug.Log("Hit!");
            this.onHit();
        }
    }

}
