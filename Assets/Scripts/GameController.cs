using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Transform player;
    [SerializeField] private Transform playerRef;

    
    [SerializeField] private TextMeshProUGUI[] deathUI;
    [SerializeField] private Image fadeImg;
    private bool fading = false;
    private bool fadingIn = false;
    [SerializeField] private float fadeRate;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            if (fadeImg.color.a >= 1)
            {
                Debug.Log("hi");
                fadingIn = false;
            }
            if (fadeImg.color.a >= .70)
            {
                FadeTextIn();
            }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a + fadeRate * Time.deltaTime);
        }
        if (fading)
        {
            foreach (TextMeshProUGUI t in deathUI)
            {
                if (t.color.a >= 1)
                {
                    fading = false;
                }
                t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + fadeRate * Time.deltaTime);
            }
        }
    }

    public void LoadLevel(int val)
    {
        SceneManager.LoadScene(val);
    }

    public void Die()
    {

        fadingIn = true;
    }
    private void FadeTextIn()
    {
        fading = true;
    }
}
