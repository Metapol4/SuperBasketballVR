using UnityEngine;
using UnityEngine.Audio;

public class Button : MonoBehaviour
{
    public Activable ObjectToActivate;

    private bool IsActive = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the ball and if the button is not already active
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
