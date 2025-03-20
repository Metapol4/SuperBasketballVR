using TMPro;
using UnityEngine;

public class ActivableWall : Activable
{
    public float speed = 2f; // Vitesse de déplacement
    public Vector3 offset = Vector3.zero;

    private Vector3 startPosition;
    private Vector3 endPosition;

    bool canMove = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + offset; // Déplacement uniquement sur l'axe Y
    }

    void Update()
    {
        if (canMove)
        {
            MoveWall();
        }
    }

    void MoveWall()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

        // Stop when reaching the target position
        if (Vector3.Distance(transform.position, endPosition) < 0.01f)
        {
            canMove = false; // Stop movement
        }
    }

    public override void Activate()
    {
        canMove = true;
    }
}
