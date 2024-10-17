using UnityEngine;
using System.Collections;

public class RamasserMain : MonoBehaviour
{
    public Transform mainDuPersonnage;
    public float forceLancer = 10f;
    public float distanceMaxRamassage = 3f; // Distance maximale de ramassage

    private GameObject objetEnMain;

    private Animator animator;

    public GameObject telecomande_bouton; // Référence à l'objet "telecomande_bouton"

    public GameObject telecomande_Led; // Référence à l'objet "telecommande"

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        yield return new WaitForSeconds(0.95f);
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
}
