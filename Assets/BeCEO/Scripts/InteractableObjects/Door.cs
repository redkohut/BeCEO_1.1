using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.BoolParameter;

public class Door : MonoBehaviour
{
    [Header("Якщо це вхідні двері, то треба галочку")]
    [Space]
    [SerializeField] private bool _isEntranceDoor;
    public bool isEntranceDoor
    {
        get { return _isEntranceDoor; }
        set { _isEntranceDoor = value; }
    }

    [Space]
    [Header("Назва сцени, де буде відбуватися перехід")]
    [SerializeField] private string nextScene;

    [Space]
    [Header("З канваса перетягнути вікно")]
    [SerializeField] private GameObject fadedSplashScreen;

    private bool isOpen = false;

    public bool isState
    {
        get { return isOpen; }
        set { isOpen = value; }
    }


    public void ShowSplashScreen()
    {
        StartCoroutine(StartSplash());
    }

    IEnumerator StartSplash()
    {
        fadedSplashScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        // виключити
        SceneLoader.LoadScene(nextScene);

    }
}
