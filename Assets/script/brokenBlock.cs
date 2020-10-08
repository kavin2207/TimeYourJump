using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenBlock : MonoBehaviour
{
    [SerializeField]
    GameObject brokenWall;
    
   
    void FixedUpdate()
    {
        
        
        if(FindObjectOfType<player>().block >= 4)
        {
            FindObjectOfType<player>().count = 0;
            Debug.Log(FindObjectOfType<player>().count + "khqdl");
        }
        
    }
    

    void Break()
    {
        Instantiate(brokenWall, transform.position, Quaternion.identity);
        
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.transform);
        if (col.transform.tag == "Player")
        {
            Break();
        }
    }

}
