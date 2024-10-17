using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool doorOpened = false; // Indique si la porte est ouverte

    void Start()
    {
        // Jouer l'animation CloseState au démarrage de la scène
        GetComponent<Animation>().Play("CloseState");
    }

    void Update()
    {
        // Vérifier si la touche "J" est enfoncée et que la porte n'est pas encore ouverte
        if (Input.GetKeyDown(KeyCode.J) && !doorOpened)
        {
            // Jouer l'animation OpenDoor
            GetComponent<Animation>().Play("Opendoor");

            // Marquer que la porte est ouverte
            doorOpened = true;
        }
    }
}
