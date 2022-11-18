using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoSingleton<ScoreUI>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI notEnoughText;
    [SerializeField] private int coinsOnLevelCount;
    private int coinsCollected = 0;
    private void Start()
    {
        UpdateText();
        notEnoughText.gameObject.SetActive(false);
    }

    public void OnCoinPickUp()
    {
        coinsCollected++;
        UpdateText();
    }

    public bool CanGoToPortal()
    {
        return coinsCollected >= coinsOnLevelCount;
    }

    private void UpdateText()
    {
        scoreText.text = coinsCollected + "/" + coinsOnLevelCount;
    }

    public void ShowNotEnough()
    {
        StartCoroutine(NotEnoughCoinsRoutine());
    }

    private IEnumerator NotEnoughCoinsRoutine()
    {
        notEnoughText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        notEnoughText.gameObject.SetActive(false);
    }
}
