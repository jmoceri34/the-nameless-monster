namespace TheNamelessMonster.SmallChild
{
    using APPack;
    using Assets.TheNamelessMonsterEventListenerService;
    using System.Collections;
    using UnityEngine;

    public class SmallChildBll : MonoBehaviour, IEntity
	{
		private TheNamelessMonsterEventListenerService ListenerService;
		
		public SmallChildModel Model;

        public SmallChildAnimationService AnimationService;
		public SmallChildAudioService AudioService;
		public SmallChildEffectsService EffectsService;
		public SmallChildUIService UIService;
        public SmallChildMovementService MovementService;

        public void OnAwake()
		{
			ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
            ListenerService.AddListener(GameEventType.SmallChildMoveRight, MoveRight);
		}

		public void OnStart()
		{

		}

        public void StopMoveRight()
        {
            StopCoroutine("MoveRightTimer");
            Move(0);
            gameObject.GetGameObjectByTag("Canvas", "Choice-2").SetActive(true);
        }

        private void MoveRight()
        {
            StartCoroutine("MoveRightTimer");
        }

        private IEnumerator MoveRightTimer()
        {
            while (true)
            {
                Move(1);
                yield return new WaitForFixedUpdate();
            }
        }
        
        public void Move(float xAxis)
        {
            AnimationService.SetAnimatorMovementSpeed(Mathf.Abs(Model.Speed * xAxis));
            MovementService.Move(Model, xAxis);
        }

        public void CheckGround()
        {
            Model.Grounded = MovementService.OnGround(Model);
            AnimationService.SetLayerWeight(1, Model.Grounded && Model.SmallChildRigidbody.velocity.y == 0f ? 0f : 1f); // When jumping up through collider platform effector
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
	}
}