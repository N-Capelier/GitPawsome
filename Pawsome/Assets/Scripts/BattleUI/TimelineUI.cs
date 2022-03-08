using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System;
using System.Linq;

public class TimelineUI : MonoBehaviour
{
    [Header("Global references")]
    [SerializeField]
    TimelinePortrait mainPortrait;
    [SerializeField]
    TimelinePortrait portraitTemplate;
    [SerializeField]
    Transform portraitCollectionRoot;

    [Header("Slide Elements")]
    [SerializeField]
    Button toRightBtton;
    [SerializeField]
    Button toLeftButton;
    [SerializeField]
    Scrollbar scroller;
    [SerializeField, Range(0f,1f)]
    float padding;

    [HideInInspector]
    public BattleUIMode mode;
    [SerializeField]
    List<TimelinePortrait> allPortraits;

    public Action TurnStarted;

    private void OnDestroy()
    {
        OnUnMount();
    }

    public void OnMount(List<Entity> orderedEntities, BattleUIMode _mode)
    {
        mode = _mode;
        allPortraits = new List<TimelinePortrait>();
        int count = 0;

        foreach (Entity entity in orderedEntities)
        {
            var por = Instantiate(portraitTemplate, portraitCollectionRoot);
            por.gameObject.name = "entity_portrait_" + count.ToString();
            por.gameObject.SetActive(true);
            allPortraits.Add(por);
            por.OnMount(entity);
            count++;
        }

        mainPortrait.OnMount(mode.fsm.entities[0]);

        UpdateTimelineControl();
        mode.fsm.EnterTurn += UpdateTimeline;
    }

    public void OnUnMount()
    {
        mode.fsm.EnterTurn -= UpdateTimeline;
    }

    void UpdateTimeline()
    {
        int lastIndex = mode.fsm.turnIndex - 1;
        if (lastIndex < 0) lastIndex = mode.fsm.entities.Count - 1;

        var lastEntity = mode.fsm.entities[lastIndex];
        var lastPortrait = allPortraits.Where(p => p.linkedEntity == lastEntity).First();

        lastPortrait.transform.SetAsLastSibling();

        mainPortrait.linkedEntity = mode.fsm.entities[mode.fsm.turnIndex];

        TurnStarted?.Invoke();
    }

    public void MoveTimeline(int steps)
    {
        scroller.value += steps * padding;
        UpdateTimelineControl();
    }

    public void UpdateTimelineControl()
    {
        if (scroller.value <= 0)
            toLeftButton.interactable = false;
        else
            toLeftButton.interactable = true;

        if (scroller.value >= 1)
            toRightBtton.interactable = false;
        else
            toRightBtton.interactable = true;
    }
}
