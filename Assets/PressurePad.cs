using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public bool hasRedKey = false; // Indicateur pour savoir si la RedKeyCrystal est présente
    private GameObject particlesObject; // Référence à l'objet des particules

    private void Start()
    {
        // Obtenez une référence à l'objet des particules
        particlesObject = transform.Find("Particles").gameObject;
        // Désactivez-le initialement
        particlesObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedKeyCrystal"))
        {
            hasRedKey = true;
            // Activez l'objet des particules
            particlesObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedKeyCrystal"))
        {
            hasRedKey = false;
            // Désactivez l'objet des particules
            particlesObject.SetActive(false);
        }
    }
}
