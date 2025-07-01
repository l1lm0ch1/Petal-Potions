using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public List<GameObject> TutorialPages = new List<GameObject>();
    public GameObject BackButon;
    public GameObject NextButton;
    public GameObject CreditsUI;

    private int currentIndex = 0;

    public void Start()
    {
        UpdateUI();
        NextButton.SetActive(true);
        BackButon.SetActive(false);
    }

    public void PressBack()
    {
        if (currentIndex == 0)
        {
            BackButon.SetActive(false);
            return;
        }
        else
        {
            currentIndex--;
            BackButon.SetActive(true);
            NextButton.SetActive(true);
        }

        UpdateUI();
    }

    public void PressForward()
    {
        if (currentIndex == (TutorialPages.Count - 1))
        {
            NextButton.SetActive(false);
            return;
        }
        else
        {
            currentIndex++;
            NextButton.SetActive(true);
            BackButon.SetActive(true);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < TutorialPages.Count; i++)
        {
            TutorialPages[i].SetActive(i == currentIndex);
        }
    }

    public void PlayCredits()
    {
        StartCoroutine(CreditsCoroutine());
    }

    IEnumerator CreditsCoroutine()
    {
        CreditsUI.SetActive(true);
        yield return new WaitForSeconds(20);
        CreditsUI.SetActive(false);
    }
}
