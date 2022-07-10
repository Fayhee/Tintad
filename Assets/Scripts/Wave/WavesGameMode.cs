using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;
    [SerializeField] Life playerBaseLife;

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public TMP_Text usernameField;

   

    void Awake()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        User = auth.CurrentUser;
        usernameField.text = User.DisplayName;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

    }




    void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerLifeChanged);
        playerBaseLife.onDeath.AddListener(OnPlayerBaseLifeChanged);
        EnemyManager.instance.onChanged.AddListener(CheckWinCondition);
        WavesManager.instance.onChanged.AddListener(CheckWinCondition);
    }
    
    void CheckWinCondition()
    {
        if (EnemyManager.instance.enemies.Count <= 0 && WavesManager.instance.waves.Count <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("WinScreen");
        }
    }
    
    void OnPlayerLifeChanged()
    {
        if (playerLife.amount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("LoseScreen1");
        }
    }
    
    void OnPlayerBaseLifeChanged()
    {
        if (playerBaseLife.amount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("LoseScreen1");
            

        }
    }

   
}