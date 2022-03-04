using UnityEngine;

public abstract class MonoState : ScriptableObject
{
    MonoStateMachine stateMachine;
    public MonoStateMachine StateMachine { get => stateMachine; }

    [SerializeField] string stateName = null;
    public string StateName { get => stateName;}

    [HideInInspector] public bool playedStateEnter = false;

    public virtual void OnStateEnter() { }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateLateUpdate() { }
    public virtual void OnStateFixedUpdate() { }
    public virtual void OnStateExit() { }

    public void SetStateMachine(MonoStateMachine _stateMachine)
	{
        stateMachine = _stateMachine;
	}

#if UNITY_EDITOR
    public void SetName(string _name)
    {
        stateName = _name;
    }
#endif
}