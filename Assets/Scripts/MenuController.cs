using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private LevelTracker levelInfo;
    [SerializeField] private GameObject controlPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
