using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    // Start is called before the first frame update]
    [SerializeField] private GameObject panelDayInfoUI;


    private float fadeSpeed = .2f;
    void Start()
    {
        panelDayInfoUI.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator StartFade()
    {
        Debug.Log("pdoifaop[" +
            "sdifaop'" +
            "sdifaopsdifapsodifaposdifopasidf");
        panelDayInfoUI.SetActive(true);
        yield return new WaitForSeconds(4f); // Затримка на 4 секунди

        float duration = 3.5f; // Тривалість анімації зникаючої прозорості
        float startTime = Time.time;

        Color startingColor = panelDayInfoUI.GetComponent<Image>().color;
        Color transparentColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0f);

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            panelDayInfoUI.GetComponent<Image>().color = Color.Lerp(startingColor, transparentColor, t);
            yield return null;
        }

        panelDayInfoUI.SetActive(false);
        
        // Тут ви також можете додати будь-які додаткові дії, які вам потрібні після завершення анімації
    }

    IEnumerator FadeIn()
    {
        var imageToFade = panelDayInfoUI.GetComponent<Image>();

        yield return new WaitForSeconds(3f);
        Debug.Log("Color.a = " + imageToFade.color.a);
        while (imageToFade.color.a > 0f)
        {
            Debug.Log("Color.a = " + imageToFade.color.a);
            float newAlpha = imageToFade.color.a - fadeSpeed * Time.deltaTime;
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, newAlpha);
            yield return null;
        }
        Debug.Log("Color.a = " + imageToFade.color.a);
        panelDayInfoUI.SetActive(false);

    }

}


