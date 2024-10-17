using UnityEngine;
using UnityEngine.UI;

public class ProximityBar : MonoBehaviour
{
    [SerializeField] Slider proximitySlider; 
    [SerializeField] Transform user; 
    [SerializeField] Transform remote; 

    [SerializeField] float maxDistance = 10f;

    void Update()
    {
        bool showProximityBar = PlayerPrefs.GetInt("ShowProximity", 1) == 1;

        if (showProximityBar)
        {
            float distance = Vector3.Distance(user.position, remote.position);

            proximitySlider.value = 1 - (distance / maxDistance);

            proximitySlider.gameObject.SetActive(true);
        }
        else
        {
            proximitySlider.gameObject.SetActive(false);
        }
    }
}