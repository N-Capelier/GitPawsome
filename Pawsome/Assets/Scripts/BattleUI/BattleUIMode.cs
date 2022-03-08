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

    public static BorderColors borderColors;

    public void Init()
	{
		timelineUI.OnMount(fsm.entities, this);
        compositionUI.OnMount(new PlayerEntity(), new PlayerEntity(), this);
	}

    static public Color GetBorderColor(Entity entity)
    {
        var borderColors = BattleUIMode.borderColors;
        Color _color = new Color();

        if (entity.isPlayerEntity)
        {
            if (entity.isPlaying)
                _color = borderColors.currentAllyColor;
            else
                _color = borderColors.allyColor;
        }
        else
        {
            if (entity.isPlaying)
                _color = borderColors.currentEnemyColor;
            else
                _color = borderColors.enemyColor;
        }

        return _color;
    }
}
