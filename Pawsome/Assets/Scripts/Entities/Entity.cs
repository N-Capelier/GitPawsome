using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Nicolas
/// Last modified by Nicolas
/// </summary>
public abstract class Entity : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] float moveDuration = 10f;

	[Header("References")]
	[SerializeField] SkinnedMeshRenderer objectRenderer;
	public GameObject models;
	[SerializeField] GameObject possessionRenderer;
	public AnimationHandler animationHandler;

	[SerializeField] GameObject damageParticle;
	[SerializeField] GameObject healParticle;

	[SerializeField] Sprite deathIcon;

	[HideInInspector] public bool isPlayerEntity = false;
	[HideInInspector] public bool isPlaying = false;

	InstaCat instaCat;
	public InstaCat InstaCat { get => instaCat; private set => instaCat = value; }

	InstaCat instaCatRef;
	public InstaCat InstaCatRef { get => instaCatRef; private set => instaCatRef = value; }


	[HideInInspector] public List<Spell> deck = new List<Spell>();

	[HideInInspector] public List<Spell> hand = new List<Spell>();

	[HideInInspector] public List<Spell> discardPile = new List<Spell>();

	Coroutine moveAlongPathCoroutine = null;

	public static Action<string> EntityDeath;
	public Action HealthChanged;
	public Action ManaChanged;

	[HideInInspector] public Entity redirectedDamages = null;
	[HideInInspector] public int chachacha = 0;
	bool isInvincible = false;
	[HideInInspector] public bool hasNineLives = false;

	public static Action<Entity> CatDeath;

	public void Init(InstaCat _instaCat)
	{
		instaCat = Instantiate(_instaCat);
		instaCatRef = _instaCat;

		objectRenderer.material = instaCat.material;
		instaCat.health = instaCat.GetHealth();
		instaCat.mana = instaCat.GetMana();
		instaCat.initiative = instaCat.GetInitiative();
		instaCat.attack = instaCat.GetAttack();
		instaCat.defense = instaCat.GetDefense();
		instaCat.movePoints = instaCat.GetMovePoints();

		foreach(Spell _spell in instaCat.spells)
		{
			deck.Add(Instantiate(_spell));
		}
		InstaCat.spells = new List<Spell>();
		deck.Shuffle();

		//create hand
		for (int i = 0; i < 3; i++)
		{
			hand.Add(deck[0]);
			deck.RemoveAt(0);
		}

		//set renderer
	}

	public Vector2Int GetGridPosition()
	{
		return new Vector2Int((int)transform.position.x, (int)transform.position.z);
	}

	public void SetPossessionRenderer(bool value)
	{
		possessionRenderer.SetActive(value);
	}

	public void DiscardSpell(int _spellIndex)
	{
		discardPile.Add(hand[_spellIndex]);
		if (deck.Count > 0)
		{
			hand[_spellIndex] = deck[0];
			deck.RemoveAt(0);
		}
		else
		{
			deck = discardPile;
			discardPile = new List<Spell>();
			deck.Shuffle();
			hand[_spellIndex] = deck[0];
			deck.RemoveAt(0);
		}
	}

	public void MoveAlongPath(Vector2Int[] _path)
	{
		if(LevelGrid.Instance.Exist(new Vector2Int(_path[0].x, _path[0].y)))
			LevelGrid.Instance.cells[_path[0].x, _path[0].y].entityOnCell = null; //May cause a null ref, or an Armageddon
		moveAlongPathCoroutine = StartCoroutine(MoveAlongPathCoroutine(_path));
	}

	IEnumerator MoveAlongPathCoroutine(Vector2Int[] _path)
	{
		animationHandler.StartMove();

		for (int i = 1; i < _path.Length; i++)
		{
			//Set renderer direction
			if(_path[i].x > transform.position.x) //East
			{
				models.transform.forward = new Vector3(1f, 0f, 0f);
			}
			else if (_path[i].x < transform.position.x) //West
			{
				models.transform.forward = new Vector3(-1f, 0f, 0f);
			}
			else if (_path[i].y > transform.position.z) //North
			{
				models.transform.forward = new Vector3(0f, 0f, 1f);
			}
			else if (_path[i].y < transform.position.z) //South
			{
				models.transform.forward = new Vector3(0f, 0f, -1f);
			}

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

		animationHandler.EndMove();
		yield return null;
	}

	public bool IsMoving()
	{
		if(moveAlongPathCoroutine == null)
			return false;
		else
			return true;
	}

	public void SetInvincibility()
	{
		isInvincible = true;
	}

	public void ClearInvincibility()
	{
		isInvincible = false;
	}

	public void TakeDamage(int _damages, Entity _caster)
	{
		//Apply defense to _damages

		if (isInvincible)
			return;

		if(redirectedDamages != null)
		{
			redirectedDamages.TakeDamage(_damages, _caster);
			return;
		}

		StartCoroutine(AnimateParticles(damageParticle));

		if(_damages > instaCat.health)
		{
			if(hasNineLives)
			{
				hasNineLives = false;
				instaCat.health = 1;
				HealthChanged?.Invoke();
				return;
			}
			else
			{
				instaCat.health = 0;
				HealthChanged?.Invoke();
				CatDeath?.Invoke(this);
				Death(_caster);
				return;
			}
		}
		else
		{
			instaCat.health -= _damages;
			animationHandler.Hit();
			HealthChanged?.Invoke();
			return;
		}
	}

	public void Heal(int _amount)
	{
		StartCoroutine(AnimateParticles(healParticle));

		if (_amount > instaCatRef.GetHealth())
		{
			instaCat.health = instaCatRef.GetHealth();
		}
		else
		{
			instaCat.health += _amount;
		}

		HealthChanged?.Invoke();
	}

	public void HealInZone()
	{
		if (chachacha == 0)
			return;

		chachacha--;
		Vector2Int catPos = GetGridPosition();
		Heal(20);
		if(LevelGrid.Instance.cells[catPos.x + 1, catPos.y].entityOnCell != null)
		{
			LevelGrid.Instance.cells[catPos.x + 1, catPos.y].entityOnCell.Heal(20);
		}
		if (LevelGrid.Instance.cells[catPos.x - 1, catPos.y].entityOnCell != null)
		{
			LevelGrid.Instance.cells[catPos.x - 1, catPos.y].entityOnCell.Heal(20);
		}
		if (LevelGrid.Instance.cells[catPos.x, catPos.y + 1].entityOnCell != null)
		{
			LevelGrid.Instance.cells[catPos.x, catPos.y + 1].entityOnCell.Heal(20);
		}
		if (LevelGrid.Instance.cells[catPos.x, catPos.y - 1].entityOnCell != null)
		{
			LevelGrid.Instance.cells[catPos.x, catPos.y - 1].entityOnCell.Heal(20);
		}
	}

	public void TakeManaDamage(int _amount)
	{
		if (instaCat.mana - _amount < 0)
			instaCat.mana = 0;
		else
			instaCat.mana -= _amount;

		ManaChanged?.Invoke();
	}

	public void HealMana(int _amount)
	{
		if (_amount > instaCat.GetMana())
		{
			instaCat.mana = instaCatRef.GetMana();
		}
		else
		{
			instaCat.mana += _amount;
		}

		ManaChanged?.Invoke();
	}

	public void Death(Entity _caster)
	{
		BattleInformationManager.Instance.Notifiate(new NotificationProps(_caster, this, true, deathIcon, "Death", $"{_caster.instaCat.name} knocked out {instaCat.name}!"));
		EntityDeath?.Invoke(instaCat.catName);
		Destroy(gameObject);
	}

	IEnumerator AnimateParticles(GameObject _particle)
	{
		_particle.SetActive(true);
		yield return new WaitForSeconds(.4f);
		_particle.SetActive(false);
	}
}
