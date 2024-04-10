using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonMec : MonoBehaviour
{
    [SerializeField] private Transform newPositionSpawnSitting;

    [SerializeField] private GameObject insideSubway;
    [SerializeField] private GameObject insideWagon;
    [SerializeField] private GameObject insedeSubway2;

    [Space]
    [Header("Назва сцени, де буде відбуватися перехід")]
    [SerializeField] private string nextScene;

    [Space]
    [Header("З канваса перетягнути вікно")]
    [SerializeField] private GameObject fadedSplashScreen;
    // Start is called before the first frame update

    public void StartRiding()
    {
        insideSubway.SetActive(false);
        insideWagon.SetActive(true);

        StartCoroutine(InsideTheWagon());
    }
 

    IEnumerator InsideTheWagon()
    {
        yield return new WaitForSeconds(6);
        // виключити
        insideWagon.SetActive(false);
        insedeSubway2.SetActive(true);

    }

    public void ShowSplashScreen()
    {
        StartCoroutine(StartSplash());
    }

    IEnumerator StartSplash()
    {
        fadedSplashScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        // виключити
        SceneLoader.LoadScene(nextScene);

    }



}
