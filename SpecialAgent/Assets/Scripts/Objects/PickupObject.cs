using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupObject : MonoBehaviour, IPickable
{
    [Header("Set Text for Pickup")]
    public string dialogueText;
    public string itemName;
    public int itemValue;
    private TextMeshProUGUI textBox;
    private float countdown = 2.5f;
    private MeshRenderer mesh;
    private MeshCollider mc;
    private bool isPickup;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        mesh = gameObject.GetComponent<MeshRenderer>();
        mc = gameObject.GetComponent<MeshCollider>();
        isPickup = false;
    }

    public void Pickup() {
        textBox.SetText(dialogueText);
        isPickup = true;
        mesh.enabled = false;
        mc.enabled = false;
        GameObject playerInventory = GameObject.Find("Inventory");
        playerInventory.GetComponent<Inventory>().addItem(this.itemName, this.itemValue);
    }

    void CountDown() { // Pickup message disappear after CountDown
        if (countdown > 0) {
            countdown -= Time.deltaTime;
        }
        if (countdown < 0) {
            textBox.SetText("");
            Destroy(gameObject, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickup) {
            CountDown();
        }
    }
}
