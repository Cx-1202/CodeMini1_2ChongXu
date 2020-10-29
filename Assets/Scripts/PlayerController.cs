using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int walltouched = 0;
    int jumpforce = 15;
    int jumpinit = 0;
    int planeB = -15;
    float speed = 10.0f;
    float zlimit = 9.0f;
    float xlimit = 9.95f;
    float zminlimit = -4.75f;
    float zmaxlimit = 3.92f;
    float xminlimit = -19.54f;
    float xmaxlimit = -10.1f;
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
        jumping();

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if ( walltouched > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }


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
            else if (transform.position.x < -xlimit && transform.position.z > 5)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -xlimit && transform.position.z < -5)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
            }
        }
        else
        {
            if (transform.position.z > zmaxlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zmaxlimit);
            }
            else if (transform.position.z < zminlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zminlimit);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            }

            if (transform.position.x < xminlimit)
            {
                transform.position = new Vector3(xminlimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("Plane A");
            Touch = false;
        }
        else if (collision.gameObject.CompareTag("PlaneB"))
        {
            Debug.Log("Plane B");
            Touch = true;
        }

        if (collision.gameObject.CompareTag("wall"))
        {
            Debug.Log("Touch wall");
            walltouched += 1;
        }
    }
    private void jumping()
    {
       
            if (Input.GetKeyDown(KeyCode.Space) && jumpinit < 2)
            {
                Debug.Log("Player JUMPING Y Position: " + transform.position.y);
                playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

                jumpinit++;
            }
        
    }

}
