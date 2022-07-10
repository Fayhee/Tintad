using System.Numerics;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
public class GetAccount : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public TMP_Text accountText;

    // Start is called before the first frame update
    void Start()
    {
        ReadAccount();
    }

    public void ReadAccount()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        string account = "";
        //Get the currently logged in user data
        DBreference.Child("users").Child(User.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                account = snapshot.Child("nervos").Value.ToString();
                Debug.Log(account);

                accountText.text = account;

            }
            else
            {
                Debug.Log("account not found");
            }
        });


    }
}
