using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour


{
    public bool dialogueRunning;
    public Text dialogueText;
    
    private Animator dialogueBoxAnimator;
    private Queue<string> sentences; 
    private Coroutine currentTypeSentence; 

    void Start()
    {
        dialogueBoxAnimator = GetComponent<Animator>();
        sentences = new Queue<string>();
        dialogueRunning = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if left mouse button is pressed
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueRunning = true;
        dialogueBoxAnimator.SetBool("isOpen", true); // running fading in
                                                     // animation for dialogue
                                                     // box and text

        sentences.Clear(); 

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        if (currentTypeSentence != null) 
        {
            StopCoroutine(currentTypeSentence); 
        }
        currentTypeSentence = StartCoroutine(TypeSentence(sentences.Dequeue()));
        
    }

    IEnumerator TypeSentence(string sentence)
    
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; 
        }
    }

    private void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("isOpen", false); // fading out dialogue
                                                      // box and text
        dialogueRunning = false;
    }

    public void HaltDialogue()
    {
        sentences.Clear(); 

        StopCoroutine(currentTypeSentence); // stopping the typing

        EndDialogue();
    }


}
