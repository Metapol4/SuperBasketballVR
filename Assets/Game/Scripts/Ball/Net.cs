using UnityEngine;

public class Net : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Confetti;
    private void Awake()
    {
        Confetti.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //trigger win here
            Debug.Log("WIN!!!!!");
            Confetti.gameObject.SetActive(true);
            Confetti.Play();
        }
    }
}
