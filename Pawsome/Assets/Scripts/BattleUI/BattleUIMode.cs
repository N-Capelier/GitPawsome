using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIMode : MonoBehaviour
{
	public TimelineUI timeLineUI;
	public BattleStateMachine fsm;

	public void Init()
	{
		timeLineUI.OnMount(fsm.entities);
	}
}
