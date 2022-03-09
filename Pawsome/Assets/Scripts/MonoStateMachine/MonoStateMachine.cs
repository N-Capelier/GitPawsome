using System.Collections.Generic;
using UnityEngine;

public abstract class MonoStateMachine : MonoBehaviour
{
    [Header("State Machine Setup")]
    [SerializeField] bool dontDestroyOnLoad = false;
	[SerializeField] bool playOnAwake = true;

	bool isPlaying = false;
	public bool IsPlaying { get => isPlaying; }

    MonoState activeState = null;
    public MonoState ActiveState { get => activeState; }

	[Header("States")]
	[SerializeField] MonoState startingState;

	public List<MonoState> states = new List<MonoState>();
	public List<MonoState> States { get => states; }

	private void Awake()
	{
		if (dontDestroyOnLoad)
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void Play()
	{
		if (isPlaying)
			return;

		isPlaying = true;
		if (!activeState.playedStateEnter)
		{
			activeState.playedStateEnter = true;
			activeState.OnStateEnter();
		}

	}

	public void Pause()
	{
		isPlaying = false;
	}

	public bool SetState(string _stateName, bool _forceState = false)
	{
		if (states.Count == 0)
			throw new System.EmptyCollectionException();

		if (activeState != null)
		{
			if (_stateName == activeState.StateName && !_forceState)
				return false;
		}

		if (activeState != null)
		{
			ActiveState.OnStateExit();
			Destroy(activeState);
		}
		activeState = Instantiate(FindStateWithName(_stateName));
		activeState.SetStateMachine(this);
		if (isPlaying)
			activeState.OnStateEnter();

		return true;
	}

	MonoState FindStateWithName(string _stateName)
	{
		foreach(MonoState _state in states)
		{
			if (_stateName == _state.StateName)
				return _state;
		}
		throw new System.ArgumentException();
	}

	private void Start()
	{
		if(playOnAwake)
		{
			SetState(startingState.StateName);
			isPlaying = true;
		}

		if (isPlaying)
		{
			activeState.playedStateEnter = true;
			activeState.OnStateEnter();
		}
	}

	private void Update()
	{
		if (IsPlaying)
			activeState.OnStateUpdate();
	}

	private void FixedUpdate()
	{
		if (IsPlaying)
			activeState.OnStateFixedUpdate();
	}

	private void LateUpdate()
	{
		if (IsPlaying)
			activeState.OnStateLateUpdate();
	}
}

namespace System
{
    public class EmptyCollectionException : SystemException
	{
        public EmptyCollectionException() : base("Collection is empty.")
		{

		}

        public EmptyCollectionException(string message) : base(message)
		{

		}

        public EmptyCollectionException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}