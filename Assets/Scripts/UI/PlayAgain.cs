using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class PlayAgain : MonoBehaviour
{
    public GameObject next;
    public GameObject playAgain;
    public Button nextButton;


    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    // Start is called before the first frame update
    void Awake()
    {

        playAgain.SetActive(false);
        nextButton.onClick.AddListener(OnNextPressed);
        StartCoroutine(ReadKills());
    }

    void OnNextPressed()
    {
        playAgain.SetActive(true);
        next.SetActive(false);

    }

    private IEnumerator ReadKills()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            string kills = snapshot.Child("kills").Value.ToString();
            StartCoroutine(UpdateKills(kills));
            Debug.Log("Former Kills is " + kills);
        }
    }

    private IEnumerator UpdateKills(string kills)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        int formerKills1 = int.Parse(kills);
        int totalKills = formerKills1 + 9;
        string newKills = totalKills.ToString();
        
        var DBTask2 = DBreference.Child("users").Child(User.UserId).Child("kills").SetValueAsync(newKills);


        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);

        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            //Kills are now updated
            Debug.Log("New kills is " + newKills);

        }
    }

   
}
