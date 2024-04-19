using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region values private 
    [Space]
    [SerializeField] private GameObject panelEnterToShop;
    [SerializeField] private GameObject panelBAnnerInfo;
    [Space]
    [SerializeField] private List<GameObject> lsitOfValues;
    [Space]
    [SerializeField] private int countOfValues;
    [Space]
    [SerializeField] private Transform character;

    private float time;

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
        panelEnterToShop.SetActive(false); // need to off
        panelBAnnerInfo.SetActive(false);
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
        // need to count

        time += Time.deltaTime;

        if (time >= 5f)
        {
            panelEnterToShop.SetActive(true);
            panelBAnnerInfo.SetActive(true);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("player"))
        {
            // start conversatin
        }
    }
}
