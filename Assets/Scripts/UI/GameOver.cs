using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class GameOver : MonoBehaviour
{
    public GameObject next;
    public GameObject gameOver;
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
        gameOver.SetActive(false);
        nextButton.onClick.AddListener(OnNextPressed);
        StartCoroutine(ReadDeaths());
       
    }

    void OnNextPressed()
    {
        gameOver.SetActive(true);
        next.SetActive(false);

    }

    

    private IEnumerator ReadDeaths()
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
            string deaths = snapshot.Child("deaths").Value.ToString();
            StartCoroutine(UpdateDeaths(deaths));
            Debug.Log("Former Deaths is " + deaths);
        }
    }




    private IEnumerator UpdateDeaths(string deaths)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        int formerDeaths1 = int.Parse(deaths);
        int totalDeaths = formerDeaths1 + 1;
        string newDeaths = totalDeaths.ToString();

        //string newDeaths = deaths.ToString();
        var DBTask2 = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(newDeaths);


        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);

        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            //Deaths are now updated
          
           
            Debug.Log("New Deaths is " + newDeaths);
        }
    }
}
