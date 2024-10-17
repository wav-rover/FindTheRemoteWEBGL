using UnityEngine;

public class RadioSound : MonoBehaviour
{
    public AudioSource audioSource; // Référence à l'AudioSource
    public GameObject[] discoObjects; // Tableau des objets à activer
    public DiscoLight discoLight; // Référence au script DiscoLight
    public Light specialLight; // Référence à la lumière spéciale à éteindre
    public CameraShake cameraShake; // Référence au script CameraShake
    private bool isPlaying = false; // Variable pour suivre si l'audio est en cours de lecture

    // Start is called before the first frame update
    void Start()
    {
        // Assurez-vous que l'AudioSource est désactivé au début
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned.");
        }

        // Assurez-vous que les objets à activer sont désactivés au début
        foreach (GameObject discoObject in discoObjects)
        {
            discoObject.SetActive(false);
        }

        // Assurez-vous que le changement de couleur est désactivé au début
        if (discoLight != null)
        {
            discoLight.enabled = false;
        }
        else
        {
            Debug.LogWarning("DiscoLight not assigned.");
        }

        // Assurez-vous que la lumière spéciale est allumée au début
        if (specialLight != null)
        {
            specialLight.enabled = true;
        }
        else
        {
            Debug.LogWarning("SpecialLight not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si la touche E est enfoncée
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Créez un raycast à partir de la caméra
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si le raycast touche quelque chose
            if (Physics.Raycast(ray, out hit))
            {
                // Si l'objet touché est cette radio
                if (hit.transform == transform)
                {
                    // Si l'audio est en cours de lecture, arrêtez-le et désactivez les objets
                    if (isPlaying)
                    {
                        if (audioSource != null)
                        {
                            audioSource.Stop();
                        }
                        isPlaying = false;

                        // Désactivez tous les objets
                        foreach (GameObject discoObject in discoObjects)
                        {
                            discoObject.SetActive(false);
                        }

                        // Désactivez le changement de couleur
                        if (discoLight != null)
                        {
                            discoLight.enabled = false;
                            Debug.Log("DiscoLight disabled");
                        }

                        // Désactivez toutes les lumières enfants
                        foreach (Transform child in transform)
                        {
                            Light childLight = child.GetComponent<Light>();
                            if (childLight != null)
                            {
                                childLight.enabled = false;
                                DiscoLight childDiscoLight = child.GetComponent<DiscoLight>();
                                if (childDiscoLight != null)
                                {
                                    childDiscoLight.enabled = false;
                                }
                            }
                        }

                        // Allumez la lumière spéciale
                        if (specialLight != null)
                        {
                            specialLight.enabled = true;
                        }

                        // Arrêtez le shake quand la radio est éteinte
                        if (cameraShake != null)
                        {
                            cameraShake.StopShake();
                            Debug.Log("Camera shake stopped");
                        }
                    }
                    // Sinon, jouez l'audio et activez les objets
                    else
                    {
                        if (audioSource != null)
                        {
                            audioSource.Play();
                        }
                        isPlaying = true;

                        // Activez tous les objets
                        foreach (GameObject discoObject in discoObjects)
                        {
                            discoObject.SetActive(true);
                        }

                        // Activez le changement de couleur
                        if (discoLight != null)
                        {
                            discoLight.enabled = true;
                            Debug.Log("DiscoLight enabled");
                        }

                        // Activez toutes les lumières enfants
                        foreach (Transform child in transform)
                        {
                            Light childLight = child.GetComponent<Light>();
                            if (childLight != null)
                            {
                                childLight.enabled = true;
                                DiscoLight childDiscoLight = child.GetComponent<DiscoLight>();
                                if (childDiscoLight == null)
                                {
                                    childDiscoLight = child.gameObject.AddComponent<DiscoLight>();
                                }
                                childDiscoLight.enabled = true;
                            }
                        }

                        // Éteignez la lumière spéciale
                        if (specialLight != null)
                        {
                            specialLight.enabled = false;
                        }

                        // Commencez le shake quand la radio est allumée
                        if (cameraShake != null)
                        {
                            cameraShake.StartShake();
                            Debug.Log("Camera shake started");
                        }
                    }
                }
            }
        }
    }
}