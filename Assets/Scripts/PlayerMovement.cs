using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f; // Vitesse de déplacement normale
    private float originalSpeed; // Vitesse de déplacement normale sauvegardée
    public float jumpForce = 8.0f; // Force de saut
    private bool isJumping = false; // Indique si le joueur est en train de sauter

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    // Variables de transition pour la caméra
    private Vector3 cameraTargetPosition;
    private Vector3 cameraVelocity = Vector3.zero;
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        originalSpeed = speed; // Sauvegarde de la vitesse de déplacement normale

        // Position initiale de la caméra
        cameraTargetPosition = transform.GetChild(0).localPosition;
    }

    void Update()
    {   
        // Vérifier si le joueur est au sol
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Déplacements horizontaux (gauche/droite et avant/arrière)
            Vector3 move = transform.right * horizontal + transform.forward * vertical;

            // Appliquer la vitesse de déplacement
            moveDirection = move * speed;
            

            // Sprint
            if (Input.GetKey(KeyCode.LeftShift)) // Ne permet pas de sprinter quand accroupi
            {
                speed = originalSpeed * 2;
            }
            else if (!isJumping) // Vérifie que le joueur n'est pas en train de sauter
            {
                speed = originalSpeed; // Rétablit la vitesse de déplacement normale
            }

            // Saut
            if (Input.GetButtonDown("Jump")) // Ne permet pas de sauter quand accroupi
            {
                moveDirection.y = jumpForce; // Applique la force de saut
                isJumping = true; // Le joueur est en train de sauter
            }
        }
        else
        {
            // Le joueur n'est pas au sol, il ne peut pas être en train de sauter
            isJumping = false;

            // Gestion de la direction en l'air
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Déplacements horizontaux (gauche/droite et avant/arrière)
            Vector3 move = transform.right * horizontal + transform.forward * vertical;

            // Appliquer la vitesse de déplacement même en l'air
            moveDirection.x = move.x * speed;
            moveDirection.z = move.z * speed;
        }

        // Appliquer la gravité
        moveDirection.y -= 9.81f * Time.deltaTime;

        // Déplacer le joueur
        controller.Move(moveDirection * Time.deltaTime);

        //ANIMATIONS
        // Activer la condition "isWalking" dans l'Animator lorsque le joueur marche en avant
        animator.SetBool("isWalking", Input.GetAxis("Vertical") > 0);

        // Désactiver la condition "isWalking" dans l'Animator lorsque le joueur ne marche pas en avant
        if (Input.GetAxis("Vertical") <= 0)
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("Backward", Input.GetAxis("Vertical") < 0); // Activer la condition "Backward" dans l'Animator lorsque le joueur marche en arrière

        // Désactiver la condition "Backward" dans l'Animator lorsque le joueur ne marche pas en arrière
        if (Input.GetAxis("Vertical") >= 0)
        {
            animator.SetBool("Backward", false);
        }

        // Activer la condition "StrafeLeft" dans l'Animator lorsque le joueur se déplace vers la gauche
        animator.SetBool("StrafeLeft", Input.GetAxis("Horizontal") < 0);

        // Désactiver la condition "StrafeLeft" dans l'Animator lorsque le joueur ne se déplace pas vers la gauche
        if (Input.GetAxis("Horizontal") >= 0)
        {
            animator.SetBool("StrafeLeft", false);
        }

        // Activer la condition "StrafeRight" dans l'Animator lorsque le joueur se déplace vers la droite
        animator.SetBool("StrafeRight", Input.GetAxis("Horizontal") > 0);

        // Désactiver la condition "StrafeRight" dans l'Animator lorsque le joueur ne se déplace pas vers la droite
        if (Input.GetAxis("Horizontal") <= 0)
        {
            animator.SetBool("StrafeRight", false);
        }

        // Activer la condition "isJumping" dans l'Animator lorsque le joueur saute
        animator.SetBool("isJumping", isJumping);

        // Désactiver la condition "isJumping" dans l'Animator lorsque le joueur ne saute pas
        if (!isJumping)
        {
            animator.SetBool("isJumping", false);
        }

        // Activer la condition "isSprint" dans l'Animator lorsque le joueur sprinte
        animator.SetBool("isSprint", Input.GetKey(KeyCode.LeftShift));

        // Désactiver la condition "isSprint" dans l'Animator lorsque le joueur ne sprinte pas
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isSprint", false);
        }

    }
}