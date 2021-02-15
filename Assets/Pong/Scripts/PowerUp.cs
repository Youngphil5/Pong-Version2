using System;
using System.Collections;
using System.Timers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUp : MonoBehaviour
{
    private int randomNumber;
    public Ball ball;
    private Color previousColor;
    //-----------------------------------------------------------------------------
    private void Start()
    {
        StartCoroutine(PowerUpTimeDelay(7));
    }
    
    private void Update()
    {
        randomNumber = Random.Range(0, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Renderer>().material.color == Color.blue)
        {
            GetComponent<Renderer>().material.color = Color.white;
            StartCoroutine(PowerUpTimeDelay(10));

            if (randomNumber == 1)
            {
                ball.manipulateSpeed();
            }
            else
            {
                previousColor = other.gameObject.GetComponent<Renderer>().material.color;
                ball.BlackBall();
                StartCoroutine(blackBallTimeDelay());
            }
        }
    }
// PowerUp Delay Times
    IEnumerator PowerUpTimeDelay(int timer)
    {
        yield return new WaitForSeconds(timer);
        GetComponent<Renderer>().material.color = Color.blue;
    }
    IEnumerator blackBallTimeDelay()
    {
        yield return new WaitForSeconds(1);
        ball.gameObject.GetComponent<Renderer>().material.color = previousColor;
    }
}
