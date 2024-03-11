using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCustomizationMec : MonoBehaviour
{
    [SerializeField]
    private Animator genderButtonAmimator;

    /* [SerializeField]
     private List<Color> colors = new List<Color>();*/

    /*[SerializeField] 
    private Image inputFieldImage;*/

    [SerializeField]
    private TMP_InputField nameInputField;

    // сплеш екран при виявленні помилки
    private float displayTime = 3f;
    [SerializeField]
    private GameObject splashError;

    // gender
    private int gender = 0;


    // BUTTON SWITCHGENDER
    public void SwitchGender()
    {
        gender = (gender == 1) ? SwitchToFemale() : SwithToMale();
        // змінюємо колір в полі для введення
        //inputFieldImage.color = colors[gender];
    }

    // BUTTON PLAYNEXTSCENE
    public void Play()
    {
        if (nameInputField.text == "")
        {
            Debug.Log("Cannot open the next scene, because we have a null string");
            // запустити сплеш екран про помилку
            
            StartCoroutine(SplashError());
        }
        else
        {
            SceneLoader.LoadScene("Home");

        }
    }
    private int SwithToMale()
    {
        genderButtonAmimator.Play("SwitchToMale");

        return 1;
    }

    private int SwitchToFemale()
    {
        genderButtonAmimator.Play("SwitchToFemale");
        return 0;
    }

    IEnumerator SplashError()
    {
        splashError.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        // виключити
        splashError.SetActive(false);
        
    }

 
}
