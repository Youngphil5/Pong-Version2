using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public float startSpeed;
    public float step;
    public bool useDebugVisualization;
    private AudioSource speaker;
    public AudioClip leftSound;//sound for leftpaddle
    public AudioClip rightSound;//sound for rightpaddle
    public AudioClip upgradeSound1;
    public AudioClip upgradeSound2;
    public AudioClip upgradeSound3;
    private float percentOfMax;// had to move it here because of powerups
    private float newHorizontalSpeed;// had to move it here because of powerups
    private float speed;
    private Rigidbody rb;

    //-----------------------------------------------------------------------------
    void Awake()
    {
        speaker = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    //-----------------------------------------------------------------------------
    // Update is called once per frame
    public void Restart()
    {
        speed = startSpeed;
        rb.MovePosition(Vector3.zero);
        
        gameObject.GetComponent<Renderer>().material.color = Color.white;
         
    }

    //-----------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight")
        {
            if (collision.gameObject.name == "PaddleLeft")
            {
                speaker.PlayOneShot(leftSound);
            }
            else
            {
                speaker.PlayOneShot(rightSound);
            }

            speed += step;
            float heightAboveOrBelow = transform.position.z - collision.transform.position.z;
            float maxHeight = collision.collider.bounds.extents.z;
            percentOfMax = heightAboveOrBelow / maxHeight;

            if (useDebugVisualization)
            {
                DebugDraw.DrawSphere(transform.position, 0.5f, Color.green);
                DebugDraw.DrawSphere(collision.transform.position, 0.5f, Color.red);
                Debug.Break();
                Debug.Log($"percent height = {percentOfMax}");
            }

            bool hitLeftPaddle = collision.gameObject.name == "PaddleLeft";
             newHorizontalSpeed = (hitLeftPaddle) ? speed : -speed;

            Vector3 newVelocity = new Vector3(newHorizontalSpeed, 0f, percentOfMax * 4f).normalized * speed;
            rb.velocity = newVelocity;
            
           //-----UPGRADE SOUNDS --------

            if (speed == 10)
            {
                GetComponent<Renderer>().material.color = Color.blue;
                speaker.PlayOneShot(upgradeSound1);
            }
            else if (speed == 20)
            {
                speaker.PlayOneShot(upgradeSound2);
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (speed == 30)
            {
                GetComponent<Renderer>().material.color = Color.red;
                speaker.PlayOneShot(upgradeSound3);
            }
        }
    }
//-------------------- Power Ups -----------------------------------------------------------------------
    public void manipulateSpeed()
    {
        Vector3 newVelocity = new Vector3(newHorizontalSpeed, 0f, percentOfMax * 4f).normalized * speed*3;
        rb.velocity = newVelocity;
            
        }
    public void BlackBall()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
    
}
