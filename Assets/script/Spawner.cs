using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject wallPrefav,platformPrefav,fullwall,sideWallLeft,sideWallRight, LevelComplete;

    SpriteRenderer sr;

    int index = 0;
    float platformIndex = 0;
    // Start is called before the first frame update

    [HideInInspector]
    public static Spawner instance = null; //singleton

    List<GameObject> wall_obj = new List<GameObject>();
   
    public bool checkSide = false;

    [SerializeField]
    float RandomColor, midC = 0.5f,rigC=0.5f;
    GameObject temp;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        temp = wallPrefav;
        if (instance == null)
            instance = this;
        //makeObject();
        colorChanger();
        for (int i = 0; i < 5; i++)
        {
            
            spawnWall();

        }

    }

    
    public void spawnWall()
    {

        float wallX, platformX,multiFactor = 6.8f,sideWall = 22f,sideLeft = -5.5f,sideRight=5.5f;
        wallX = 5.5f;
        platformX = 5f;
        int x;
        x = Random.Range(0, 2);

        if (x == 0)
        {
            wallX = wallX * -1;
            platformX = platformX * -1;
        }

        Vector2 nextposi = new Vector2(wallX, index * multiFactor);
        Vector2 platformNextPosi = new Vector2(platformX, platformIndex * multiFactor);
        Vector2 sideLeftWall = new Vector2(sideLeft, index * sideWall);
        Vector2 sideRightWall = new Vector2(sideRight, index * sideWall);

        GameObject wall = Instantiate(temp, nextposi, Quaternion.identity);
        GameObject platformObj = Instantiate(platformPrefav, platformNextPosi+new Vector2(0,3.5f), Quaternion.identity);
        GameObject leftWall = Instantiate(sideWallLeft, sideLeftWall, Quaternion.identity);
        GameObject rightWall = Instantiate(sideWallRight, sideRightWall, Quaternion.identity);

        wall_obj.Add(wall);
        //platform_obj.Add(platformObj);
        DestroyWall();

        wall.transform.SetParent(transform);
        platformObj.transform.SetParent(transform);
        leftWall.transform.SetParent(transform);
        rightWall.transform.SetParent(transform);


        //Sprite color
        wallColor(wall);
        leftWall.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(RandomColor, midC, rigC);
        rightWall.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(RandomColor, midC, rigC);


        // for opacity
        Color tmp = leftWall.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.56f;
        leftWall.GetComponent<SpriteRenderer>().color = tmp;
        Color tmp2 = rightWall.GetComponent<SpriteRenderer>().color;
        tmp2.a = 0.56f;
        rightWall.GetComponent<SpriteRenderer>().color = tmp2;





        int length = Random.Range(2, 5);
        float hieght = 0.8f;
        platformObj.transform.localScale = new Vector2(length, hieght);

        index++;
        platformIndex++;

    }

    void colorChanger()
    {
        RandomColor = UnityEngine.Random.Range(0f, 1f);
        Camera.main.backgroundColor = Color.HSVToRGB(RandomColor, 0.6f, 0.8f);
    }

    void wallColor(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(RandomColor, midC, rigC);
    }

    void DestroyWall()
    {
        foreach (GameObject obj in wall_obj) {
            if (obj.transform.position.y < Camera.main.transform.position.y - 20)
            {
                //wall_obj.Remove(obj.gameObject);
                //Debug.Log(obj.transform.name);
                obj.SetActive(false);
            }
        }
    }

    public void Display()
    {
        Vector3 newPosi = new Vector3(0, 5f, 0);
        GameObject spawn = Instantiate(fullwall, FindObjectOfType<player>().gameObject.transform.position + newPosi, Quaternion.identity);
        Vector3 posi = spawn.transform.position + newPosi;
        for (int i = 0; i < 5; i++)
        {
            Instantiate(fullwall, posi, Quaternion.identity);
            posi = posi + newPosi;
        }
        levelCom(posi);
    }

    public void levelCom(Vector3 posi)
    {
        Vector3 newPosi = new Vector3(0, 8f, 0);
        Instantiate(LevelComplete, posi, Quaternion.identity);
    }
}
