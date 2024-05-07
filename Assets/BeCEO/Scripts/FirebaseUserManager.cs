using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.WSA;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class FirebaseUserManager : MonoBehaviour
{
    public static FirebaseUserManager instance;
    private static FirebaseUser _currentUser;
    private static DatabaseReference _currentDB;


    public static FirebaseUser CurrentUser
    {
        get { return _currentUser; }
        set { _currentUser = value; }
    }

    public static DatabaseReference CurrentDB
    {
        get { return _currentDB; }
        set { _currentDB = value; }
    }


    // character customize body indexes
    public static int[] bodyPartIndexes;

    public static int[] dataCustomizeFromFirebase = new int[] { };

    #region Registration Data
    public static int gender;
    //public static string password; ------------ for security do not include both variables
    //public static string email;
    public static string name;
    
    public static string age;
    public static int language;
    #endregion

    public static int[] GetCustomizeData()
    {
        Debug.Log("Перевіряємио шо швидше спрацьовує");
        return dataCustomizeFromFirebase;
    }
    private void Awake()
    {
        instance = this;
        // не врахував, що требюа спочатку ввести ті дані, а тоді вже забирати
        LoadCustomizeData();
    }

    // set body part indexex
    public static async void UpdateUserPlace(int place)
    {
        try
        {
            await UpdatePlace(place);
            Debug.Log("Place updated successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Failed to update place: {ex.Message}");
        }
    }

    private static Task UpdatePlace(int place)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("place").SetValueAsync(place);
    }

    // CUSTOMIZATION SET DATA -------------- working 
    #region Customization/Set Values To Firebase

    public static async void UpdateCustomizationData(int[] partIndexes)
    {
        try
        {
            await UpdateBodyData(partIndexes[0]);
            await UpdateHairData(partIndexes[1]);
            await UpdateTorsoData(partIndexes[2]);
            await UpdateLegsData(partIndexes[3]);

            Debug.Log("CustomizeData updated successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Failed to update place: {ex.Message}");
        }
    }

    private static Task UpdateBodyData(int body)
    {
        return _currentDB.Child("customize").Child(_currentUser.UserId).Child("body").SetValueAsync(body);
    }
    private static Task UpdateHairData(int hair)
    {
        return _currentDB.Child("customize").Child(_currentUser.UserId).Child("hair").SetValueAsync(hair);
    }
    private static Task UpdateTorsoData(int torso)
    {
        return _currentDB.Child("customize").Child(_currentUser.UserId).Child("torso").SetValueAsync(torso);
    }
    private static Task UpdateLegsData(int legs)
    {
        return _currentDB.Child("customize").Child(_currentUser.UserId).Child("legs").SetValueAsync(legs);
    }
    #endregion

    // CUSOMIZATION GET DATA --------------- in process
    #region Customization/Get data from Firebase
    public static async void LoadCustomizeData()
    {
        try
        {
            await LoadCustomizeDataCoroutine();
            Debug.Log("Customize data loaded successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Failed to load customize data: {ex.Message}");
        }
    }

    public static async Task LoadCustomizeDataCoroutine()
    {
        // Отримання даних поточного користувача
        DataSnapshot snapshot = await _currentDB.Child("customize").Child(_currentUser.UserId).GetValueAsync();

        if (snapshot.Value != null)
        {
            // Дані отримано
            dataCustomizeFromFirebase = new int[]
            {
                snapshot.Child("body").Exists ? int.Parse(snapshot.Child("body").Value.ToString()) : 0,
                snapshot.Child("hair").Exists ? int.Parse(snapshot.Child("hair").Value.ToString()) : 0,
                snapshot.Child("torso").Exists ? int.Parse(snapshot.Child("torso").Value.ToString()) : 0,
                snapshot.Child("legs").Exists ? int.Parse(snapshot.Child("legs").Value.ToString()) : 0
            };
            Debug.Log("Check array dataCusr.. = " + string.Join(", ", dataCustomizeFromFirebase));
            BodyPartsSelector selector = GameObject.FindGameObjectWithTag("bodySelector").GetComponent<BodyPartsSelector>();
            //selector.FromFirebaseManager(dataCustomizeFromFirebase);
        }
        else
        {
            // Дані відсутні, ініціалізуємо масив нулями
            dataCustomizeFromFirebase = new int[4];
            Debug.Log("No customize data found.");
        }
    }
    #endregion


    // register data
    #region All Registration Data
    public static async void UpdateUserRegisterData(int _gender, string _name, int _age, string _email, string _password, int _language)
    {
        try
        {
            await UpdateGender(_gender);
            await UpdateAge(_age);
            await UpdateName(_name);
            await UpdateEmail(_email);
            await UpdatePassword(_password);
            await UpdateLanguage(_language);
            // local save 
            gender = _gender;
            language = _language;
            Debug.Log("Place updated successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Failed to update place: {ex.Message}");
        }
    }

    private static Task UpdateGender(int _gender)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("gender").SetValueAsync(_gender);
    }
    private static Task UpdateAge(int _age)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("age").SetValueAsync(_age);
    }
    private static Task UpdateName(string _name)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("name").SetValueAsync(_name);
    }
    private static Task UpdateEmail(string _email)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("email").SetValueAsync(_email);
    }
    private static Task UpdatePassword(string _password)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("password").SetValueAsync(_password);
    }
    private static Task UpdateLanguage(int _language)
    {
        return _currentDB.Child("users").Child(_currentUser.UserId).Child("password").SetValueAsync(_language);
    }
    #endregion


    // register 
    #region Register

    #endregion

}
