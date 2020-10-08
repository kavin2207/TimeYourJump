using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    float gravity = 1f;
    float jumpForce;
    public float jumpMultipler;
    public Vector3 lastPosi;
    public GameObject lastPlatform;
    public int count = 0, hitValue = 10;
    [HideInInspector]
    public int block = 0;

    int spawnCount = 4;

    public int levelNumber = 1;
    [SerializeField]
    GameObject jumpEffect, speedUp, deathEffect;

    bool isDeath = false;

    int BaseGameEnd = 0; //three time base touch game end;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitValue = PlayerPrefs.GetInt("hitvalue", 10);
        spawnCount = PlayerPrefs.GetInt("spawnCount", 4);

    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("tag : "+col.transform.tag);
        if (col.transform.tag == "base")
        {
            if (rb.velocity.y <= 0f)
            {
                jumpForce = gravity * jumpMultipler;
                rb.velocity = new Vector2(0, jumpForce);
                effect();
                BaseGameEnd++;
            }
        }
        if (col.transform.tag == "platform")
        {
            if (count < hitValue)
            {
                if (rb.velocity.y <= 0f)
                {
                    jumpForce = gravity * jumpMultipler;
                    rb.velocity = new Vector2(0, jumpForce);
                    effect();
                    //FindObjectOfType<nextLevel>().LevelCount();
                    FindObjectOfType<ScoreManager>().AddScore();

                    makeAndDestroy(col);
                }
            }
        }

        if (!isDeath && col.transform.tag == "box")
        {
            isDeath = true;
            PlayerDeath();
            FindObjectOfType<GameManager>().gameOver();
        }

        if (col.transform.tag == "fullBlock")
        {
            block++;

            if (block < 1)
            {
                FindObjectOfType<Spawner>().Display();
            }
        }

        if (col.transform.tag == "LevelComplete")
        {

            Debug.Log("dead");

            level();
            PlayerPrefs.SetInt("hitvalue", hitValue);
            PlayerPrefs.SetInt("spawnCount", spawnCount);
            //PlayerPrefs.SetInt("levelNumber", levelNumber);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            FindObjectOfType<GameManager>().nextLevelAwake = true;
            FindObjectOfType<nextLevel>().newTry();

        }

    }

    void makeAndDestroy(Collision2D col)
    {
        col.gameObject.SetActive(false);
        count++;



        if (FindObjectOfType<player>().count < spawnCount)//by default 4
        {
            Spawner.instance.spawnWall();
        }


    }
    void SpeedUpEffect()
    {
        Vector3 newPosi = new Vector3(0, 0.5f, 0);
        Destroy(Instantiate(speedUp, transform.position + newPosi, Quaternion.identity), 0.2f);
    }
    void effect()
    {
        Destroy(Instantiate(jumpEffect, transform.position, Quaternion.identity), 0.1f);
    }

    void PlayerDeath()
    {
        //gameObject.SetActive(false);
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 0.5f);
    }

    void Update()
    {
        if (isDeath)
        {
            //gameObject.SetActive(false);
            return;
        }

        Debug.Log(gameObject);
        GameEnd();
        
        
        if(count == hitValue-2)
        {
            FindObjectOfType<Spawner>().Display();
            count++;
        }
        if (count == hitValue-1  && block<5)
        {
            rb.isKinematic = true;
            jumpForce = gravity * jumpMultipler;
            rb.velocity = new Vector2(0, jumpForce);
            SpeedUpEffect();
        }
    }

    void GameEnd()
    {
        if (!isDeath &&transform.position.y < Camera.main.transform.position.y -10)
        {
            //gameObject.SetActive(false);
            isDeath = true;
            //rb.velocity = Vector2.zero;
            PlayerDeath();
            FindObjectOfType<GameManager>().gameOver();
            
        }
        if(!isDeath && BaseGameEnd >= 3)
        {
            //gameObject.SetActive(false);
            isDeath = true;
            //gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            //PlayerDeath();
            FindObjectOfType<GameManager>().gameOver(); 
            BaseGameEnd = 0;
        }
        
    }
    
    void level()
    {
        hitValue++;
        spawnCount++;
        //levelNumber++;
    }


    
}
