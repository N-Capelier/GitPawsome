using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System;
using System.Linq;

public class TimelineUI : MonoBehaviour
{
    [Serializable]
    internal struct BorderColors
    {
        [SerializeField]
        internal Color allyColor;
        [SerializeField]
        internal Color enemyColor;
        [SerializeField]
        internal Color currentAllyColor;
        [SerializeField]
        internal Color currentEnemyColor;
    }

    [Header("Global references")]
    [SerializeField]
    BorderColors borderColors;
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

        UpdateTimelineControl();
        mode.fsm.EnterTurn += UpdateTimeline;
    }

    public void OnUnMount()
    {
        mode.fsm.EnterTurn -= UpdateTimeline;
    }

    void UpdateTimeline()
    {
        var firstPor = allPortraits.First();

        allPortraits.RemoveAt(0);
        allPortraits.Insert(allPortraits.Count - 1, firstPor);
        firstPor.transform.SetAsLastSibling();

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

    public Color ReturnBorderColor(Entity entity)
    {
        Color _color = new Color();

        if (entity.isPlayerEntity)
        {
            if (mode.IsEntityTurn(entity))
                _color = borderColors.currentAllyColor;
            else
                _color = borderColors.allyColor;
        }
        else
        {
            if (mode.IsEntityTurn(entity))
                _color = borderColors.currentEnemyColor;
            else
                _color = borderColors.enemyColor;
        }

        return _color;
    }

}
