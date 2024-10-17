using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    public Light discoLight; // Référence à la lumière
    public float changeInterval = 0.5f; // Intervalle de changement de couleur en secondes

    private Color[] colors = new Color[]
    {
        new Color(0x57 / 255f, 0x15 / 255f, 0x83 / 255f), // #571583
        new Color(0xFE / 255f, 0xF1 / 255f, 0x00 / 255f), // #FEF100
        new Color(0xFF / 255f, 0x4E / 255f, 0xA9 / 255f), // #FF4EA9
        new Color(0x02 / 255f, 0xFF / 255f, 0x01 / 255f), // #02FF01
        new Color(0xF5 / 255f, 0x09 / 255f, 0x12 / 255f), // #F50912
        new Color(0x20 / 255f, 0x7B / 255f, 0xEF / 255f)  // #207BEF
    };
    private int currentColorIndex = 0;

    // Start is called before the first frame update
    public void Start()
    {
        // Assurez-vous que la lumière est assignée
        if (discoLight == null)
        {
            Debug.LogWarning("Light not assigned.");
            return;
        }

        // Commencez à changer la couleur de la lumière
        InvokeRepeating("ChangeColor", changeInterval, changeInterval);
    }

    // Change la couleur de la lumière de manière aléatoire
    public void ChangeColor()
    {
        discoLight.color = colors[currentColorIndex];
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
    }
}