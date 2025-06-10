using UnityEngine;

[CreateAssetMenu(menuName = "FairyFarm/Petal")]
public class PetalData : ScriptableObject
{
    public string petalName;
    public Sprite icon;
    public int basePrice; // z. B. Handelswert oder Punktzahl
    public GameObject petalPrefab;
    public string petalID;

    // evtl. spätere Effekte für Tränke
}

