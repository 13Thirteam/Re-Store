using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private LevelTracker levelInfo;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeRate;
    [SerializeField] private Image fadeImg;


    private bool fadingIn = true;

    // Start is called before the first frame update
    void Start()
    {
        fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1);
        audioSource.volume = 0;
    }

    private void Update()
    {
        if (fadingIn)
        {
            if (fadeImg.color.a <= 0) { fadingIn = false;
                fadeImg.gameObject.SetActive(false);
            }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a - fadeRate * Time.deltaTime);
            audioSource.volume += fadeRate * Time.deltaTime;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(levelInfo.currentLevel);
    }

    public void Quit()
    {
        Application.Quit();   
    }

    public void Controls(bool opening)
    {
        controlPanel.SetActive(opening);
    }


}
