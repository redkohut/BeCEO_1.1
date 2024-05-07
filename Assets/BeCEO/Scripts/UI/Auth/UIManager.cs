using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;

    public GameObject userDataUI;
    public GameObject scoreboardUI;

    public GameObject emailVerificationUI;

    [Header("Registration")]
    public GameObject customizeCharacterUI;
    public GameObject registrationUI;

    // text
    [Header("Verification text")]
    [SerializeField] private TMP_Text emailVerificationText;

    [Space]
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField ageRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public int genderIndex;

    [Header("Selector")]
    public BodyPartsSelector selector;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void From2To3()
    {
        emailVerificationUI.SetActive(false);
        customizeCharacterUI.SetActive(true);
        registrationUI.SetActive(false);
    }

    // ------------------------------------ SET DATA BUTTON
    public void EndRegistrationButton() 
    { 
        //
        //Firebase.Update
    }

    #region Register
/*    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        // tranition to verification panel 
                        if (User.IsEmailVerified)
                        {
                            //Username is now set
                            //Now return to login screen
                            UIManager.instance.LoginScreen();
                            warningRegisterText.text = "";
                            ClearRegisterFeilds();
                            ClearLoginFeilds();
                        }
                        else
                        {
                            SendEmailForVerification();
                        }
                    }
                }
            }
        }
    }*/
    #endregion

    private void Clear()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
        emailVerificationUI.SetActive(false);
    }
    public void LoginScreen() //Back button
    {
        //Clear();
        //loginUI.SetActive(true);
        // save data
        var temArray = selector.GetBodyPartArray();
        PlayerPrefs.SetInt("indexBody", temArray[0]);
        PlayerPrefs.SetInt("indexHair", temArray[1]);
        PlayerPrefs.SetInt("indexTorso", temArray[2]);
        PlayerPrefs.SetInt("indexLegs", temArray[3]);

        SceneLoader.LoadScene("Authentication");
    }
    public void RegisterScreen() // Regester button
    {
        //Clear();
        // registerUI.SetActive(true);
        SceneManager.LoadScene("StartGame");
    }

    public void UserDataScreen() //Logged in
    {
        //Clear();
        SceneLoader.LoadScene("Home");
        userDataUI.SetActive(true);
    }

    public void MenuScene()
    {
        //Clear();
        SceneLoader.LoadScene("Home");
    }

    public void ScoreboardScreen() //Scoreboard button
    {
        //Clear();
        scoreboardUI.SetActive(true);
    }

    public void ShowVerificationResponse(bool isEmailSent, string emailID, string errorMessage)
    {
        //Clear();
        emailVerificationUI.SetActive(true);

        if (isEmailSent)
        {
            emailVerificationText.text = $"Please, verify your email address \n Verification email has been sent to {emailID}";
        }
        else
        {
            emailVerificationText.text = $"Could not sent email: {errorMessage}";
        }
    }
}
