namespace TheNamelessMonster.Father
{
    using UnityEngine;
    using APPack;
    using Assets.Father.Scripts;
    using Assets.TheNamelessMonsterEventListenerService;
    using System.Collections;
    using System.Linq;
    using System;

    public class FatherBll : MonoBehaviour, IEntity
    {
        private TheNamelessMonsterEventListenerService ListenerService;

        public FatherAudioService AudioService;
        public FatherAnimationService AnimationService;
        public FatherEffectsService EffectsService;
        public FatherUIService UIService;
        public FatherModel Model;
        public FatherMovementService MovementService;

        #region Initialization
        public void OnAwake()
        {
            ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
            ListenerService.AddListener(GameEventType.FatherTurnOffBaby, TurnOffBaby);
            ListenerService.AddListener(GameEventType.FatherMoveRight, MoveRightCallback);
            ListenerService.AddListener(GameEventType.FatherSetMovementSpeed, () => { Model.SetMovementSpeed(2); });
        }

        public void OnStart()
        {
            MoveRightCallback();
            AnimationService.SetModel(Model);
        }

        // Only one of these per entity
        void Awake()
        {
            OnAwake();
            ListenerService.OnAwake();
            AudioService.OnAwake();
            AnimationService.OnAwake();
            //CameraService.OnAwake();
            EffectsService.OnAwake();
            UIService.OnAwake();
            Model.OnAwake();
        }

        public void IdleToKnees()
        {
            AnimationService.IdleToKnees();
        }

        public void KneesToCryingEntry()
        {
            AnimationService.KneesToCryingEntry();
        }

        // Only one of these per entity
        void Start()
        {
            OnStart();
            ListenerService.OnStart();
            AudioService.OnStart();
            AnimationService.OnStart();
            //CameraService.OnStart();
            EffectsService.OnStart();
            UIService.OnStart();
            Model.OnStart();
        }
        
        public void SetEnding(string ending)
        {
            Model.Ending = ending;
        }
        #endregion

        #region Public Methods
        public void CameraFollow()
        {
            //CameraService.FollowTarget(Model);
        }

        public void Move(float xAxis)
        {
            AnimationService.SetAnimatorMovementSpeed(Mathf.Abs(Model.Speed * xAxis));
            MovementService.Move(Model, xAxis);
        }

        public void ChangeXDirection(float xAxis)
        {
            if (xAxis != 0) // Retain direction when stopped
            {
                var scale = Model.FatherTransform.localScale;
                Model.FatherTransform.localScale = new Vector3(Mathf.Abs(scale.x) * Mathf.Sign(xAxis), scale.y, scale.z); // flip sprite
                if (Mathf.Sign(xAxis) != Mathf.Sign(scale.x)) // Activate camera deadzone when changing x direction
                {
                    //CameraService.ActivateDeadzoneX(Model);
                }
            }
        }

        public void CheckGround()
        {
            Model.Grounded = MovementService.OnGround(Model);
            AnimationService.SetLayerWeight(1, Model.Grounded && Model.FatherRigidbody.velocity.y == 0f ? 0f : 1f); // When jumping up through collider platform effector
        }
        #endregion

        #region Callback Methods
        public void MoveRightCallback()
        {
            StartCoroutine("MoveRight");
        }

        public void TurnOffBaby()
        {
            AnimationService.SetBabyState(false);
        }

        public void Stop()
        {
            StopCoroutine("MoveRight");
            Move(0);
        }
        
        public IEnumerator MoveRight()
        {
            while (true)
            {
                Move(1); // move right until otherwise
                yield return new WaitForFixedUpdate();
            }
        }
        #endregion
    }
}