using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class TimelinePortrait : MonoBehaviour
{
    [SerializeField]
    Image portraitMask;
    [SerializeField]
    Image portrait;
    [SerializeField]
    Image border;
    [SerializeField]
    TimelineUI timelineHandler;

    [Header("Display Logic")]
    [SerializeField]
    bool isMain;
    [ReadOnly]
    public Entity linkedEntity;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(Entity _linkedEntity)
    {
        linkedEntity = _linkedEntity;
        UpdatePortrait();
        timelineHandler.TurnStarted += UpdatePortrait;
    }

    public void OnUnMount()
    {
        timelineHandler.TurnStarted -= UpdatePortrait;
    }

    public void UpdatePortrait()
    {
        portrait.sprite = linkedEntity.InstaCatRef.CatSprite;
        border.color = BattleUIMode.GetBorderColor(linkedEntity);

        if (!isMain && linkedEntity.isPlaying)
        {
            portraitMask.rectTransform.localScale = Vector3.one * 1.3f;
            border.rectTransform.localScale = Vector3.one * 1.3f;
        }
        else
        {
            portraitMask.rectTransform.localScale = Vector3.one * 1;
            border.rectTransform.localScale = Vector3.one * 1;
        }
    }
}
