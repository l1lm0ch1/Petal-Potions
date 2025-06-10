using UnityEngine;

[CreateAssetMenu(fileName = "NewFlowerData", menuName = "Flower/FlowerData")]
public class FlowerData : ScriptableObject
{
    public string flowerName;
    public float growthDuration; // in Sekunden
    public Sprite icon;
    public int basePrice;

    [Header("3D Modell Referenz")]
    public GameObject flowerPrefab;
}
