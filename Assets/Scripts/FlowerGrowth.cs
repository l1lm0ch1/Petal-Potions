using UnityEngine;
using UnityEngine.UI;

public class FlowerGrowth : MonoBehaviour
{
    [Header("Blumen-Daten")]
    public FlowerData flowerData; // Gibt die Blumen-Art an

    [Header("Wachstum")]
    public float growthSpeed = 0.5f; // Wie schnell wächst die Blume
    private float currentGrowth = 0f; // 0 = klein, 100 = voll ausgewachsen

    [Header("UI")]
    public Canvas worldCanvas;
    public Slider growthSlider;

    private bool isFullyGrown = false;
    private Vector3 initialScale;
    public Vector3 targetScale = Vector3.one;

    public bool getFlowerGrowth()
    {
        return isFullyGrown;
    }

    void Start()
    {
        if (growthSlider != null)
            growthSlider.value = 0f;

        initialScale = Vector3.zero;
        transform.localScale = initialScale;
    }

    void Update()
    {
        if (!isFullyGrown)
        {
            currentGrowth += growthSpeed * Time.deltaTime;
            if (currentGrowth >= 100f)
            {
                currentGrowth = 100f;
                isFullyGrown = true;
                Debug.Log("Blume ausgewachsen und bereit zum Pflücken!");
            }

            // Skalierung anwenden
            float t = currentGrowth / 100f;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // UI updaten
            if (growthSlider != null)
                growthSlider.value = t;
        }
    }

    public void Harvest()
    {
        if (isFullyGrown)
        {
            Debug.Log("Blume gepflückt!");

            if (flowerData != null)
            {
                FlowerInventory.Instance.AddPetals(flowerData, 1);
            }
            else
            {
                Debug.LogWarning("Kein FlowerData verknüpft!");
            }

            Destroy(gameObject);
        }
    }
}
