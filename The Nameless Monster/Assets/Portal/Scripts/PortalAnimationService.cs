namespace TheNamelessMonster.Portal 
{
    using UnityEngine;
    using APPack;
    using System;
    using System.Collections;

    public class PortalAnimationService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Animator PortalAnimator;

		public void OnAwake()
		{

		}

		public void OnStart()
		{

		}

        public void SmallToLarge()
        {
            PortalAnimator.SetTrigger("SmallToLarge");
        }

        public void ActivateLarge()
        {
            PortalAnimator.SetBool("Large", true);
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }
    }
}