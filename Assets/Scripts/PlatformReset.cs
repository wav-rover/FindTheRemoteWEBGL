using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReset : MonoBehaviour
{
    public PlatformMoving platformMoving;
    public float resetDelay = 3f; // Délai de réinitialisation en secondes

    private float lastMovementTime;

    void Start()
    {
        lastMovementTime = Time.time;
    }
 
    void Update()
    {
        if (platformMoving.moving) // Si la plateforme est en mouvement
        {
            lastMovementTime = Time.time; // Mettre à jour le temps du dernier mouvement
        }
        else if (Time.time - lastMovementTime >= resetDelay) // Si la plateforme n'est pas en mouvement depuis le délai spécifié
        {
            ResetPlatform(); // Réinitialiser la plateforme
        }
    }

    void ResetPlatform()
    {
        platformMoving.transform.position = platformMoving.points[0].position; // Réinitialiser la position de la plateforme
        platformMoving.currentPointIndex = 0; // Réinitialiser l'index de la destination actuelle
        platformMoving.movingForward = true; // Réinitialiser la direction du mouvement
    }
}
