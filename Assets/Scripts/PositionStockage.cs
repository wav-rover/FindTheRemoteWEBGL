using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Enregistrer la position et la rotation initiales
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Autres méthodes de votre script pour déplacer le personnage, etc.
}
