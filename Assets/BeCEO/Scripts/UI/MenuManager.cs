using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject ship;

    [Header("UI Panels")]
    [SerializeField] private GameObject SettingsUI;
    [SerializeField] private GameObject TrajectoryUI;
    [SerializeField] private GameObject MotivationUI;
    [SerializeField] private GameObject HistoryUI;

    [Space]
    [SerializeField] private GameObject darkPanel;

    [Header("Scenario Presentation")]
    [SerializeField] private GameObject presentationScenarioUI;
    private float fadeSpeed = 0.5f;
    private bool isMoving = false;
    private float speed = 4f;

    private void Start()
    {

        //presentationScenarioUI.SetActive(true);
    }

    private IEnumerator ShowPresentation()
    {

        yield return new WaitForSeconds(10f);
    }
    public void SkipPresentation()
    {

    }

    public void HistoryButton()
    {

    }
    public void JustButton()
    {
        Debug.Log("ClickButton");
    }
    public void PlayButton()
    {
        Debug.Log("ClickButtonPlay");
        darkPanel.SetActive(true);
        StartCoroutine(SmoveSwim());
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var imageToFade = darkPanel.GetComponent<Image>();
        while (imageToFade.color.a < 1f)
        {
            float newAlpha = imageToFade.color.a + fadeSpeed * Time.deltaTime;
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, newAlpha);
            yield return null;
        }

    }
    private IEnumerator SmoveSwim()
    {
        isMoving = true;
        yield return new WaitForSeconds(2);

        SceneLoader.LoadScene("Home");
    }
    

    private void Update()
    {
        if (isMoving)
        {
            ship.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
