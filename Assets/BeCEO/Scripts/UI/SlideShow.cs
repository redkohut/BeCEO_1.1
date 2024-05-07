using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> slides = new List<GameObject>();


    public void Next1to2Button()
    {
        slides[0].SetActive(false);
        slides[1].SetActive(true);
    }
    public void Next2to2Button()
    {
        slides[1].SetActive(false);
        slides[2].SetActive(true);
    }
    public void Next3to4Button()
    {
        slides[2].SetActive(false);
        slides[3].SetActive(true);
    }
    public void Next4to5Button()
    {
        slides[3].SetActive(false);
        slides[4].SetActive(true);
    }
    public void Next5to6Button()
    {
        slides[4].SetActive(false);
        slides[5].SetActive(true);
    }
    public void Next6to7Button()
    {
        slides[5].SetActive(false);
        slides[6].SetActive(true);
    }
    public void Next7to8Button()
    {
        slides[6].SetActive(false);
        slides[7].SetActive(true);
    }

    public void TheEnd()
    {
        SceneLoader.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
