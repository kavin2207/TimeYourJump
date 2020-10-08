using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField]
    public Transform leftPosi, rightPosi;
    
    public bool check = false;
    float speed = 8f;
    int k = 0,Raydistance = 20;
    Rigidbody2D rb;
    float grav;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grav = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayLeft = Physics2D.Raycast(leftPosi.position, -transform.right* Raydistance);
        RaycastHit2D rayRight = Physics2D.Raycast(rightPosi.position, transform.right * Raydistance);

        diffculty();


        if (rayLeft.collider != null)
        {
            if (rayLeft.transform.tag == "Player")
            {
                check = true;
                k = 1;
                Debug.DrawLine(leftPosi.position, rayLeft.point, Color.red);
            }
        }
        else
        {
            Debug.DrawLine(leftPosi.position, transform.position - transform.right * Raydistance, Color.green);
        }
        if (rayRight.collider != null)
        {

            if (rayRight.transform.tag == "Player")
            {
                check = true;
                k = -1;
                Debug.DrawLine(rightPosi.position, rayRight.point, Color.red);
            }
        }
        else
        {
            Debug.DrawLine(rightPosi.position, transform.position + transform.right * Raydistance, Color.green);
            Debug.DrawLine(rightPosi.position, transform.position + transform.right * Raydistance, Color.green);
        }
            if (check)
            {
                move();
            }
        }
        void move()
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.S))//Input.GetKey(KeyCode.Mouse0)
            {
                if (k > 0)
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    
                }
                else if (k < 0)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    
                }
            }
        }

    void diffculty()
    {
        if (FindObjectOfType<nextLevel>().currentLevel % 5 == 0)
        {
            grav += 0.5f;
            rb.gravityScale = grav;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.transform.tag);
        if(col.transform.tag == "base")
        {
            Destroy(gameObject);
        }
    }
}
