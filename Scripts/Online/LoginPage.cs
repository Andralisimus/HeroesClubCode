using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class LoginPage : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public Button loginButton;
    public Button registrationButton;
    public Text errorText;

    public static string userId = "";

    private FirebaseAuth auth;

    void Start()
    {
        InitializeFirebase();
        loginButton.onClick.AddListener(delegate {
            login();
        });

        registrationButton.onClick.AddListener(delegate {
            register();
        });
    }

    private void login()
    {
        var email = emailField.text;
        var password = passwordField.text;

        auth.SignInWithEmailAndPasswordAsync(email, password);
    }

    private void register()
    {
        var email = emailField.text;
        var password = passwordField.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password);
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != null)
        {
            errorText.text = "logged in as: " + auth.CurrentUser.Email;
            userId = auth.CurrentUser.UserId;
            SceneManager.LoadScene("SampleScene");
        } 
        else errorText.text = "Make sure password and email are correct";
    }
}
