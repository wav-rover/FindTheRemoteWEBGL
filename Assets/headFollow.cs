using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public Vector3 offset; // Décalage de la caméra par rapport au joueur
    public float smoothSpeed = 0.125f; // Vitesse de lissage

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcule la position souhaitée
            Vector3 desiredPosition = player.position + offset;
            // Lisse le mouvement vers la position souhaitée
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
