using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIMode : MonoBehaviour
{
    [Serializable]
    public struct BorderColors
    {
        [SerializeField]
        public Color allyColor;
        [SerializeField]
        public Color enemyColor;
        [SerializeField]
        public Color currentAllyColor;
        [SerializeField]
        public Color currentEnemyColor;
    }

    public BattleStateMachine fsm;

	public TimelineUI timelineUI;
    public CompositionUI compositionUI;

    static public BorderColors PlayerColors;
    public BorderColors playerColors;

    public void Init()
	{
        PlayerColors = playerColors;
		timelineUI.OnMount(fsm.entities, this);
        compositionUI.OnMount(fsm.playerInfo, fsm.enemyInfo, this);
	}

    static public Color GetBorderColor(Entity entity)
    {
        Color _color = new Color();

        if (entity.isPlayerEntity)
        {
            if (entity.isPlaying)
                _color = PlayerColors.currentAllyColor;
            else
                _color = PlayerColors.allyColor;
        }
        else
        {
            if (entity.isPlaying)
                _color = PlayerColors.currentEnemyColor;
            else
                _color = PlayerColors.enemyColor;
        }

        return _color;
    }
}
