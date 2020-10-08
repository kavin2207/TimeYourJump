using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    bool isRewind = false;

    Rigidbody2D rb;

    List<Vector2> positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            startRewind();
            FindObjectOfType<player>().lastPlatform.SetActive(true);
        }

    }

    void FixedUpdate()
    {
        if (isRewind)
        {
            rewind();
        }
        else
        {
            Record();
        }
    }
    public void rewind()
    {
        //for (int i = 0; i < positions.Count; i++)
        //{
        if (transform.position != FindObjectOfType<player>().lastPosi)
        {
            Debug.Log("ho gya");
            //Debug.Log("positions" + transform.position);
            stopRewind();

        }
        //Debug.Log("inedex : "+Mathf.Round(positions[0].magnitude));

        transform.position = positions[0];
        positions.RemoveAt(0);

        //if (positions.Count == 0)
        //  stopRewind();

    }

    void Record()
    {

        if (positions.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            positions.RemoveAt(0);
        }

        positions.Insert(0, transform.position);
    }

    void startRewind()
    {
        isRewind = true;
        //rb.isKinematic = true;
    }

    void stopRewind()
    {
        isRewind = false;
        //rb.isKinematic = false;
    }
}
