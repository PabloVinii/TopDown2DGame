using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    
    public string[] dialogueNPC;
    public int dialogueIndex;
    
    public GameObject dialoguePanel;
    public Text dialogueText;

    public Text nameNPC;
    public Image imageNPC;
    public Sprite spriteNPC;

    public bool readyToSpeak;
    public bool startDialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && readyToSpeak)
        if (!startDialogue)
        {
            FindObjectOfType<PlayerMovement>().speed = 0f;
            StartDialogue();
        }
        else if (dialogueText.text == dialogueNPC[dialogueIndex])
        {
            NextDialogue();
        }
    }


    void NextDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex < dialogueNPC.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            FindObjectOfType<PlayerMovement>().speed = 5f;
        }
    }

    void StartDialogue()
    {
        nameNPC.text = "Slime";
        imageNPC.sprite = spriteNPC;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueNPC[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
}
