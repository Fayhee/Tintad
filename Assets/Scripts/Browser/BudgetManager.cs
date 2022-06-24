using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetManager : MonoBehaviour
{
    public Text budgetText;
    public DialogueManager dialogueManager;
    public Dialogue penaltyDialogue;

    private int budget;
    
    void Start()
    {
        budget = 10000;
        budgetText.text = "Budget: " + budget.ToString(); 
    }

    public void Penalize(int penalty)
    {
        StartCoroutine(DelayedPenalize(penalty));
    }

    public void Buy(int price)
    {
        Debug.Log("buying");

        budget -= price;
        budgetText.text = "Budget: " + budget.ToString(); 
    }

    IEnumerator DelayedPenalize(int penalty)
    {
        Debug.Log("penalizing");
        
        yield return new WaitForSeconds(3);

        budget -= penalty;
        budgetText.text = "Budget: " + budget.ToString(); 

        dialogueManager.StartDialogue(penaltyDialogue);
    }
}
