using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerMec : MonoBehaviour
{
    // window
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI compLecture;
    private string textLecture;

    // game window
    [SerializeField] private GameObject gameWindow;

    //[SerializeField] private Button closeButton;
    // Start is called before the first frame update

    public void CloseButtonGame()
    {
        gameWindow.SetActive(false);
    }

    public void Game1()
    {
        gameWindow.SetActive(true);
    }
    public void CloseButton()
    {
        window.SetActive(false);
    }
    public void Lect1()
    {
        window.SetActive(true);

        textLecture = EducationController.lecture1;
        compLecture.text = textLecture;
    }

    public void Lect2()
    {
        window.SetActive(true);

        textLecture = EducationController.lecture2;
        compLecture.text = textLecture;
    }

    public void Lect3()
    {
        window.SetActive(true);

        textLecture = EducationController.lecture3;
        compLecture.text = textLecture;
    }

    public void Lect4()
    {
        window.SetActive(true);

        textLecture = EducationController.lecture4;
        compLecture.text = textLecture;
    }
}
