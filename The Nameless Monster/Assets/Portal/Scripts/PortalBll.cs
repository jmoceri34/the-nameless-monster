namespace TheNamelessMonster.Portal
{
    using UnityEngine;
    using APPack;
    using Assets.TheNamelessMonsterEventListenerService;
    using System;

    public class PortalBll : MonoBehaviour, IEntity
	{
		private TheNamelessMonsterEventListenerService ListenerService;
		
		public PortalModel Model;

        public PortalAnimationService AnimationService;
		public PortalAudioService AudioService;
		public PortalEffectsService EffectsService;
		public PortalUIService UIService;

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

        public void AnimateSmallToLarge()
        {
            Model.Light.gameObject.SetActive(true);
            AnimationService.SmallToLarge();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}