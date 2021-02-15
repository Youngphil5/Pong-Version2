using UnityEngine;

public class Paddle : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioSource speaker;

    //-----------------------------------------------------------------------------
    public void MadeContact()
    {
        speaker.PlayOneShot(hitSound);
    }
}
