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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xRGrabInteractable.selectExited.AddListener(BallThrown);
        OriginalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
