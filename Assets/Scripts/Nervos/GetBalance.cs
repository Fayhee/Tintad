using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class GetBalance : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public TMP_Text tokenText;

    

    public void ReadBalance()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        string balance = "";
        //Get the currently logged in user data
        DBreference.Child("users").Child(User.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                balance = snapshot.Child("balance").Value.ToString();
            

                tokenText.text = balance;

            }
            else
            {
                tokenText.text = "0";
            }
        });


    }
}
