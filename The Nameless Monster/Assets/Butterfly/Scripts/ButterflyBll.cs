namespace TheNamelessMonster.Butterfly
{
    using UnityEngine;
    using APPack;
    using Assets.TheNamelessMonsterEventListenerService;

    public class ButterflyBll : MonoBehaviour, IEntity
	{
		private TheNamelessMonsterEventListenerService ListenerService;
		
		public ButterflyModel Model;
		
		public ButterflyAnimationService AnimationService;
		public ButterflyEffectsService EffectsService;

		public void OnAwake()
		{
			ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
			ListenerService.AddListener(GameEventType.OnGameStart, StartGameCallback);
			ListenerService.AddListener(GameEventType.OnGameOver, EndGameCallback);
		}

		public void OnStart()
		{

		}
		
		// Only one of these per entity
		void Awake ()
		{
			OnAwake();
			Model.OnAwake();
			ListenerService.OnAwake();
			AnimationService.OnAwake();
			EffectsService.OnAwake();
		
		}

		// Only one of these per entity
		void Start ()
		{
			OnStart();
			Model.OnStart();
			ListenerService.OnStart();
			AnimationService.OnStart();
			EffectsService.OnStart();
			
		}

		private void EndGameCallback()
		{

		}

		private void StartGameCallback()
		{

		}
	}
}