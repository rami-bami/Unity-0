using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private int count;
    private int lives;

    private float movementX;
    private float movementY;

    private Transform spawnPoint;
    private Transform playerPos;
    private Transform checkPoint;
    private Transform checkPoint2;
    private Transform checkPoint3;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI liveText;
    public float jumpForce;
    public GameObject winTextObject;
    public GameObject gameOverObject;

    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; 
        lives = 3;
        SetCountText();
        SetLiveText();
        winTextObject.SetActive(false);
        gameOverObject.SetActive(false);

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        checkPoint = GameObject.FindGameObjectWithTag("CheckPoint").transform;
        checkPoint2 = GameObject.FindGameObjectWithTag("CheckPoint2").transform;
        checkPoint3 = GameObject.FindGameObjectWithTag("CheckPoint3").transform;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
        if (count >= 15)
       {
           winTextObject.SetActive(true);
       }
   }

   void SetLiveText()
   {
        liveText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            gameOverObject.SetActive(true);
            speed = 0;
        }
   }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Dead"))
        {
            playerPos.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
            lives =  lives - 1;
            SetLiveText();

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerPos.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
            lives =  lives - 1;
            SetLiveText();

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            spawnPoint.position = new Vector3(checkPoint.position.x, checkPoint.position.y, checkPoint.position.z);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("CheckPoint2"))
        {
            spawnPoint.position = new Vector3(checkPoint2.position.x, checkPoint2.position.y, checkPoint2.position.z);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("CheckPoint3"))
        {
            spawnPoint.position = new Vector3(checkPoint3.position.x, checkPoint3.position.y, checkPoint3.position.z);
            other.gameObject.SetActive(false);
        }
    }
}
