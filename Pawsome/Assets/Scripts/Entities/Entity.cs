using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] float moveDuration = 10f;

	[Header("References")]
	[SerializeField] MeshRenderer objectRenderer;

	[HideInInspector] public bool isPlayerEntity = false;
	[HideInInspector] public bool isPlaying = false;

	InstaCat instaCat;
	public InstaCat InstaCat { get => instaCat; private set => instaCat = value; }

	InstaCat instaCatRef;
	public InstaCat InstaCatRef { get => instaCatRef; private set => instaCatRef = value; }


	[HideInInspector] public List<Spell> spells = new List<Spell>();

	[HideInInspector] public List<Spell> hand = new List<Spell>();

	[HideInInspector] public List<Spell> discardPile = new List<Spell>();

	Coroutine moveAlongPathCoroutine = null;

	public void Init(InstaCat _instaCat)
	{
		instaCat = Instantiate(_instaCat);
		instaCatRef = _instaCat;

		instaCat.health = instaCat.GetHealth();
		instaCat.mana = instaCat.GetMana();
		instaCat.initiative = instaCat.GetInitiative();
		instaCat.attack = instaCat.GetAttack();
		instaCat.defense = instaCat.GetDefense();
		instaCat.movePoints = instaCat.GetMovePoints();

		foreach(Spell _spell in instaCat.spells)
		{
			spells.Add(Instantiate(_spell));
		}
		InstaCat.spells = new List<Spell>();
		spells.Shuffle();

		//create hand
		for (int i = 0; i < 3; i++)
		{
			hand.Add(spells[0]);
			spells.RemoveAt(0);
		}

		//set renderer
	}

	public void MoveAlongPath(Vector2Int[] _path)
	{
		LevelGrid.Instance.cells[_path[0].x, _path[0].y].entityOnCell = null;
		moveAlongPathCoroutine = StartCoroutine(MoveAlongPathCoroutine(_path));
	}

	IEnumerator MoveAlongPathCoroutine(Vector2Int[] _path)
	{
		for (int i = 1; i < _path.Length; i++)
		{
			float _completion = 0f;
			float _elapsedTime = 0f;
			Vector3 _startPos = transform.position;
			Vector3 _targetPos = new Vector3(_path[i].x, 0f, _path[i].y);

			while(_completion <= 1f)
			{
				_elapsedTime += Time.deltaTime;
				_completion = _elapsedTime / moveDuration;
				transform.position = Vector3.Lerp(_startPos, _targetPos, _completion);
				yield return null;
			}
		}

		LevelGrid.Instance.cells[_path[_path.Length - 1].x, _path[_path.Length - 1].y].entityOnCell = this;

		yield return null;
	}

	public bool IsMoving()
	{
		if(moveAlongPathCoroutine == null)
			return false;
		else
			return true;
	}

	public void TakeDamage(int _damages)
	{
		//Apply defense to _damages

		if(_damages > instaCat.health)
		{
			instaCat.health = 0;
			Death();
		}
		else
		{ 
			instaCat.health -= _damages;
		}
	}

	public void Heal(int _amount)
	{
		if(_amount > instaCatRef.GetHealth())
		{
			instaCat.health = instaCatRef.GetHealth();
		}
		else
		{
			instaCat.health += _amount;
		}
	}

	private void Death()
	{
		//
	}
}
