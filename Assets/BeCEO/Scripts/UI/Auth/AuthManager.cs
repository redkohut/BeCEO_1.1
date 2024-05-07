using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference db;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField ageRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField levelField;
    public TMP_InputField moneyField;
    public TMP_InputField placeField;
    public GameObject scoreElement;
    public Transform scoreboardContent;

    [Header("Selector COmponent")]
    public BodyPartsSelector bodySelector;


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // AutoLogin
    /*private void AutoLogin()
    {
        if (User != null)
        {
            References.userName = User.DisplayName;
            SceneLoader.LoadScene("Menu");
        }
        else
        {
            UIManager.instance.LoginScreen();
        }
    }*/

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;

        // add db
        db = FirebaseDatabase.DefaultInstance.RootReference;
        //FirebaseUserManager.Instance.SetCurrentDB(db);

        // check adding 
        if (db == null)
        {
            Debug.LogError("Firebase Database reference (db) is null!");
        }
    }

    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, username, age, language
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text, ageRegisterField.text, PlayerPrefs.GetInt("language")));
    }

    // function for the sign out button
    public void SignOutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterFeilds();
        ClearLoginFeilds();

    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateLevel(int.Parse(levelField.text)));
        StartCoroutine(UpdateMoney(int.Parse(moneyField.text)));
        StartCoroutine(UpdatePlace(int.Parse(placeField.text)));
    }

    //Function for the scoreboard button
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            

            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            if (User.IsEmailVerified)
            {
                //StartCoroutine(LoadUserData());

                // next window (check database operation)
                yield return new WaitForSeconds(2);

                //UIManager.instance.Us

                //usernameField.text = User.DisplayName;

                FirebaseUserManager.CurrentUser = User; // save data about user
                FirebaseUserManager.CurrentDB = db;

                UIManager.instance.MenuScene(); // Change to user data UI             
                

                /*confirmLoginText.text = "";
                ClearLoginFeilds();
                ClearRegisterFeilds();*/

                
            }
            else
            {
                SendEmailForVerification();
            }
        }
    }

    private IEnumerator Register(string _email, string _password, string _username, string _age, int _language)
    {
        // save local 
        PlayerPrefs.SetString("email", _email);
        PlayerPrefs.SetString("password", _password);
        PlayerPrefs.SetString("username", _username);
        PlayerPrefs.SetInt("age", int.Parse(_age));
        PlayerPrefs.SetInt("language", _language);

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

                            // set db and user
                            FirebaseUserManager.CurrentUser = User; // save data about user
                            FirebaseUserManager.CurrentDB = db;
                            // save data
                            /* FirebaseUserManager.UpdateUserRegisterData(
                                 PlayerPrefs.GetInt("gender"),
                                 _username,
                                 int.Parse(_age),
                                 _email,
                                 _password,
                                 PlayerPrefs.GetInt("language"));*/
                            //Now return to login screen

                            // SAVE DATA LOCALY
                            // збережемо дані
                           

                            /*var  temArray = bodySelector.GetBodyPartArray();
                            Debug.Log("\tПопробуємо перевірити чи встановилися дані: ");
                            *//*PlayerPrefs.SetInt("indexBody", bodySelector.bodyPartSelections[0].bodyPartCurrentIndex);
                            PlayerPrefs.SetInt("indexHair", bodySelector.bodyPartSelections[1].bodyPartCurrentIndex);
                            PlayerPrefs.SetInt("indexTorso", bodySelector.bodyPartSelections[2].bodyPartCurrentIndex);
                            PlayerPrefs.SetInt("indexLegs", bodySelector.bodyPartSelections[3].bodyPartCurrentIndex);*//*
                            PlayerPrefs.SetInt("indexBody", temArray[0]);
                            PlayerPrefs.SetInt("indexHair", temArray[1]);
                            PlayerPrefs.SetInt("indexTorso", temArray[2]);
                            PlayerPrefs.SetInt("indexLegs", temArray[3]);

                            Debug.Log("Дані з PlayerPrefs в функції Register: " + PlayerPrefs.GetInt("indexBody") + " " +
                                PlayerPrefs.GetInt("indexHair") + " " + 
                                PlayerPrefs.GetInt("indexTorso") + " " + PlayerPrefs.GetInt("indexLegs"));*/

                            UIManager.instance.LoginScreen();
                            /*warningRegisterText.text = "";

                            

                            ClearRegisterFeilds();
                            ClearLoginFeilds();*/
                        }
                        else
                        {
                            FirebaseUserManager.CurrentUser = User; // save data about user
                            FirebaseUserManager.CurrentDB = db;

                            FirebaseUserManager.UpdateUserRegisterData(
                                 PlayerPrefs.GetInt("gender"),
                                 _username,
                                 int.Parse(_age),
                                 _email,
                                 _password,
                                 PlayerPrefs.GetInt("language"));


                            SendEmailForVerification();
                        }
                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        // create a user profile and set the username
        UserProfile profile = new UserProfile
        {
            DisplayName = _username
        };

        // call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);

        // waith until task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        // check
        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            // auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        Task DBTask = db.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }



    private IEnumerator UpdateLevel(int _level)
    {
        //Set the currently logged in user xp
        Task DBTask = db.Child("users").Child(User.UserId).Child("level").SetValueAsync(_level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }

    private IEnumerator UpdateMoney(int _money)
    {
        //Set the currently logged in user kills
        Task DBTask = db.Child("users").Child(User.UserId).Child("money").SetValueAsync(_money);

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

    private IEnumerator UpdatePlace(int _place)
    {
        //Set the currently logged in user deaths
        Task DBTask = db.Child("users").Child(User.UserId).Child("place").SetValueAsync(_place);

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

/*    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        Task<DataSnapshot> DBTask = db.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            levelField.text = "0";
            moneyField.text = "0";
            placeField.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            levelField.text = snapshot.Child("level").Value.ToString();
            moneyField.text = snapshot.Child("money").Value.ToString();
            placeField.text = snapshot.Child("place").Value.ToString();
        }
    }*/

    private IEnumerator LoadScoreboardData()
    {
        //Get all the users data ordered by kills amount
        Task<DataSnapshot> DBTask = db.Child("users").OrderByChild("place").GetValueAsync();

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
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int level = int.Parse(childSnapshot.Child("level").Value.ToString());
                int money = int.Parse(childSnapshot.Child("money").Value.ToString());
                int place = int.Parse(childSnapshot.Child("place").Value.ToString());

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                //scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, kills, deaths, xp);
            }

            //Go to scoareboard screen
            UIManager.instance.ScoreboardScreen();
        }
    }

    // Verification email
    public void SendEmailForVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());   
    }

    private IEnumerator SendEmailForVerificationAsync()
    {
        if (User != null)
        {
            var sendEmailTask = User.SendEmailVerificationAsync();

            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string errorInfo = "Unknown Error : Please try later";

                switch (error)
                {
                    case AuthError.None:
                        errorInfo = "Email Verification was Cancelled";
                        break;
                    case AuthError.TooManyRequests:
                        errorInfo = "Too many requests";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        errorInfo = "The email you entered in invalid";
                        break;
                }

                UIManager.instance.ShowVerificationResponse(false, User.Email, errorInfo);
            }
            else
            {
                Debug.Log("Email has successfully sent");



                UIManager.instance.ShowVerificationResponse(true, User.Email, null);
            }
        }
    }

}
