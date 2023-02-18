using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{

    private GameObject manager;
    private TextMeshProUGUI textBox;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
        player = manager.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.SetText("Life: " + player.health.ToString());
    }
}
