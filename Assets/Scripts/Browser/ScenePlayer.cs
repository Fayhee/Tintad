using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ScenePlayer : MonoBehaviour

{
    // all objects required for First Cutscene 
    public DialogueManager dialogueManager;
    public Dialogue livingRoomSceneDialogue;
    public Dialogue bedroomSceneDialogue;
    public GameObject livingRoomScene;
    public GameObject bedroomScene;

    // all objects required for Second Cutscene
    public Dialogue endSecondSceneDialogue;
    public CanvasGroup browserCanvasGroup;
    public GameObject scoreBox;
    public GameObject budgetBox;

    private PlayableDirector pd;
    private bool secondCutscenePlayed;

    void Start()
    {
        secondCutscenePlayed = false;
        pd = GetComponent<PlayableDirector>();

        StartCoroutine(FirstCutscene());
    }

    IEnumerator FirstCutscene()
    {
        yield return new WaitForSeconds(1);

        dialogueManager.StartDialogue(livingRoomSceneDialogue);

        while (dialogueManager.dialogueRunning)
        {
            yield return null; // waiting a single frame
        }

        bedroomScene.SetActive(true); // enabling all objects required by
                                      // bedroom scene
        bedroomScene.GetComponent<Animator>().SetTrigger("FadeInTrigger");
        // running fading in animation on all of them

        yield return new WaitForSeconds(2);

        livingRoomScene.SetActive(false); // disabling all objects required by
                                          // living room scene as they are no
                                          // longer needed

        dialogueManager.StartDialogue(bedroomSceneDialogue);

        while (dialogueManager.dialogueRunning)
        {
            yield return null;
        }

        bedroomScene.GetComponent<CanvasGroup>().interactable = true;
        // enabling interaction at the end of First Scene
    }

    public void PlaySecondCutscene()
    {
        if (!secondCutscenePlayed)
        {
            secondCutscenePlayed = true;
            StartCoroutine(SecondCutscene());
        }
    }

    IEnumerator SecondCutscene()
    {
        pd.Play();
        yield return new WaitForSeconds((float)pd.duration); 

        dialogueManager.StartDialogue(endSecondSceneDialogue);
        while (dialogueManager.dialogueRunning)
        {
            yield return null; // waiting a single frame
        }

        browserCanvasGroup.interactable = true;
        scoreBox.SetActive(true);
        budgetBox.SetActive(true);
    }


}


