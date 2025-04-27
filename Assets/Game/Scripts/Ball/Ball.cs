using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float TimeToReturnBall = 5.0f;
    private Vector3 OriginalPosition;
    private bool returning = false;
    private Coroutine returnRoutine;

    private XRGrabInteractable xRGrabInteractable;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }
    void Start()
    {
        //Bind to a event to know when the input for calling the ball back is trigger
        xRGrabInteractable.selectExited.AddListener(BallThrown);
        //Get spawning position of the ball to return it there
        OriginalPosition = transform.position;
    }

    private void BallThrown(SelectExitEventArgs args)
    {
        if(returning)
        {
            StopCoroutine(returnRoutine);
            returning = false;
        }
        
        returnRoutine = StartCoroutine(ReturnBallIn(TimeToReturnBall));
    }

    private IEnumerator ReturnBallIn(float seconds)
    {
        returning = true;
        yield return new WaitForSeconds(seconds);
        transform.position = OriginalPosition;
        rb.linearVelocity = Vector3.zero;
        returning = false;
    }
}
