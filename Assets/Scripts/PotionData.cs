using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "FairyFarm/Potion")]
public class PotionData : ScriptableObject
{
    public string potionName;
    public Sprite icon;
    public int basePrice;

    [Header("3D Modell Referenz")]
    public GameObject prefab;
}
