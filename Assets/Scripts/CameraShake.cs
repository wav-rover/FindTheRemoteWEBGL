using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeMagnitude = 0.7f; // Amplitude du shake
    public bool isShaking = false; // Nouveau booléen pour contrôler le shake

    // Méthode pour commencer à secouer la caméra
    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    // Méthode pour arrêter de secouer la caméra
    public void StopShake()
    {
        isShaking = false;
    }

    private IEnumerator Shake()
    {
        isShaking = true;

        while (isShaking)
        {
            // Calculer un décalage aléatoire pour le shake
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Appliquer le décalage au positionnement de la caméra
            transform.localPosition += new Vector3(x, y, 0);

            yield return null;

            // Réinitialiser le décalage de la caméra à chaque frame
            transform.localPosition -= new Vector3(x, y, 0);
        }
    }
}