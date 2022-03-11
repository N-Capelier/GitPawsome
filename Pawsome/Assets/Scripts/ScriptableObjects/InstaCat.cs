using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "New InstaCat", menuName = "InstaCat", order = 50)]
public class InstaCat : ScriptableObject
{
	public string catName;
	[Space]
	public Sprite CatSprite;
	public Sprite largePortrait;
	public Sprite DeadCatSprite;
	[Space]
	public Material material;
	[Space]
	public Archetype catClass;
	[Space]
	public int deckSize;
	[Space]
	public int baseHealth;
	public int bonusHealth;
	[Space]
	public int baseMana;
	public int bonusMana;
	[Space]
	public int baseInitiative;
	public int bonusInitiative;
	[Space]
	public int baseAttack;
	public int bonusAttack;
	[Space]
	public int baseDefense;
	public int bonusDefense;
	[Space]
	public int baseMovePoints;
	public int bonusMovePoints;
	[Space]
	public int basePower;
	public int bonusPower;
	[Space]
	public bool Dead = false;

	public List<Spell> spells = new List<Spell>();
	public List<int> Temp = new List<int>();

	public int health;
	[HideInInspector] public int mana;
	[HideInInspector] public int initiative;
	[HideInInspector] public int attack;
	[HideInInspector] public int defense;
	[HideInInspector] public int movePoints;

	public int GetHealth()
	{
		return baseHealth + bonusHealth;
	}

	public int GetMana()
	{
		return baseMana + bonusMana;
	}

	public int GetInitiative()
	{
		return baseInitiative + bonusInitiative;
	}

	public int GetAttack()
	{
		return baseAttack + bonusAttack;
	}

	public int GetDefense()
	{
		return baseDefense + bonusDefense;
	}

	public int GetMovePoints()
	{
		return baseMovePoints + bonusMovePoints;
	}
	public void Heal(int amount)
    {
		if(health + amount > GetHealth())
        {
			health = GetHealth();
			return;
        }
		health += amount;
    }
}
