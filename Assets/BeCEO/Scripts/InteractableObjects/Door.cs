using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.BoolParameter;

public class Door : MonoBehaviour
{
    [Header("���� �� ����� ����, �� ����� �������")]
    [Space]
    [SerializeField] private bool _isEntranceDoor;
    public bool isEntranceDoor
    {
        get { return _isEntranceDoor; }
        set { _isEntranceDoor = value; }
    }

    [Space]
    [Header("����� �����, �� ���� ���������� �������")]
    [SerializeField] private string nextScene;

    [Space]
    [Header("� ������� ����������� ����")]
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
        // ���������
        SceneLoader.LoadScene(nextScene);

    }
}
