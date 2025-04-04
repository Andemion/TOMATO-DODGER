using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Référence au joueur
    public float smoothTime = 0.3f; // Pour lisser le mouvement de la caméra

    public Vector2 minPosition;     // Position minimale (en bas à gauche du décor)
    public Vector2 maxPosition;     // Position maximale (en haut à droite du décor)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            // Nouvelle position cible (avec la hauteur de la caméra conservée)
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Appliquer le smoothing
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Limiter la position avec Clamp
            float clampedX = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
            float clampedY = Mathf.Clamp(smoothPosition.y, minPosition.y, maxPosition.y);

            transform.position = new Vector3(clampedX, clampedY, smoothPosition.z);
        }else{
            Debug.Log("no player");
        }
    }
}
