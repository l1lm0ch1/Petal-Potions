using System.Collections.Generic;
using UnityEngine;

public class PotionCombinationUI : MonoBehaviour
{
    public List<GameObject> PotionCombinations = new List<GameObject>();

    private int currentIndex = 0;

    public void Start()
    {
        UpdateUI();
    }

    public void PressBack()
    {
        if (PotionCombinations.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = PotionCombinations.Count - 1;

        UpdateUI();
    }

    public void PressForward()
    {
        if (PotionCombinations.Count == 0) return;

        currentIndex++;
        if (currentIndex >= PotionCombinations.Count)
            currentIndex = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        for(int i = 0; i < PotionCombinations.Count; i++)
        {
            PotionCombinations[i].SetActive(i == currentIndex);
        }
    }
}
