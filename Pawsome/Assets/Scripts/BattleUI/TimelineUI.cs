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
        Color AllyColor;
        [SerializeField]
        Color EnemyColor;
        [SerializeField]
        Color CurrentAlly;
        [SerializeField]
        Color CurrentEnemy;
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

    List<TimelinePortrait> allPortraits;

    public void Start()
    {
        UpdateTimeLineControl();
    }

    public void OnMount(List<InstaCat> orderedEntities)
    {
        allPortraits = new List<TimelinePortrait>();

        foreach (InstaCat entity in orderedEntities)
        {
            var por = Instantiate(portraitTemplate, portraitCollectionRoot);
            allPortraits.Add(por);
            por.OnMount(entity);
        }

        UpdateTimeLineControl();

        //BattleManager.OnCollectionChanged += UpDateTimeLine;
    }

    public void OnUnMount()
    {
        //BattleManager.OnCollectionChanged -= UpDateTimeLine;
    }

    void UpdateTimeline(List<InstaCat> orderedEntities)
    {
        var firstPor = allPortraits.First();

        firstPor.OnUnMount();
        allPortraits.Remove(firstPor);

        firstPor.OnMount(orderedEntities.Last());
        firstPor.transform.SetAsLastSibling();
        allPortraits.Add(firstPor);
    }

    public void MoveTimeline(int steps)
    {
        scroller.value += steps * padding;
        UpdateTimeLineControl();
    }

    public void UpdateTimeLineControl()
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

    public Color ReturnBorderColor(InstaCat entity)
    {
        Color _color = new Color();

        //Logic to test if the entity is either one of the following type:
        // Ally, Enemy, CurrentAlly, CurrentEnemy

        return _color;
    }

}
