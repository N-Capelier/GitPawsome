using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    float animDuration;

    BattleInitUIMode mode;

    public Action ContinueToPlacement;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(BattleInitUIMode _mode)
    {
        mode = _mode;
        //TODO: subscribe OnTossEnd to correct Event
        StartCoroutine(TossCoinAnimation(animDuration));
        continueButton.gameObject.SetActive(false);
    }

    public void OnUnMount()
    {
        //TODO: unsubscribe OnTossEnd to correct Event
    }

    public void OnTossEnd(bool playerPlayingFirst)
    {
        title.text = playerPlayingFirst ?
            "You play first !" :
            "You play second !";

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

        StartCoroutine(TossCoinAnimation(animationDuration));
    }
}
