using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    [SerializeField] private LevelTracker levelInfo;

    [SerializeField] private SpriteRenderer text;
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private SpriteRenderer bear;
    [SerializeField] private Sprite[] Texts;
    [SerializeField] private Sprite[] Players;
    [SerializeField] private Sprite[] Bears;
    [SerializeField] private Image fadeImg;
    [SerializeField] private GameObject potion;

    private bool fadingIn = true;
    private bool fadingOut = false;

    private int currentSprite = 0;
    private float fadeRate = .75f;

    // Start is called before the first frame update
    void Start()
    {
        fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1);
    }

    private void Update()
    {
        if (fadingIn)
        {
            if (fadeImg.color.a <= 0)
            {
                fadingIn = false;
                UpdateSprites();
            }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a - fadeRate * Time.deltaTime);
        }
        if (fadingOut)
        {
            if (fadeImg.color.a >= 1)
            {
                fadingOut = false;
                NextScene();
            }
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, fadeImg.color.a + fadeRate * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            levelInfo.currentLevel++;
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator SpriteCooldown()
    {
        yield return new WaitForSeconds(3f);
        currentSprite++;
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        if(currentSprite == 2)
        {
            Vector2 v = new Vector2(-.5f, -4.5f);
            potion = Instantiate(potion, v, Quaternion.identity);
        }
        if(currentSprite==4)
        {
            Destroy(potion);
        }
        if (currentSprite < Texts.Length)
        {
            text.sprite = Texts[currentSprite];
            player.sprite = Players[currentSprite];
            bear.sprite = Bears[currentSprite];
            StartCoroutine(SpriteCooldown());
        }
        else
        {
            fadingOut = true;
        }
    }

    private void NextScene()
    {
        levelInfo.currentLevel++;
        SceneManager.LoadScene(2);
    }
}
