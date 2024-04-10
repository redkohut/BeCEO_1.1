using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolEnter : MonoBehaviour
{
    [Space]
    [Header("����� �����, �� ���� ���������� �������")]
    [SerializeField] private string nextScene;

    [Space]
    [Header("� ������� ����������� ����")]
    [SerializeField] private GameObject fadedSplashScreen;

 

    public void ShowSplashScreen()
    {
        StartCoroutine(StartSplash());
    }

    IEnumerator StartSplash()
    {
        fadedSplashScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        // ���������
        SceneLoader.LoadScene(nextScene);

    }
}
