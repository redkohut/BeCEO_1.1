using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.ShaderGraph.Internal;

public class ComputerMec : MonoBehaviour
{
    [Header("Computers buttons")]
    [SerializeField] private List<GameObject> buttonsLection;
    [SerializeField] private List<GameObject> buttonsTest;

    [SerializeField] private List<GameObject> buttonsGame;

    [Header("WIndow")]
    // window
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI compLecture;
    private string textLecture;

    [Header("Test")]
    [SerializeField] private List<GameObject> tests;
    // game window
    [SerializeField] private GameObject gameWindow;

    [SerializeField] private Color blockColor;
    //[SerializeField] private Button closeButton;
    // Start is called before the first frame update

    private int indexLection;

    private int[] canPress;

    private void Start()
    {
        // загрузити дані з firebase по рівні

        // треба перевірити чи це перший раз запускається

        indexLection = 1; // 2 лекція
        canPress = new int[buttonsLection.Count];

        BlockButton();

    }

    private void BlockButton()
    {
        for (int i = 0; i < buttonsLection.Count; i++)
        {
            if (i > indexLection)
            {
                Color tempColor = buttonsLection[i].GetComponent<Image>().color;
                buttonsLection[i].GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 0.5f);

                canPress[i] = 1;
            }

        }
    }

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
        if (canPress[0] == 0)
        {
            window.SetActive(true);

            textLecture = EducationController.lecture1;
            compLecture.text = textLecture;
        }
        
    }

    public void Lect2()
    {
        if (canPress[1] == 0)
        {
            window.SetActive(true);

            textLecture = EducationController.lecture2;
            compLecture.text = textLecture;
        }
            
    }

    public void Lect3()
    {
        if (canPress[2] == 0)
        {
            window.SetActive(true);

            textLecture = EducationController.lecture3;
            compLecture.text = textLecture;
        }
            
    }

    public void Lect4()
    {
        if (canPress[3] == 0)
        {
            window.SetActive(true);

            textLecture = EducationController.lecture4;
            compLecture.text = textLecture;
        }    
    }
}
