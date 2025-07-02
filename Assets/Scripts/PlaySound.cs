using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource clip;
    
    public void PLaySound()
    {
        clip.Play();
    }
}
