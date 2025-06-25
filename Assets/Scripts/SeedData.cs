using UnityEngine;

[CreateAssetMenu(fileName = "NewSeedData", menuName = "FairyFarm/Seed")]
public class SeedData : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public FlowerData flowerData;
    public GameObject seedVisualPrefab;
    public Color bagColor;
    public string flowerName;
    public int Inventory;
}
