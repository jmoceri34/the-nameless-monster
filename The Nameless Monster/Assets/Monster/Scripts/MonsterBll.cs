namespace TheNamelessMonster.Monster
{
    using APPack;
    using APPack.Effects;
    using Assets.Father.Scripts;
    using Assets.TheNamelessMonsterEventListenerService;
    using System.Collections;
    using UnityEngine;

    public class MonsterBll : MonoBehaviour, IEntity
	{
		private TheNamelessMonsterEventListenerService ListenerService;
		
		public MonsterModel Model;
		
		public MonsterAnimationService AnimationService;
		public MonsterAudioService AudioService;
		public MonsterEffectsService EffectsService;
		public MonsterUIService UIService;
        public MonsterMovementService MovementService;

		public void OnAwake()
		{
			ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
            ListenerService.AddListener(GameEventType.MonsterActivate, Activate);
            ListenerService.AddListener(GameEventType.MonsterTurnOnBaby, TurnOnBaby);
            ListenerService.AddListener(GameEventType.MonsterMoveRight, MoveRight);
		}
        
        public void OnStart()
		{

		}

        public void FadeScreenInScene1()
        {
            StartCoroutine(Scene1());
        }

        private IEnumerator Scene1()
        {
            StopCoroutine("MoveLeft");
            Move(0);
            ListenerService.Invoke(GameEventType.MonsterTurnOnBaby);
            ListenerService.Invoke(GameEventType.FatherTurnOffBaby);
            transform.localPosition = new Vector3(1.130799f, -0.1746065f, 0f);
            transform.localScale = Vector3.one;
            yield return new WaitForSeconds(1f);
            gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Reverse();

            yield return new WaitForSeconds(2.5f);
            ListenerService.Invoke(GameEventType.MonsterMoveRight);
        }

        private void MoveRight()
        {
            StartCoroutine("MoveRightTimer");
            StartCoroutine("SmallChildMoveRight");
        }

        private IEnumerator SmallChildMoveRight()
        {
            yield return new WaitForSeconds(2f);
            ListenerService.Invoke(GameEventType.SmallChildMoveRight);
        }

        private IEnumerator MoveRightTimer()
        {
            var start = 0f;
            var end = 3f;
            while (start <= end)
            {
                Move(1);
                start += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            Move(0);
        }

        public void TurnOnBaby()
        {
            AnimationService.SetBabyState(true);
        }

        public void Activate()
        {
            transform.localScale = new Vector3(-1, 1, 1);
            AnimationService.Activate();
            StartCoroutine(Timer());
            StartCoroutine("MoveLeft");
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Play();
            yield return new WaitForSeconds(2.5f);
            FadeScreenInScene1();
        }

        private IEnumerator MoveLeft()
        {
            yield return new WaitForSeconds(1f);
            var start = 0f;
            var end = 3f;
            while (start <= end)
            {
                Move(-1);
                start += Time.deltaTime;
                yield return null;// new WaitForFixedUpdate();
            }
        }

        public void Stop()
        {
            StopCoroutine("MoveRight");
            Move(0);
        }

        public void Move(float xAxis)
        {
            AnimationService.SetAnimatorMovementSpeed(Mathf.Abs(Model.Speed * xAxis));
            MovementService.Move(Model, xAxis);
        }

        public void CheckGround()
        {
            Model.Grounded = MovementService.OnGround(Model);
            AnimationService.SetLayerWeight(1, Model.Grounded && Model.MonsterRigidbody.velocity.y == 0f ? 0f : 1f); // When jumping up through collider platform effector
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