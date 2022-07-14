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

    public async void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        User = auth.CurrentUser;

        string account = User.DisplayName;
        BigInteger balanceOf = -1;

        if (rpc != null)
        {
            balanceOf = await ERC20.BalanceOf(chain, network, contract, account, rpc);
        }
        else
        {
            balanceOf = await ERC20.BalanceOf(chain, network, contract, account);
        }

        if (balanceOf == -1)
        {
            Debug.Log("Nothing");
        }

        if (balanceOf != null) balance1 = balanceOf.ToString();

        string balance = balance1.Substring(0, 5);
        tokenField.text = balance;
        StartCoroutine(UpdateBalance(int.Parse(balance)));
    }
    
    private IEnumerator UpdateBalance(int balance)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("balance").SetValueAsync(balance);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("balance are now updated");
        }
    }


   
}
