using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip stepFoot;
    [SerializeField] AudioClip stepFoot2;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource fxSource;

    public void OnStepFoot()
    {
        fxSource.PlayOneShot(stepFoot);
    }
    
}
