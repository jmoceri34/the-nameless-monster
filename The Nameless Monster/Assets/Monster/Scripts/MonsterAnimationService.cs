namespace TheNamelessMonster.Monster 
{
    using UnityEngine;
    using APPack;
    using System;

    public class MonsterAnimationService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Animator MonsterAnimator;
        public Sprite defaultSprite;

		public void OnAwake()
		{
            MonsterAnimator.SetBool("Active", false);
        }

        public void OnStart()
		{

		}

        public void SetBabyState(bool state)
        {
            MonsterAnimator.SetBool("Baby", state);
        }

        public void SetAnimatorMovementSpeed(float speed)
        {
            MonsterAnimator.SetFloat("Speed", speed);
        }

        public void Activate()
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            MonsterAnimator.SetBool("Active", true);
        }

        public void SetLayerWeight(int layer, float weight)
        {
            MonsterAnimator.SetLayerWeight(layer, weight);
        }
    }
}