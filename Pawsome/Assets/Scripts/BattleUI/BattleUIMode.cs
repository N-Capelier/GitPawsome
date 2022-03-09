using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleUIMode : MonoBehaviour
{
    public BattleStateMachine fsm;

	public TimelineUI timelineUI;
    public CompositionUI compositionUI;
    public EntityDetailsUI detailsUI;
    public ActionHistoryUI historyUI;
    public CardHandUI cardHandUI;

    [Header("Ressources")]
    public GraphicReferences graphicRessources;

    static public BorderColors PlayerColors;

    public void Init()
	{
        PlayerColors = graphicRessources.entityColorCode;
		timelineUI.OnMount(fsm.entities, this);
        compositionUI.OnMount(fsm.playerInfo, fsm.enemyInfo, this);
        detailsUI.OnMout(this);
        historyUI.OnMount(this);
	}

    static public Color GetBorderColor(Entity entity, bool excludePlaying = false)
    {
        Color _color = new Color();

        if (entity.isPlayerEntity)
        {
            if (entity.isPlaying && !excludePlaying)
                _color = PlayerColors.currentAllyColor;
            else
                _color = PlayerColors.allyColor;
        }
        else
        {
            if (entity.isPlaying && !excludePlaying)
                _color = PlayerColors.currentEnemyColor;
            else
                _color = PlayerColors.enemyColor;
        }

        return _color;
    }

    public Entity GetCurrentPlaying()
    {
        var playingEntity = fsm.entities[fsm.turnIndex];
        return playingEntity;
    }

    public Sprite GetClassIcon(InstaCat cat)
    {
        var sprite = graphicRessources.classIcons
            .Where(i => i.Key == cat.catClass)
            .Select(i => i.Value)
            .First();

        return sprite;
    }

    public Sprite GetStatIcon(AllStats stat)
    {
        var sprite = graphicRessources.statsIcon
            .Where(i => i.Key == stat)
            .Select(i => i.Value)
            .First();

        return sprite;
    }

    public Sprite GetCardBackground(Archetype _class)
    {
        var sprite = graphicRessources.cardBackground
            .Where(i => i.Key == _class)
            .Select(i => i.Value)
            .First();

        return sprite;
    }


    //TODO: implement logic 
    public void OpenMenu()
    {

    }

    public void OpenSettings()
    {

    }

    public void OpenChat()
    {

    }
}
