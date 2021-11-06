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
    [SerializeField] private float timeBeforeLevelChange = 2f;

    //references
    public static Transform player;
    [SerializeField] private Transform playerRef;
    [SerializeField] private TextMeshProUGUI[] deathUI;
    [SerializeField] private TextMeshProUGUI[] winUI;
    [SerializeField] private Image fadeImg;

    //states
    private bool fadingLose = false;
    private bool fadingBlack = false;
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
    }

    // Update is called once per frame
    void Update()
    {
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
        if(killCount >= spawnCount) { Win(); }
    }

    private void FadeTextIn(TextMeshProUGUI[] UI)
    {
        foreach (TextMeshProUGUI t in UI)
        {
            if (t.color.a >= 1)
            {
                if(won) fadingWin = false;
                if(lost) fadingLose = false;
            }
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + fadeRate * Time.deltaTime);
        }
    }

    private void Win()
    {
        fadingBlack = true;
        won = true;
        //sfx
        //StartCoroutine(levelChangeTimer());
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
        lost = true;
    }
    
}
