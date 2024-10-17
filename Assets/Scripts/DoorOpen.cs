using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private int clickCount = 0; // Compteur de clics
    private bool isRotating = false; // Indique si la porte est en train de tourner

    // Update est appelée une fois par frame
    void Update()
    {
        // Si la touche J est enfoncée et que la porte n'est pas déjà en train de tourner
        if (Input.GetKeyDown(KeyCode.J) && !isRotating)
        {
            StartRotation();
        }
    }

    public void StartRotation()
    {
        if (!isRotating)
        {
            Debug.Log("Touche J enfoncée"); // Log pour le débogage
            clickCount++; // Incrémente le compteur de clics
            StartCoroutine(RotateDoor(clickCount % 2 == 0 ? -100 : 100)); // Démarre la coroutine pour faire tourner la porte
        }
    }

    public IEnumerator RotateDoor(float rotationAmount)
    {
        Debug.Log("Début de la rotation de la porte"); // Log pour le débogage
        isRotating = true; // Indique que la porte est en train de tourner

        float rotationSpeed = 100f; // Vitesse de rotation
        float totalRotation = 0; // Rotation totale effectuée jusqu'à présent

        // Tant que la rotation totale est inférieure à la rotation souhaitée
        while (Mathf.Abs(totalRotation) < Mathf.Abs(rotationAmount))
        {
            float currentRotation = rotationSpeed * Time.deltaTime; // Calcule la rotation pour cette frame
            transform.Rotate(Vector3.forward, rotationAmount < 0 ? -currentRotation : currentRotation); // Fait tourner la porte
            totalRotation += currentRotation; // Ajoute la rotation de cette frame à la rotation totale

            // Si la porte est en train de se fermer et a atteint ou dépassé 326.82 sur l'axe Z, arrête la rotation
            if (rotationAmount < 0 && transform.rotation.eulerAngles.z >= 326.82)
            {
                break;
            }

            yield return null; // Attend la prochaine frame
        }

        isRotating = false; // Indique que la porte a fini de tourner
        Debug.Log("Fin de la rotation de la porte"); // Log pour le débogage
    }
}