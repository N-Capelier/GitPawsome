using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Created by Rémi
/// Last modified by Rémi
/// </summary>
public class TossCoinUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Image coinDisplay;
    [SerializeField]
    Button continueButton;
    [SerializeField]
    Sprite[] tossCoinAnimationSprites;
    [SerializeField]
    Sprite playerStartSprite;
    [SerializeField]
    Sprite enemyStartSprite;
    [SerializeField]
    float animDuration;

    BattleInitUIMode mode;

    public Action ContinueToPlacement;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(BattleInitUIMode _mode, float _animationDuration)
    {
        mode = _mode;
        mode.fsm.CoinTossed += OnTossEnd;
        StartCoroutine(TossCoinAnimation(_animationDuration));
        continueButton.gameObject.SetActive(false);
    }

    public void OnUnMount()
    {
        mode.fsm.CoinTossed -= OnTossEnd;
    }

    public void OnTossEnd(bool playerPlayingFirst)
    {
        title.text = playerPlayingFirst ?
            "You play first !" :
            "You play second !";

        coinDisplay.sprite = playerPlayingFirst ?
            playerStartSprite :
            enemyStartSprite;

        StopAllCoroutines();
        continueButton.gameObject.SetActive(true);
    }

    public void OnConfirm()
    {
        ContinueToPlacement?.Invoke();
    }

    IEnumerator TossCoinAnimation(float animationDuration)
    {
        for (int i = 0; i < tossCoinAnimationSprites.Length; i++)
        {
            coinDisplay.sprite = tossCoinAnimationSprites[i];
            yield return new WaitForSeconds(animationDuration / tossCoinAnimationSprites.Length);
        }

        //StartCoroutine(TossCoinAnimation(animationDuration));
    }
}
