using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerOS : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonsLection;
    [SerializeField] private List<GameObject> buttonsTest;

    [SerializeField] private List<GameObject> buttonsGame;

    private int currentIndexLection;
    private void Start()
    {
        // when game is starting
        // get dara from firebase
        
        // at now just 0
        currentIndexLection = 0;


    }

    private void BlockButtons()
    {

    }
}
