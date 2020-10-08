using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameEndPanel,player,platformManager,NextLevelUI;

    public bool nextLevelAwake = false;
    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void gameOver()
    {
        FindObjectOfType<nextLevel>().start = false;
        //nextLevelAwake = true;
        //NextLevelUI.SetActive(true);
        player.SetActive(false);
        //player.SetActive(false);
        StartCoroutine(GameOverCorotine());
    }

    void Update()
    {
        if (nextLevelAwake)
        {
            NextLevelUI.SetActive(true);
            nextLevelAwake = false;
        }
    }

    IEnumerator GameOverCorotine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(1f);
        GameEndPanel.SetActive(true);
        
        platformManager.SetActive(false);

        yield break;
    }


    public void Restart()
    {
        Debug.Log("dead");
        //nextLevelAwake = true;
        FindObjectOfType<nextLevel>().start = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelStart()
    {
        FindObjectOfType<nextLevel>().start = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<nextLevel>().check = PlayerPrefs.GetInt("levelNumber", 1);
        FindObjectOfType<nextLevel>().check++;

        PlayerPrefs.SetInt("levelNumber", FindObjectOfType<nextLevel>().check);
    }
    public void reset()
    {
        PlayerPrefs.DeleteAll();
    }


}
