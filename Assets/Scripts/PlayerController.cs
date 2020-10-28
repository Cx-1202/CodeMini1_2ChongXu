using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int planeB = -15;
    float speed = 10.0f;
    float zlimit = 9.0f;
    float xlimit = 9.3f;
    float minlimit = -4.34f;
    float maxlimit = 5.95f;
    float xminlimit = -9.3f;
    bool Touch;
    Rigidbody playerRb;
    float gravitymoldifier = 2.5f;



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravitymoldifier;
    }
  
    void Update()
    {
        
        
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        
        if (Touch == false)
        {
            if (transform.position.z > zlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zlimit);
            }
            else if (transform.position.z < -zlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zlimit);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            }


            if (transform.position.x > xlimit)
            {
                transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
            }
            else if ( transform.position.x <= xminlimit && transform.position.z < minlimit && transform.position.z > maxlimit )
            {
                transform.position = new Vector3(xminlimit, transform.position.y, transform.position.z);
                transform.position = new Vector3(transform.position.x, transform.position.y, minlimit);
                transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
            }

            /* if (transform.position.z < maxlimit && transform.position.x <= xminlimit)
             {
                 transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
             }*/





            /*else if (transform.position.x < -xlimit)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }*/

            if (transform.position.z < minlimit && transform.position.z > maxlimit && transform.position.x < -xlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minlimit);
                transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Touch = false;
        }
        else if ( collision.gameObject.CompareTag("PlaneB"))
        {
            Touch = true;
        }
    }


        }
