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

    private string accountText;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        User = auth.CurrentUser;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        accountText = User.DisplayName;
    }

    public string GetTheAccount()
    {
        auth = FirebaseAuth.DefaultInstance;
        User = auth.CurrentUser;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        string account = User.DisplayName;
        Debug.Log("The account is " + account);

        return account;
    }

   
}
