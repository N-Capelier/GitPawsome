using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class TimelinePortrait : MonoBehaviour
{
    [SerializeField]
    Image portrait;
    [SerializeField]
    Image border;
    [SerializeField]
    TimelineUI timeLineHandler;

    [Header("Display Logic")]
    [SerializeField]
    bool isMain;
    [SerializeField, ReadOnly]
    InstaCat linkedEntity;

    public void OnMount(InstaCat _linkedEntity)
    {
        //linkedEntity = _linkedEntity;
        //battleManager.TurnChanged += OnTurnChange
    }

    public void OnUnMount()
    {
        //battleManager.TurnChanged += OnTurnChange
    }

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnTurnChange()
    {
        //portrait.sprite = linkedEntity.portrait;
        //border.color = timeLineHandler.ReturnBorderColor(linkedEntity);

        if (!isMain /*&& linkedEnity == curentTurnEntity*/ )
        {
            portrait.rectTransform.localScale = Vector3.one * 1.3f;
            border.rectTransform.localScale = Vector3.one * 1.3f;
        }
        else
        {
            portrait.rectTransform.localScale = Vector3.one * 1;
            border.rectTransform.localScale = Vector3.one * 1;
        }
    }
}
