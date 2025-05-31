using UnityEngine;
using UnityEngine.UI;

public class FlowerGrowth : MonoBehaviour
{
    [Header("Blumen-Daten")]
    public FlowerData flowerData; // HIER wird die Blumen-Art angegeben!

    [Header("Wachstum")]
    public float growthSpeed = 0.5f; // Wie schnell wächst die Blume (kann randomisiert werden)
    private float currentGrowth = 0f; // 0 = klein, 100 = voll ausgewachsen

    [Header("Blendshape")]
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int growthBlendshapeIndex = 0;

    [Header("UI")]
    public Canvas worldCanvas;
    public Slider growthSlider;

    private bool isFullyGrown = false;

    public bool getFlowerGrowth()
    {
        return isFullyGrown;
    }

    void Start()
    {
        if (growthSlider != null)
            growthSlider.value = 0f;

        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.SetBlendShapeWeight(growthBlendshapeIndex, 0f);
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

            // Blendshape updaten
            if (skinnedMeshRenderer != null)
                skinnedMeshRenderer.SetBlendShapeWeight(growthBlendshapeIndex, currentGrowth);

            // UI updaten
            if (growthSlider != null)
                growthSlider.value = currentGrowth / 100f;
        }
    }

    public void Harvest()
    {
        if (isFullyGrown)
        {
            Debug.Log("Blume gepflückt!");

            // Prüfen, ob flowerData gesetzt ist
            if (flowerData != null)
            {
                FlowerInventory.Instance.AddPetals(flowerData, 1);
            }
            else
            {
                Debug.LogWarning("Kein FlowerData verknüpft!");
            }

            Destroy(gameObject); // Blume verschwindet
        }
    }
}
