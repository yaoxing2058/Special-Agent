using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public GameObject getScore;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.SetText("Score: " + getScore.GetComponent<Score>().score.ToString());
    }
}
