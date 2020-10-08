using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class nextLevel : MonoBehaviour
{
    public static nextLevel instance;

    //public Text levels;
    [SerializeField]
    TextMeshProUGUI currentLevelText;
    [SerializeField]
    TextMeshProUGUI levelText;
    
    public bool start = false;

    [SerializeField]
    GameObject gameStartUI,ScoreDes;

    public int check = 1;
    
    public GameObject player;

    public int currentLevel=1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = null;
        currentLevelText.text = PlayerPrefs.GetInt("levelNumber", 1).ToString();
        check = PlayerPrefs.GetInt("levelNumber", 1);
        //Debug.Log("current level : "+ currentLevelText.text);

    }

    public void newTry()
    {
        currentLevel = PlayerPrefs.GetInt("levelNumber", 1);
        currentLevel++;
        
        PlayerPrefs.SetInt("levelNumber",currentLevel);
    }

    

    void Update()
    { 
        tapToStart();
        startGame();
        if(check != 1)
        {
            gameStartUI.SetActive(false);
            start = true;
        }
    }

    public void startButton()
    {
        start = true;

    }

    void startGame()
    {
        if (start)
        {
            player.SetActive(true);
            ScoreDes.SetActive(true);
            //start = false;
        }
    }
    void tapToStart()
    {
        
        if (start)
        {
            gameStartUI.SetActive(false);

        }
    }

    public void Home()
    {
        check = 1;
        PlayerPrefs.SetInt("levelNumber", FindObjectOfType<nextLevel>().check);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
