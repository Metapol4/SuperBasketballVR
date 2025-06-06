using UnityEngine;
using UnityEngine.Events;

public class Net : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Confetti;
    private AudioSource audioSource;

    public UnityEvent BallEnterNet;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Confetti.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the ball and the trigger end level logic
        if (other.gameObject.CompareTag("Ball"))
        {
            //trigger win here
            Debug.Log("WIN!!!!!");
            Confetti.gameObject.SetActive(true);
            Confetti.Play();
            audioSource.Play();
            BallEnterNet.Invoke();
        }
    }
}
