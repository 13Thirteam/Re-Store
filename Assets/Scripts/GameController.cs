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
    [SerializeField] private float timeBeforeGameOver = 7f;

    //references
    public static Transform player;
    [SerializeField] private Transform playerRef;
    [SerializeField] private Image[] deathUI;
    [SerializeField] private Image[] winUI;
    [SerializeField] private Image fadeImg;
    [SerializeField] private LevelTracker levelInfo;
    [SerializeField] private Image dots;
    [SerializeField] private Sprite[] dotImages;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource gameOverMusic;

    //states
    private bool fadingLose = false;
    private bool fadingBlack = false;
    private bool fadingIn = true;
    private bool fadingWin = false;
    private bool lost = false;
    private bool won = false;
    public static int spawnCount = 0;
    public static int killCount = 0;
    private int currentDot = 0;
    private bool dotting = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = playerRef;
        spawnCount = 0;
        killCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image t in deathUI)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        }
        foreach (Image t in winUI)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
        }
        fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1);
        if (dots != null) { dots.color = new Color(dots.color.r, dots.color.g, dots.color.b, 0); }
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
        if(dotting && dots != null)
        {
            dots.sprite = dotImages[currentDot];
            currentDot++;
            if(currentDot >=3)
            {
                currentDot = 0;
            }
            StartCoroutine(DotTimer());
        }
    }

    private IEnumerator DotTimer()
    {
        dotting = false;
        yield return new WaitForSecondsRealtime(.33f);
        dotting = true;
    }

    private void FadeTextIn(Image[] UI)
    {
        foreach (Image t in UI)
        {
            if (t.color.a >= 1)
            {
                if(won) fadingWin = false;
                if (lost)
                {
                    fadingLose = false;
                    StartCoroutine(GameOverTimer());
                }
            }
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + fadeRate * Time.deltaTime);
        }
        if(won)
        {
            if (dots != null) { dots.color = new Color(dots.color.r, dots.color.g, dots.color.b, dots.color.a + fadeRate * Time.deltaTime); }
        }
    }

    private void FinalWin()
    {
        SceneManager.LoadScene(4);
        levelInfo.currentLevel = 1;
    }

    private void Win()
    {
        Debug.Log(levelInfo.currentLevel);
        fadingBlack = true;
        levelInfo.currentLevel++;
        //sfx
        won = true;
        if (levelInfo.currentLevel < 4)
        {
            StartCoroutine(levelChangeTimer());
        }
        else
        {
            Invoke("FinalWin", 6);
        }
    }

    private IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(timeBeforeGameOver);
        SceneManager.LoadScene(0);
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
        if (!lost)
        {
            gameOverMusic.Play();
            music.Pause();
        }
        lost = true;
    }
    
}
