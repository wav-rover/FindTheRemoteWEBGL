using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    // Référence à la caméra du joueur
    public Camera playerCamera;

    // Distance à laquelle l'objet peut être ramassé
    public float pickupRange = 3f;

    // Vitesse à laquelle l'objet est ramassé
    public float pickupSpeed = 10f;

    public GameObject pickupUI;

    // Indique si l'objet est actuellement ramassé
    private bool isPickedUp = false;

    // Référence à l'objet actuellement ramassé
    private Transform pickedUpObject;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Vérifie si le joueur appuie sur le bouton de la souris pour ramasser ou lâcher un objet
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isPickedUp)
            {
                Pickup();
            }
            else
            {
                Drop();
            }
        }

        // Si un objet est ramassé, déplace-le vers la position de la caméra du joueur
        if (isPickedUp)
        {
            MovePickedUpObject();
        }


        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Ramassable") || hit.collider.CompareTag("RedKeyCrystal") || hit.collider.CompareTag("GreenKeyCrystal") || hit.collider.CompareTag("BlueKeyCrystal"))
            {
                pickupUI.SetActive(true);
            }
            else
            {
                pickupUI.SetActive(false);
            }
        }
        else
        {
            pickupUI.SetActive(false);
        }
        
    }

        void Pickup()
    {
        
        // Lancer un rayon depuis la caméra du joueur pour détecter les objets à ramasser
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            // Récupérer la référence de l'objet ramassé
            Transform objectToPickup = hit.collider.transform;

            // Vérifier si l'objet détecté est ramassable
            if (objectToPickup.CompareTag("Ramassable") || hit.collider.CompareTag("RedKeyCrystal") || hit.collider.CompareTag("GreenKeyCrystal") || hit.collider.CompareTag("BlueKeyCrystal"))
            {

                // Mettre à jour l'état du ramassage
                pickedUpObject = objectToPickup;
                isPickedUp = true;

                // Désactiver la gravité de l'objet ramassé
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }


    void MovePickedUpObject()
    {
        // Déplacer l'objet ramassé vers la position de la caméra du joueur avec une interpolation
        pickedUpObject.position = Vector3.Lerp(pickedUpObject.position, playerCamera.transform.position + playerCamera.transform.forward * pickupRange, pickupSpeed * Time.deltaTime);
    }

    void Drop()
    {
        // Réactiver la gravité de l'objet lâché
        pickedUpObject.GetComponent<Rigidbody>().useGravity = true;

        // Réinitialiser les variables de ramassage
        isPickedUp = false;
        pickedUpObject = null;
    }
}
