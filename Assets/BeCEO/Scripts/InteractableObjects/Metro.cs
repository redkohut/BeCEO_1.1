using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metro : MonoBehaviour
{
    [Space]
    [Header("����� �����, �� ���� ���������� �������")]
    [SerializeField] private string nextScene;

    [Space]
    [Header("� ������� ����������� ����")]
    [SerializeField] private GameObject fadedSplashScreen;
    // Start is called before the first frame update
    public void ShowSplashScreen()
    {
        StartCoroutine(StartSplash());
    }

    IEnumerator StartSplash()
    {
        fadedSplashScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        // ���������
        SceneLoader.LoadScene("Metro");

    }


}
