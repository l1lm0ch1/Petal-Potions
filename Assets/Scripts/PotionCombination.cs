using UnityEngine;

[System.Serializable]
public class PotionCombination
{
    public string petal1;
    public string petal2;
    public GameObject potionPrefab;

    public string MatchKey => string.Compare(petal1, petal2) < 0
        ? petal1 + "+" + petal2
        : petal2 + "+" + petal1;
}
