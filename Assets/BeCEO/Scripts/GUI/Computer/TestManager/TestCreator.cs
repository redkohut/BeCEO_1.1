using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestCreator : MonoBehaviour
{
    /*[System.Serializable]
    public class Question
    {
        [Header("Question")]
        public TMP_Text questionTextComponent;

        [Header("Choose correct answer")]
        public bool A;
        public bool B;
        public bool C;

        [Header("Buttons")]
        public List<Button> buttons = new List<Button>();

        public bool isTrueAnswer;
    }

    [SerializeField] private List<Question> questions = new List<Question>();


    private void Start()
    {
        // ����������� ���� ��������, ��� ������� ���� ������
        // ������������ �� ������
        foreach (var question in questions)
        {
            *//*for (int i = 0; i < question.buttons.Count; i++)
            {
                if ()
                question.buttons[i].onClick.AddListener(EmptyButton);
            }*//*
            if (question.A)
            {
                question.buttons[0].onClick.AddListener(AnswerButton);
                question.buttons[1].onClick.AddListener(EmptyButton);
                question.buttons[2].onClick.AddListener(EmptyButton);
            }
            else if (question.B)
            {
                question.buttons[0].onClick.AddListener(EmptyButton);
                question.buttons[1].onClick.AddListener(AnswerButton);
                question.buttons[2].onClick.AddListener(EmptyButton);
            }
            else if (question.C)
            {
                question.buttons[0].onClick.AddListener(EmptyButton);
                question.buttons[1].onClick.AddListener(EmptyButton);
                question.buttons[2].onClick.AddListener(AnswerButton);
            }
                
        }
    }

    private void EmptyButton()
    {

    }

    private void AnswerButton()
    {

    }*/
    [System.Serializable]
    public class Question
    {
        [Header("Question")]
        public TMP_Text questionTextComponent;

        [Header("Choose correct answer")]
        public List<bool> isTrueAnswers = new List<bool>();

        [Header("Buttons")]
        public List<Button> buttons = new List<Button>();

        public bool isCorrect = false;
    }

    [SerializeField] private List<Question> questions = new List<Question>();
    [SerializeField] private Color normalColor = Color.white; 
    [SerializeField] private Color selectedColor = Color.blue;
    private void Start()
    {
        // ��������� ���� ��������, ��� ������� ���� ������
        for (int i = 0; i < questions.Count; i++)
        {
            // ������ ������� �� ������
            for (int j = 0; j < questions[i].buttons.Count; j++)
            {
                int questionIndex = i;
                int answerIndex = j;

                questions[i].buttons[j].onClick.AddListener(() => AnswerButton(questionIndex, answerIndex));
            }
        }
    }

    private void AnswerButton(int questionIndex, int answerIndex)
    {
        // �������� ������� �� �������
        Question question = questions[questionIndex];

        // ����������, �� ��������� �������
        bool isCorrect = question.isTrueAnswers[answerIndex];

        // ��������� �������� ���������� ������ �����������, ���� ���� ����
        for (int i = 0; i < question.buttons.Count; i++)
        {
            if (i != answerIndex)
            {
                // question.buttons[i].interactable = true;
                question.buttons[i].image.color = Color.white;

            }
        }

        // ��������� ���� ��������� ������ ��� �����������
        if (isCorrect)
        {
            Debug.Log("Correct Answer!");
            question.isCorrect = true;
            // ��������� ������� ����������� �� ��������� � �������� �� ���� ������
            //question.buttons[answerIndex].interactable = false;
        }
        else
        {
            Debug.Log("Wrong Answer!");
            question.isCorrect = false;
            // ��������� ������� ����������� �� ����������� � �������� �� ���� ������
            //question.buttons[answerIndex].interactable = false;
        }

        // ϳ��� ����, �� ���������� ������ �� �� �������, �� ������ ��������� ����� CalculateResults ��� ��������� ����������.
        question.buttons[answerIndex].image.color = Color.blue;

    }


    private void CalculateResults()
    {
        int totalQuestions = questions.Count;
        int correctAnswersCount = 0;

        // ���������� �� ������� � ���������� ������� ���������� ��������
        foreach (var question in questions)
        {
            /*foreach (var button in question.buttons)
            {
                // ���� ������ �������� (������� ���� ����������), �������� �������� ���������� ��������
                if (!button.interactable)
                {
                    correctAnswersCount++;
                    break; // �������� � �����, ���� �������� ��������� �������
                }
            }*/
            if (question.isCorrect)
            {
                correctAnswersCount++;
            }
            
        }

        // �������� ����������
        float percentage = (float)correctAnswersCount / totalQuestions * 100f;
        Debug.Log("Total Questions: " + totalQuestions);
        Debug.Log("Correct Answers: " + correctAnswersCount);
        Debug.Log("Percentage: " + percentage + "%");
    }

    public void CheckResults()
    {
        CalculateResults();
    }

}
