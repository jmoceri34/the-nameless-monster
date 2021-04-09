namespace TheNamelessMonster.Monster 
{
	using UnityEngine;
	using APPack;

	public class MonsterUIService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Transform UITempSpawnPoint;
		public GameObject UITempScoreTextPrefab;

		public void OnAwake()
		{

		}

		public void OnStart()
		{

		}
	}
}