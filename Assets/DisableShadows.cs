using UnityEngine;

public class PerformanceOptimizer : MonoBehaviour
{
    // Variables que vous pouvez ajuster selon la scène
    public float shadowDistanceNear = 50f;  // Distance des ombres quand la caméra est proche
    public float shadowDistanceFar = 200f;  // Distance des ombres quand la caméra est loin
    public float performanceCheckInterval = 2f;  // Temps entre chaque vérification de la performance
    public float targetFrameRate = 60f;  // Framerate cible à maintenir
    public Light mainLight;  // La lumière principale de la scène

    private float originalShadowDistance;
    private float timeSinceLastCheck = 0f;

    void Start()
    {
        // Sauvegarder la distance d'ombre par défaut
        originalShadowDistance = QualitySettings.shadowDistance;
    }

    void Update()
    {
        // Met à jour les paramètres d'ombre en fonction du framerate
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= performanceCheckInterval)
        {
            CheckPerformance();
            timeSinceLastCheck = 0f;
        }
    }

    void CheckPerformance()
    {
        float currentFPS = 1f / Time.deltaTime;

        if (currentFPS < targetFrameRate)
        {
            // Si le FPS est trop bas, réduire la distance des ombres pour optimiser
            QualitySettings.shadowDistance = Mathf.Lerp(QualitySettings.shadowDistance, shadowDistanceNear, Time.deltaTime);
        }
        else
        {
            // Si le FPS est bon, augmenter doucement la distance des ombres jusqu'à la valeur originale
            QualitySettings.shadowDistance = Mathf.Lerp(QualitySettings.shadowDistance, shadowDistanceFar, Time.deltaTime);
        }
    }

    public void DisableShadowsForDistantObjects(Transform player, float disableDistance)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (Vector3.Distance(player.position, obj.transform.position) > disableDistance)
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off)
                {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }
        }
    }

    public void OptimizeLightSettings()
    {
        if (mainLight != null)
        {
            // Désactiver les ombres douces pour améliorer les performances
            mainLight.shadows = LightShadows.Hard;

            // Ajuster l'intensité de la lumière pour équilibrer les ombres
            mainLight.intensity = 0.85f;

            // Désactiver les ombres sur les lumières non essentielles
            foreach (Light light in FindObjectsOfType<Light>())
            {
                if (light != mainLight)
                {
                    light.shadows = LightShadows.None;
                }
            }
        }
    }
}
