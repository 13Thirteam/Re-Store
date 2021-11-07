using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    //parameters
    [SerializeField] private float fadeRate;
    [SerializeField] private float fadeInRate;
    [SerializeField] private float timeBeforeLevelChange = 2f;

    //references
    public static Transform player;
    [SerializeField] private Transform playerRef;
    [SerializeField] private TextMeshProUGUI[] deathUI;
    [SerializeField] private TextMeshProUGUI[] winUI;
    [SerializeField] private Image fadeImg;
    [SerializeField] private LevelTracker levelInfo;

    //states
    private bool fadingLose = false;
    private bool fadingBlack = false;
    private bool fadingIn = true;
    private bool fadingWin = false;
    private bool lost = false;
    private bool won = false;
    public static int spawnCount = 0;
    public static int killCount = 0;


    // Start is called before the first frame update
    void Awake()
    {
        player = playerRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshProUGUI t in deathUI)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        }
        foreach (TextMeshProUGUI t in winUI)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        }
        fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            if(fadeImg.color.a <= 0) { fadingIn = false; }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a - fadeInRate * Time.deltaTime);
        }
        if (fadingBlack)
        {
            if (fadeImg.color.a >= 1)
            {
                fadingBlack = false;
            }
            if (fadeImg.color.a >= .70)
            {
                if (won) { fadingWin = true; }
                if (lost) { fadingLose = true; }
            }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a + fadeRate * Time.deltaTime);
        }
        if (fadingLose)
        {
            FadeTextIn(deathUI);
        }
        if (fadingWin)
        {
            FadeTextIn(winUI);
        }
        if(!won && killCount >= spawnCount) { Win(); }
    }

    private void FadeTextIn(TextMeshProUGUI[] UI)
    {
        foreach (TextMeshProUGUI t in UI)
        {
            if (t.color.a >= 1)
            {
                if(won) fadingWin = false;
                if (lost)
                {
                    fadingLose = false;
                    SceneManager.LoadScene(0);
                }
            }
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + fadeRate * Time.deltaTime);
        }
    }

    private void Win()
    {
        Debug.Log(levelInfo.currentLevel);
        fadingBlack = true;
        levelInfo.currentLevel++;
        //sfx
        if (levelInfo.currentLevel < 4)
        {
            won = true;
            StartCoroutine(levelChangeTimer());
        }
        else
        { 
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator levelChangeTimer()
    {
        yield return new WaitForSeconds(timeBeforeLevelChange);
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int val)
    {
        SceneManager.LoadScene(val);
    }

    public void Die()
    {
        fadingBlack = true;
        levelInfo.currentLevel = SceneManager.GetActiveScene().buildIndex;
        lost = true;
    }
    
}
