using UnityEngine;
using UnityEngine.Audio;

public class Button : MonoBehaviour
{
    public Activable ObjectToActivate;

    private bool IsActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && !IsActive)
        {
            if(ObjectToActivate)
            {
                IsActive = true;
                ObjectToActivate.Activate();
            }
        }
    }
}
