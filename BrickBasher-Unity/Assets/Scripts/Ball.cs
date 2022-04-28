/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Ben Jenkins
 * Last Edited:4/28/2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public Text ballTxt; //text for lives remaining
    public Text scoreTxt; //text for current score
    public GameObject paddle; //reference to the paddle
    [HideInInspector] public AudioSource audioSource; //reference to the audioSource
    [HideInInspector] public int score; //current score

    [Header("Ball Settings")]
    public int numberOfBalls; //used as lives
    [HideInInspector] public bool isInPlay = false; //booleant to control if the game is live
    [HideInInspector] public Rigidbody rb; //reference to the rigidbody
    [HideInInspector] public float speed; //the speed of the ball
    public float InitialForce; //the force the ball leaves tha paddle at when the game starts
    [HideInInspector] public Vector3 initForce; //vector3 to format the initial force

 


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); //the rigidbody on the ball
        audioSource = this.gameObject.GetComponent<AudioSource>(); //the audio source of the ball to play the bounce effect
        initForce = new Vector3(0, InitialForce, 0); //format the inital force into a vector3
    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        ballTxt.text = "Balls: " + numberOfBalls; //update lives gui
        scoreTxt.text = "Score: " + score; //update score gui

        if (isInPlay == false) //if the game is not in play, have the ball follow the paddle
        {
            Vector3 Pos = transform.position;
            Pos.x = paddle.transform.position.x;
            transform.position = Pos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isInPlay == false) //if space is pressed and the game is not currently playing, restart the game
        {
            isInPlay = true;
            Move();
        }
        
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            Vector3 velocity = rb.velocity; //get the current velocity of the ball
            velocity+=speed*rb.velocity.normalized; //add to the current velocity the speed times the normalized velocity
            rb.velocity = velocity;
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    void Move()
    {
        rb.AddForce(initForce); //adds the inital force to the ball to start movement
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        audioSource.Play();

        if (otherGO.tag == "Brick") //if the ball touches a brick, destroy it and award 100 points
        {
            score += 100;
            Destroy(otherGO);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OutBounds")//if the ball goes out of bounds, lose a life and start over
        {
            numberOfBalls--;
            isInPlay = false;
        }
        if (numberOfBalls > 0) //as long as the player still has lives, reset to the starting position
        {
            SetStartingPos();
        }
    }
}
