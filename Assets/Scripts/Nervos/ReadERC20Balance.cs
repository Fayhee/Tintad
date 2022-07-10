using System.Numerics;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class ReadERC20Balance : MonoBehaviour
{
    // Chain == Chain name connecting
    [SerializeField] private string chain = "godwoken";

    // Network == The network RPC name
    [SerializeField] private string network = "testnet-v1";

    // USDC address == Token Contract Address
    [SerializeField] private string contract = "0xD233FFD436D68235B5fB51dfB9e368b598Dc5F9B";

    

    public TMP_Text accountText;
 

    // rpc of chain
    [SerializeField] private string rpc = "https://godwoken-testnet-v1.ckbapp.dev";

    private string balance1;
    

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;


    // Start is called before the first frame update
    
    public async void StartUp()
    {
       string account = accountText.text;
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
