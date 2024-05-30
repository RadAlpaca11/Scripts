using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [Tooltip("This is the speed of the paddle.")]
    public float speed;

    [Tooltip("This is the key pressed to make the paddle go up.")]
    public string upKey;

    [Tooltip("This is the key pressed to make the paddle go down.")]
    public string downKey;

    [Tooltip("Enter the height of the paddle here.")]
    public float paddleHeight;

    private Vector2 screenBounds;

    private float screenHeight;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenHeight = screenBounds.y;
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        Vector3 startPosition = gameObject.transform.position;

        if(upKey == "w")
        {
            startPosition.x = -8.25f;
            startPosition.y = 0;
        }
        else if(upKey == "up")
        {
            startPosition.x = 8.25f;
            startPosition.y = 0;
        }
        gameObject.transform.position = startPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        // FunSpec: As a user, I want W and S to cause the paddle to go up and down
        Vector3 position = gameObject.transform.position;
        float delta = 0;


        // Check upper screen bounds
        if(position.y <=  (screenHeight - paddleHeight/2))
        {
            if(Input.GetKey(upKey)) delta += speed;
        }

        // Check lower screen bounds
        if(position.y >= (- screenHeight + paddleHeight/2))
        {
            if(Input.GetKey(downKey)) delta = -speed;
        }
        
        // Move paddle
        position.y += delta * Time.deltaTime;
        gameObject.transform.position = position;
    }
}
