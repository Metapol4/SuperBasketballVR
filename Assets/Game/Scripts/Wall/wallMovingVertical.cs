using UnityEngine;

public class MovingWallVertical : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public float distance = 2f; // Distance maximale de déplacement

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(0f, distance, 0f); // Déplacement uniquement sur l'axe Y
    }

    void Update()
    {
        // Déplacement continu de haut en bas
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(Time.time * speed, 1));
    }
}
