using UnityEngine;

public class BattleInformationManager : Singleton<BattleInformationManager>
{
	public InstaCat[] instaCats;
	[SerializeField] BattleUIMode battleUIMode;

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

	public void Notifiate(NotificationProps props)
	{
		battleUIMode.historyUI.PushNotification(props);
	}
}
