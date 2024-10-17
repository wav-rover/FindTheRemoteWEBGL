using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Vector3 MoveDir;
    private Vector2 inputVector;
    public CharacterController characterController;
    Vector3 PlayerVelocity;
    float PlayerSpeed = 5;
    float Gravity = 9.81f;
    private Animator animator;
    private float jumpForce = 6.0f;
    private bool isJumping = false;
    public Camera playerCamera;
    public float pickupRange = 3f;
    public GameObject telecomande_bouton;
    public GameObject telecomande_Led;
    public Light World_Light;
    public float pickupSpeed = 10f;
    public Transform wrist;
    public GameObject pickupUI;
    private bool isPickedUp = false;
    private Transform pickedUpObject;
    public Transform mainDuPersonnage;
    public float forceLancer = 10f;
    public float distanceMaxRamassage = 3f;
    private GameObject objetEnMain;


    // Start is called before the first frame update
    void Start()
    {
        MoveDir = Vector3.zero;
        animator = GetComponent<Animator>();
    }

    public void OnGrab(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            if (objetEnMain != null)
            {
                PoserObjet();
            }
            else
            {
                RamasserObjet();
            }
        }
    }

    void PoserObjet()
    {
        // Remettre l'objet au sol
        objetEnMain.transform.SetParent(null);
        Rigidbody rb = objetEnMain.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        objetEnMain = null;
        animator.SetBool("Telecommande", false);
    }

    IEnumerator InteragirTelecommande()
    {
        // Lancer l'intéraction de la télécommande
        animator.SetBool("TelecommandePush", true);
        yield return StartCoroutine(AnimateButton());
        animator.SetBool("TelecommandePush", false);
    }

    IEnumerator AnimateButton()
    {
        yield return new WaitForSeconds(0.85f);
        // Réduire l'échelle du bouton à 0.23
        Vector3 targetScale = new Vector3(0.792043f, 0.3946826f, 0.23f);
        telecomande_bouton.transform.localScale = targetScale;

        yield return new WaitForSeconds(1f); // Attendre pendant 0.5 secondes

        Light telecommandeLed = telecomande_Led.GetComponent<Light>();
        telecommandeLed.intensity = 0f;
        yield return new WaitForSeconds(0.5f); // Attendre pendant 0.5 secondes
        telecommandeLed.intensity = 30f;
        yield return new WaitForSeconds(0.5f); // Attendre pendant 0.5 secondes
        telecommandeLed.intensity = 0f;
        yield return new WaitForSeconds(0.5f); // Attendre pendant 0.5 secondes
        telecommandeLed.intensity = 30f;
        yield return new WaitForSeconds(0.5f); // Attendre pendant 0.5 secondes
        telecommandeLed.intensity = 0f;
        yield return new WaitForSeconds(0.5f); // Attendre pendant 0.5 secondes
        telecommandeLed.intensity = 30f;
        yield return new WaitForSeconds(1f); // Attendre pendant 0.5 secondes

        // Remettre l'échelle du bouton à sa taille d'origine
        targetScale = new Vector3(0.792043f, 0.3946826f, 0.4297258f);
        telecomande_bouton.transform.localScale = targetScale;
    }

    void RamasserObjet()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distanceMaxRamassage))
        {
            if (hit.collider.CompareTag("Ramassable") || hit.collider.CompareTag("RedKeyCrystal") || hit.collider.CompareTag("GreenKeyCrystal") || hit.collider.CompareTag("BlueKeyCrystal"))
            {
                objetEnMain = hit.collider.gameObject;
                objetEnMain.GetComponent<Rigidbody>().isKinematic = true;
                animator.SetBool("Telecommande", true);
                objetEnMain.transform.SetParent(mainDuPersonnage);
                Vector3 customPosition = new Vector3(0.155f, 0.027f, 0.09f);
                objetEnMain.transform.localPosition = customPosition;
                Quaternion rotation = Quaternion.Euler(34.9f, -54.8f, -121f);
                objetEnMain.transform.localRotation = rotation;
                objetEnMain.GetComponent<Rigidbody>().useGravity = false;
            }
            else if (hit.collider.CompareTag("Telecommande")) // Nouvelle condition pour ramasser la télécommande
            {
                objetEnMain = hit.collider.gameObject;
                objetEnMain.GetComponent<Rigidbody>().isKinematic = true;
                animator.SetBool("Telecommande", true);
                objetEnMain.transform.SetParent(mainDuPersonnage);
                Vector3 customPosition = new Vector3(0.155f, 0.027f, 0.09f);
                objetEnMain.transform.localPosition = customPosition;
                Quaternion rotation = Quaternion.Euler(34.9f, -54.8f, -121f);
                objetEnMain.transform.localRotation = rotation;
                objetEnMain.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    IEnumerator LancerObjetAprèsAnimation()
    {
        yield return new WaitForSeconds(1f);
        LancerObjet(); // Lancez l'objet après le délai
    }

    void LancerObjet()
    {
        objetEnMain.transform.SetParent(null);
        Rigidbody rb = objetEnMain.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(Camera.main.transform.forward * forceLancer, ForceMode.Impulse);
        objetEnMain = null;
        animator.SetBool("Telecommande", false);
    }

    public void OnPickup(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed) {
            if (!isPickedUp)
            {
                Pickup();
            }
            else
            {
                Drop();
            }

            if (isPickedUp)
            {
                MovePickedUpObject();
            }
        }
    }
    
    void Pickup()
    {
        animator.SetBool("TelecommandePush", false);
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            Transform objectToPickup = hit.collider.transform;
            if (objectToPickup.CompareTag("Ramassable"))
            {
                pickedUpObject = objectToPickup;
                isPickedUp = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
    
    void MovePickedUpObject()
    {
        pickedUpObject.position = Vector3.Lerp(pickedUpObject.position, playerCamera.transform.position + playerCamera.transform.forward * pickupRange, pickupSpeed * Time.deltaTime);
    }
    
    void Drop()
    {
        pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
        isPickedUp = false;
        pickedUpObject = null;
    }

    public void OnMove(InputAction.CallbackContext ctxt) {
        inputVector = ctxt.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext ctxt) {
        if(ctxt.performed) {
            PlayerSpeed = PlayerSpeed * 2;
            animator.SetBool("isSprint", true);
        }
        if(ctxt.canceled) {
            PlayerSpeed = 5;
            animator.SetBool("isSprint", false);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if(characterController.isGrounded){
            // Saut
            if (Input.GetButtonDown("Jump")) // Ne permet pas de sauter quand accroupi
            {
                PlayerVelocity.y = jumpForce; // Applique la force de saut
                isJumping = true; // Le joueur est en train de sauter
                animator.SetBool("isJumping", true);
            }
        } else {
            // Le joueur n'est pas au sol, il ne peut pas être en train de sauter
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        // affichage de l'ui de ramassage
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Ramassable") || hit.collider.CompareTag("RedKeyCrystal") || hit.collider.CompareTag("GreenKeyCrystal") || hit.collider.CompareTag("BlueKeyCrystal") || hit.collider.CompareTag("Telecommande") || hit.collider.CompareTag("Radio"))
            {
                if (!isPickedUp && objetEnMain==null)
                {
                    pickupUI.SetActive(true);
                }
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

        if(isPickedUp)
        {
            MovePickedUpObject();
        }

        if (Input.GetMouseButtonDown(1) && objetEnMain != null)
        {   
            animator.SetBool("isThrowing", true);
            StartCoroutine(LancerObjetAprèsAnimation());
        }
        else
        {
            animator.SetBool("isThrowing", false);
        }

        if (Input.GetMouseButtonDown(0) && objetEnMain != null && objetEnMain.CompareTag("Telecommande"))
        {
            animator.SetBool("TelecommandePush", true);
            StartCoroutine(AnimateButton());

            // Si l'objet ramassé est la télécommande et que le bouton gauche de la souris est cliqué, active les animations et le passage du portail
            ClickableObject clickableObject = objetEnMain.GetComponent<ClickableObject>();
            if (clickableObject != null)
            {
                clickableObject.OnMouseDown();
            } 
        }
        else 
        {
            animator.SetBool("TelecommandePush", false);
        }

        // Appliquer la gravité
        PlayerVelocity.y -= Gravity * Time.deltaTime;
    
        Vector3 NewMoveDir = new Vector3(inputVector.x, 0, inputVector.y); // Ajoute PlayerVelocity.y à MoveDir
        NewMoveDir = Camera.main.transform.TransformDirection(NewMoveDir);
        NewMoveDir.y = 0;
        NewMoveDir.Normalize();
        MoveDir.x = NewMoveDir.x;
        MoveDir.z = NewMoveDir.z;
    
        animator.SetBool("isWalking", inputVector.y > 0);
        animator.SetBool("Backward", inputVector.y < 0);
        animator.SetBool("StrafeLeft", inputVector.x < 0);
        animator.SetBool("StrafeRight", inputVector.x > 0);
    
        characterController.Move(MoveDir * PlayerSpeed*Time.deltaTime);
    
        // Déplacer le joueur en fonction de la gravité et de la force de saut
        characterController.Move(PlayerVelocity * Time.deltaTime);
    }
}