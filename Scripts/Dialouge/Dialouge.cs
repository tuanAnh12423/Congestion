using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
using System.Diagnostics;

public class Dialouge : MonoBehaviour
{
    //Window
    [SerializeField] private GameObject window;
    //Indicator
    [SerializeField] private GameObject indicator;
    //Dialogue List
    [SerializeField] private List<string> dialogues;
    //Text Component
    [SerializeField] private TMP_Text dialogueText;
    //Writing Speed
    [SerializeField] private float writingSpeed;    
    //Index on dialogue
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next boolean
    private bool waitForNext;
    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }
    //Start Dialogue
    public void StartDialouge()
    {
        if (started)
            return;
        //Boolean to indicator that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //Hide the Indicator
        ToggleIndicator(false);
        //start with first dialouge
        GetDialouge(0);
    }
    private void GetDialouge(int i)
    {
        //Start index at zero
        index = i;
        //reset the character index
        charIndex = 0;
        //Clear the dialouge component text
        dialogueText.text = string.Empty;
        //Start Writing
        StartCoroutine(Writing());
    }
    //End Dialogue
    public void EndDialogue()
    {
        //Started is disable
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all IEnumerators
        StopAllCoroutines();
        //Hide the window
        ToggleWindow(false);
    }
    //Writing Logic     
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);
        string currentDialogue = dialogues[index];
        //Write  the character
        dialogueText.text += currentDialogue[charIndex];
        //Increase the character index
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            //wait X seconds
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }
    private void Update()
    {
        if (!started)
            return;
        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialouge(index);
            }
            else
            {
                // If not end the dialogue process
                EndDialogue();
            }
        }
    }
}
