using UnityEngine;

public class BattleInformationManager : Singleton<BattleInformationManager>
{
	public InstaCat[] instaCats;

	[Header("Debug")]
	[SerializeField] InstaCat[] fakeInstaCatArray;

	private void Awake()
	{
		CreateSingleton(true);
	}

	public InstaCat[] GetInstaCats()
	{
		return fakeInstaCatArray;
	}
}
