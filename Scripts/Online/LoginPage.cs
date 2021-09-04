using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class LoginPage : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public Button loginButton;
    public Button registrationButton;
    public Text errorText;

    public static string userId = "";

    private FirebaseAuth auth;


    private bool allowLoginIfHasUser = false; //Если true, то после регистрации пользователю не надобудет потом заново входить.
    private int errorSuffixLenght = 28; //Длина начала ошибки которую мы хотим убрать чтобы output был покрасивее 

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
        var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        var email = emailField.text;
        var password = passwordField.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                errorText.text = task.Exception.GetBaseException().ToString().Remove(0, errorSuffixLenght);
                return;
            }

            if (task.IsCompleted)
            {
                GoToGame();
                return;
            }
        }, taskScheduler);
    }

    private void register()
    {
        var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        var email = emailField.text;
        var password = passwordField.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                errorText.text = task.Exception.GetBaseException().ToString().Remove(0, errorSuffixLenght);
                return;
            }

            if (task.IsCompleted)
            {
                GoToGame();
                return;
            }
        }, taskScheduler);
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser != null)
        {
            if (allowLoginIfHasUser == false)
            {
                auth.SignOut();
            }
            else
            {
                GoToGame();
            }
        }    
    }

    private void GoToGame()
    {
        userId = auth.CurrentUser.UserId;
        SceneManager.LoadScene("SampleScene");
    }
}
