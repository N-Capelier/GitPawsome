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
        //TODO: start animation
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

        //TODO: stop animation;
        continueButton.gameObject.SetActive(true);
    }

    public void OnConfirm()
    {
        ContinueToPlacement?.Invoke();
    }
}
