using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    private bool playerDetected;
    [SerializeField] private Dialouge dialougeScript;

    //Detect trigger with Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If We trigger the player enable playerDetected and show indicator
        if (collision.tag == "Character")
        {
            playerDetected = true;
            dialougeScript.ToggleIndicator(playerDetected);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger with the player disable playerDetected and hide indicator
        if (collision.tag == "Character")
        {
            playerDetected = false;
            dialougeScript.ToggleIndicator(playerDetected);
            dialougeScript.EndDialogue();
        }   
    }
    //While detected if we interact start the dialouge
    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialougeScript.StartDialouge();
        }
    }
}
