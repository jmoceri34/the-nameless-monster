namespace TheNamelessMonster.Baby
{
    using UnityEngine;
    using APPack;
    using Assets.TheNamelessMonsterEventListenerService;
    using System;

    public class BabyBll : MonoBehaviour, IEntity
	{
		private TheNamelessMonsterEventListenerService ListenerService;
		
		public BabyModel Model;
		
		public BabyAnimationService AnimationService;
		public BabyAudioService AudioService;
		public BabyEffectsService EffectsService;
		public BabyUIService UIService;

		public void OnAwake()
		{
			ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
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
			AudioService.OnAwake();
			EffectsService.OnAwake();
			UIService.OnAwake();
		
		}

		// Only one of these per entity
		void Start ()
		{
			OnStart();
			Model.OnStart();
			ListenerService.OnStart();
			AnimationService.OnStart();
			AudioService.OnStart();
			EffectsService.OnStart();
			UIService.OnStart();
			
		}

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void MoveIntoPortal()
        {
            StartCoroutine(AnimationService.MoveIntoPortal());
        }
    }
}