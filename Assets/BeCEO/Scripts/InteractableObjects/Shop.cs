using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region values private 
    [Space]
    [SerializeField] private GameObject panelEnterToShop;
    [Space]
    [SerializeField] private List<GameObject> lsitOfValues;
    [Space]
    [SerializeField] private int countOfValues;
    [Space]
    [SerializeField] private Transform character;



    [Header("AUdioController")]
    [Space]
    [Space]
    [SerializeField] private AudioSource audioController;
    [SerializeField] private AudioClip clipSoundEnter;
    [SerializeField] private AudioClip clipSOundxit;
 
    private Rigidbody2D characterRigid;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // first of all need to get referect ce
        if (character != null)
        {
            // if our character != null
            if (true)
            {
                characterRigid = character.GetComponent<Rigidbody2D>();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
