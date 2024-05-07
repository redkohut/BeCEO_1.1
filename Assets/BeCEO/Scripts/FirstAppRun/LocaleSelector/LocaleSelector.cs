using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;

    [Header("Firebase")]

    public FirebaseUser User;
    public DatabaseReference db;

    private int indexBody, indexHair, indexTorso, indexLegs = 1;
    private void Awake()
    {
        // 
        //FirebaseUserManager.UpdateUserPlace(1000);

        //




        //FirebaseUserManager.UpdateCustomizationData(new int[] { 1, 1, 1, 1});
    }
    public void ChangeLocale(int localeID)
    {
        if (active)
        {
            return;
        }
        //SaveSystemSimple.SetLocaleID(localeID);

        // simplify this process
        // save just localization index
        PlayerPrefs.SetInt("language", localeID);
        StartCoroutine(SetLocale(localeID));
        
    }
    IEnumerator SetLocale(int localeID)
    {
        // перевіримо чи готова функція введення в локалізацію
        yield return LocalizationSettings.InitializationOperation;
        // і виберемо мову
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
        // загрузимо зразу наступну сцену
        SceneLoader.LoadScene("CharacterCustomization");

    }
    
   
}
