using UnityEngine;

public class PressurePad2 : MonoBehaviour
{
    public bool hasGreenKey = false; // Indicateur pour savoir si la GreenKeyCrystal est présente
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
        if (other.CompareTag("GreenKeyCrystal"))
        {
            hasGreenKey = true;
            // Activez l'objet des particules
            particlesObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GreenKeyCrystal"))
        {
            hasGreenKey = false;
            // Désactivez l'objet des particules
            particlesObject.SetActive(false);
        }
    }
}
