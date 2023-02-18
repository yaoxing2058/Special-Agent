using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvestigateObject : MonoBehaviour, IInteractable
{
    [Header("Set Text for Interaction")]
    public string dialogueText;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
    }

    public void Interact() {

        textBox.SetText(dialogueText);
    }

    public void Cancel() {

        textBox.SetText("");
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
