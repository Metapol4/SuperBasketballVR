using UnityEngine;

public class Net : MonoBehaviour
{

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //trigger win here
            Debug.Log("WIN!!!!!");
        }
    }
}
