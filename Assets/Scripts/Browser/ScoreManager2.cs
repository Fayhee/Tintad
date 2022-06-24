using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager2 : MonoBehaviour

{
    public LearningOutcome[] learningOutcomes;
    public Text scoreText;
    public DialogueManager dialogueManager;

    private int score;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString(); 
    }

    public void AddPosInteraction(string learningOutcome)
    {
        foreach (LearningOutcome lo in learningOutcomes)
        {
            if (lo.name == learningOutcome)
            {
                lo.posInteractions += 1;
                score += 1; 
                scoreText.text = "Score: " + score.ToString(); // updating
                                                               // score

                if (lo.posInteractions == 1)
                {
                    dialogueManager.StartDialogue(lo.approval);
                }
                else
                {
                    dialogueManager.HaltDialogue(); 
                }
            }
        }
    }

    public void AddNegInteraction(string learningOutcome)
    {
        foreach (LearningOutcome lo in learningOutcomes)
        {
            if (lo.name == learningOutcome)
            {
                lo.negInteractions += 1;
                
                if (lo.negInteractions == 1)
                {
                    dialogueManager.StartDialogue(lo.warning);
                }
                else
                {
                    dialogueManager.HaltDialogue(); 

                    score -= 1; 
                    scoreText.text = "Score: " + score.ToString(); 
                }
            }
        }
    }

}
