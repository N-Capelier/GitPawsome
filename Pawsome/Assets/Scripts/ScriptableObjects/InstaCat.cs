using UnityEngine;

[CreateAssetMenu(fileName = "New InstaCat", menuName = "InstaCat", order = 50)]
public class InstaCat : ScriptableObject
{
	public string catName;
	[Space]
	public Mesh mesh;
	[Space]
	public Class catClass;
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
	public int baseMoveSpeed;
	public int bonusMoveSpeed;

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

	public int GetMoveSpeed()
	{
		return baseMoveSpeed + bonusMoveSpeed;
	}
}
