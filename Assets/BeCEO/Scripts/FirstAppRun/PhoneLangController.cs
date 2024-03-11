using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PhoneLangController : MonoBehaviour
{
    /*
     * 0 - Ukraine
     * 1 - Poland
     * 2 - English
     */
    [Header("UI elements")]
    [Space]
    [SerializeField]
    private List<GameObject> buttonsLang = new List<GameObject>();
    [SerializeField]
    private List<GameObject> phoneBgs = new List<GameObject>();

    // ����
    [Space]
    [Header("All for Audio")]
    [Space]
    [SerializeField]
    private AudioClip buttonSound;
    [SerializeField]
    private AudioClip phoneDisableSound;
    [SerializeField]
    private AudioClip waterFlowSound;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource waterAudioSource;

    // �������
    [Space]
    [Header("Animator Controllers")]
    [Space]
    [SerializeField]
    private List<Animator> characters = new List<Animator>();
    [SerializeField]
    private List<Animator> flags = new List<Animator>();
    [SerializeField]
    private List<Animator> water = new List<Animator>();

    // ������� ������� ����
    [Space]
    [Header("Particle EffectList")]
    [Space]
    [SerializeField]
    private List<ParticleSystem> waterFlow = new List<ParticleSystem>();


    private int currentFocusPhone = 0;

    private void Awake()
    {
        foreach (ParticleSystem flow in waterFlow)
        {
            flow.Stop();
        }
        foreach (GameObject button in buttonsLang)
        {
            button.SetActive(false);
        }
    }

    public void UkraineButton()
    {
        Debug.Log("UkraineButton");
        PlayButtonSound();
    }

    public void PolandButton()
    {
        Debug.Log("PolandButton");
        PlayButtonSound();
    }

    public void EnglishButton()
    {
        Debug.Log("EnglishButton");
        PlayButtonSound();
    }

    public void PlayButtonSound()
    {
        audioSource.clip = buttonSound;
        audioSource.PlayOneShot(buttonSound);

    }

    public void PlayPhoneDisableSound()
    {
        audioSource.clip = phoneDisableSound;
        audioSource.PlayOneShot(phoneDisableSound);
    }

    private void PlayFlowWaterSound()
    {
        waterAudioSource.clip = waterFlowSound;
        waterAudioSource.Play(0);
    }
    public void OnMouseEnterUkraine()
    {
        if (currentFocusPhone == 0)
        {
            // ��������� ����������� ����
            PlayPhoneDisableSound();
            PlayFlowWaterSound();
            // ��������� flag
            currentFocusPhone = 1;
            // ��������� �������
            StartAnimation(currentFocusPhone);
        }
        if (currentFocusPhone != 1)
        {
            // ��������� ����������� ����
            PlayPhoneDisableSound();
            // ��������� ��������� �������
            FinishAnimation(currentFocusPhone); 
            // ��������� ����
            StartAnimation(1);
        }
        phoneBgs[currentFocusPhone - 1].SetActive(true);
        currentFocusPhone = 1;
        buttonsLang[currentFocusPhone - 1].SetActive(true);
        phoneBgs[currentFocusPhone - 1].SetActive(false);
    }


    public void OnMouseEnterPoland()
    {
        if (currentFocusPhone == 0)
        {
            // ��������� ����������� ����
            PlayPhoneDisableSound();
            PlayFlowWaterSound();
            // ����
            currentFocusPhone = 2;
            // ��������� �������
            StartAnimation(currentFocusPhone);
        }
        if (currentFocusPhone != 2)
        {
            PlayPhoneDisableSound();
            // ��������� ��������� �������
            // � ��� �� �� ������� ���� ����������(��� flag)
            FinishAnimation(currentFocusPhone);
            // ��������� ����
            StartAnimation(2);
        }
        phoneBgs[currentFocusPhone - 1].SetActive(true);
        currentFocusPhone = 2;
        buttonsLang[currentFocusPhone - 1].SetActive(true);
        phoneBgs[currentFocusPhone - 1].SetActive(false);
    }

   
    public void OnMouseEnterEnglish()
    {

        if (currentFocusPhone == 0)
        {
            // ��������� ����������� ����
            PlayPhoneDisableSound();
            PlayFlowWaterSound();
            // ����
            currentFocusPhone = 3;
            // ��������� �������
            StartAnimation(currentFocusPhone);
        }
        if (currentFocusPhone != 3)
        {
            PlayPhoneDisableSound();
            // ��������� ��������� �������
            // � ��� �� �� ������� ���� ����������(��� flag)
            FinishAnimation(currentFocusPhone);
            // ��������� ����
            StartAnimation(3);
        }
        phoneBgs[currentFocusPhone - 1].SetActive(true);
        currentFocusPhone = 3;
        buttonsLang[currentFocusPhone - 1].SetActive(true);
        phoneBgs[currentFocusPhone - 1].SetActive(false);

    }

    private void StartAnimation(int indexOfPhone)
    {
        int index = indexOfPhone - 1;
        // pass
        if (index == 0)
        {
            // defaut Ukraine
            // �������� ����� �������� ��� � �������� �����������
            // 1. ������� 2. ���������� 3. ������
            water[index].Play("WaterBath");
            characters[index].Play("Ukraine");
            flags[index].Play("UkraineFlag");
            waterFlow[index].gameObject.SetActive(true);
            buttonsLang[index].SetActive(true);
            waterFlow[index].Play();

        }
        else if (index == 1)
        {
            water[index].Play("WaterBath");
            characters[index].Play("Poland");
            flags[index].Play("PolandFlag");
            waterFlow[index].gameObject.SetActive(true);
            buttonsLang[index].SetActive(true);
            waterFlow[index].Play();
        }
        else if (index == 2)
        {
            water[index].Play("WaterBath");
            characters[index].Play("USA");
            flags[index].Play("EnglishFlag");
            waterFlow[index].gameObject.SetActive(true);
            buttonsLang[index].SetActive(true);
            waterFlow[index].Play();
        }
        else
        {
            Debug.Log("Troubles!");
        }
    }

    private void FinishAnimation(int indexOfPhone)
    {
        int index = indexOfPhone - 1;
        // pass
        if (index == 0)
        {            
            SimplifyFinishVoid(index);

        }
        else if (index == 1)
        {
            SimplifyFinishVoid(index);
        }
        else if (index == 2)
        {
            SimplifyFinishVoid(index);
        }
        else
        {
            Debug.Log("Troubles");
        }

    }

    private void SimplifyFinishVoid(int index) 
    {
        // �������� ����� �������� ������� ������� � �������� �����������
        // 1. ������� 2. ���������� 3. ������
        water[index].Play("DefaultWater");
        characters[index].Play("New State");
        flags[index].Play("New State");
        waterFlow[index].gameObject.SetActive(false);
        buttonsLang[index].SetActive(false);
        waterFlow[index].Stop();
    }

    
}
