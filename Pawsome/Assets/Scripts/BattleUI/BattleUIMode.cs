using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIMode : MonoBehaviour
{
	public BattleStateMachine fsm;

	public TimelineUI timeLineUI;

    private void Start()
    {
        Init();
    }

    public void Init()
	{
		timeLineUI.OnMount(fsm.entities, this);
	}

    public bool IsEntityTurn(Entity _entity)
    {
        if (fsm.turnIndex >= 0)
            return _entity == fsm.entities[fsm.turnIndex];
        else return false;
    }
}
