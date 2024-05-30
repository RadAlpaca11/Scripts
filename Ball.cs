using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Tooltip("This is the speed of the ball.")]
    public float speed;
    private float currentSpeed;

    public Rigidbody2D rb;

    [Tooltip("This is the score that will end the game.")]
    public int winningScore;

    private int rand;
    
    private float ballDirX;

    private float ballDirY;
    
    private int player1Score;
    private int player2Score;
    // The number of paddle hits since start/last obstacle spawn
    [HideInInspector] public int obstacleProgressCount;

    [HideInInspector] public int paddleHeight;
    public PlayerPaddle Player1;
    public PlayerPaddle Player2;

    [Tooltip("Must be the same as Object Spawn's Obstacle Target Count.")]
    public int obstacleTargetCount;

    // Use to record the position of the paddle when collided
    private float paddlePosY;
    private float collisionPointY;

    private Vector2 position;

    [Tooltip("This is the seconds that will wait before launching the ball.")]
    public float countdown;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
        Debug.Log(paddleHeight);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(ballDirX, ballDirY).normalized * currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        ContactPoint2D contact = collision.GetContact(0);
        // Gets the position of the contact point as a 2D vector
        position = contact.point;

        if (collision.gameObject.CompareTag("Player1"))
        {
            // Gets the y position of the paddle
            paddlePosY = Player1.transform.position.y;
            // Gets the difference in the y axis between the position of the paddle and contact point
            ballDirY = position.y - paddlePosY;
            
            ballDirX = -ballDirX;
            
            currentSpeed += 0.25f;
            // *Add one to obstacle generator count
            obstacleProgressCount += 1;
            
        }

        if (collision.gameObject.CompareTag("Player2"))
        {
            paddlePosY = Player2.transform.position.y;
            ballDirY = position.y - paddlePosY;

            ballDirX = -ballDirX;
            currentSpeed += 0.25f;
            obstacleProgressCount +=1;
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            ballDirY = -ballDirY;
        }
        
        if(collision.gameObject.CompareTag("Player1Goal"))
        {
            player2Score += 1;
            Debug.Log("Player1:" + player1Score + "Player2:" + player2Score);
            ResetBall();
        }

        if(collision.gameObject.CompareTag("Player2Goal"))
        {
            player1Score += 1;
            Debug.Log("Player1:" + player1Score + "Player2:" + player2Score);
            ResetBall();
        }
    }
    
    void ResetBall()
    {
        transform.position = new Vector2(0, 0);
        rb.velocity = position;
        currentSpeed = speed;
        obstacleProgressCount = 0;
        rand = Random.Range(0, 2);

        Player1.ResetPaddle();
        Player2.ResetPaddle();
        
        
        // Launches to the left, towards player 1
        if(rand == 0)
        {
            ballDirX = -1;
            ballDirY = Random.Range(-2.0f, 2.0f);
        }
        // Launches to the right, towards player 2
        else if(rand == 1)
        {
            ballDirX = 1;
            ballDirY = Random.Range(-2.0f, 2.0f);
        }

        rb.velocity = new Vector2(currentSpeed * ballDirX, currentSpeed * ballDirY);
        
    }
}
