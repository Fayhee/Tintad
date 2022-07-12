using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Numerics;
public class ScoreData : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

   
    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField tokenField;
    public TMP_Text accountText;
    public TMP_InputField killsField;
    public TMP_InputField deathsField;
    public GameObject scoreElement;
    public Transform scoreboardContent;


    // Chain == Chain name connecting
    [SerializeField] private string chain = "godwoken";

    // Network == The network RPC name
    [SerializeField] private string network = "testnet-v1";

    // USDC address == Token Contract Address
    [SerializeField] private string contract = "0xD233FFD436D68235B5fB51dfB9e368b598Dc5F9B";



    


    // rpc of chain
    [SerializeField] private string rpc = "https://godwoken-testnet-v1.ckbapp.dev";

    private string balance1;



    void Awake()
    {
        

        InitializeFirebase();
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        StartCoroutine(LoadUserData());
        accountText.text = User.DisplayName;

        Debug.Log("Setting up Firebase Auth");
    }



    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    

   
    //Function for the sign out button
    public void SignOutButton()
    {
        auth.SignOut();
        SceneManager.LoadScene("LoginScene");

    }

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


    //Function for the save button
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(accountText.text));
        StartCoroutine(UpdateUsernameDatabase(accountText.text));
        StartCoroutine(UpdateKills(int.Parse(killsField.text)));
        StartCoroutine(UpdateDeaths(int.Parse(deathsField.text)));
    }
    //Function for the scoreboard button
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    public void UserDataButon()
    {
        accountText.text = User.DisplayName;
        UIManager1.instance.UserDataScreen(); // Change to user data UI
       
        
    }

   

    private IEnumerator UpdateUsernameAuth(string _nervos)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _nervos };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _nervos)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("nervos").SetValueAsync(_nervos);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database nervos account is now updated
        }
    }

    

    private IEnumerator UpdateKills(int _kills)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("kills").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateDeaths(int _deaths)
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(_deaths);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }

    private IEnumerator LoadUserData()
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
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
           
            killsField.text = "0";
            deathsField.text = "0";
            usernameField.text = " ";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

 
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();
            usernameField.text = snapshot.Child("username").Value.ToString();
        }
    }

    private IEnumerator LoadScoreboardData()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("kills").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
               // Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int kills = int.Parse(childSnapshot.Child("kills").Value.ToString());
                int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());              
                //string balance = childSnapshot.Child("balance").Value.ToString();

                string balance = "5";



                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, kills, deaths, balance);
            }

            //Go to scoareboard screen
            UIManager1.instance.ScoreboardScreen();
        }

        
    }
}
