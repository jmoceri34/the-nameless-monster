namespace TheNamelessMonster.Father 
{
    using UnityEngine;
    using APPack;
    using Baby;
    using Portal;
    using System;
    using APPack.Effects;
    using System.Collections;

    public class FatherAnimationService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Animator FatherAnimator;
        public FatherModel Model;

        public void OnAwake()
        {
            FatherAnimator = GetComponent<Animator>();
        }

        public void OnStart()
        {

        }

        public void SetModel(FatherModel model)
        {
            this.Model = model;
        }

        public void SetAnimatorMovementSpeed(float speed)
        {
            FatherAnimator.SetFloat("Speed", speed);
        }

        public void SetAnimatorJumpTrigger()
        {
            FatherAnimator.ResetTrigger("Jump");
            FatherAnimator.SetTrigger("Jump");
        }

        public void SetBabyState(bool state)
        {
            FatherAnimator.SetBool("Baby", state);
        }

        public void SetLanding(bool state)
        {
            FatherAnimator.SetBool("Landing", state);
        }

        public void SetLayerWeight(int layer, float weight)
        {
            FatherAnimator.SetLayerWeight(layer, weight);
        }

        public void IdleToKnees()
        {
            FatherAnimator.SetTrigger("IdleToKnees");
        }

        public void DoEnding()
        {
            switch (Model.Ending)
            {
                case "Good":
                    FatherAnimator.SetTrigger("KneesToBaby");
                    break;
                case "Bad":
                    FatherAnimator.SetTrigger("PulledIntoPortal");
                    GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalBll>().AnimateSmallToLarge();
                    break;
                case "SuperBad":
                    FatherAnimator.SetTrigger("KneesToCrying");
                    break;
            }
        }

        public void KneesToCryingEntry()
        {
            StartCoroutine(KneesToCryingTimer());
        }

        private IEnumerator KneesToCryingTimer()
        {
            yield return new WaitForSeconds(2f);
            IdleToKnees();
        }

        public void GoodEnding()
        {
            DisableAnimator();
            HidePortal();
            StartCoroutine(EndGame(2.5f));
        }

        private IEnumerator EndGame(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Play();
            yield return new WaitForSeconds(2f);
            gameObject.GetGameObjectByTag("Canvas", "MusicThankYou").GetComponent<APFade>().Play();
            gameObject.GetGameObjectByTag("Canvas", "MusicThankYou").GetComponent<APMove>().Play();

            gameObject.GetGameObjectByTag("Canvas", "play-again-button").GetComponent<APFade>().Play();
            gameObject.GetGameObjectByTag("Canvas", "play-again-button").GetComponent<APMove>().Play();
        }

        public void HidePortal()
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalBll>().Hide();
        }

        public void BadEnding()
        {
            DisableAnimator();
            StartCoroutine(MoveDelay());
            StartCoroutine(EndGame(4f));
        }

        private IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(1f);
            var apMove = GetComponent<APMove>();

            apMove.Play();

            while (apMove.NormalizedTimeValue < 0.6f)
            {
                yield return null;
            }

            GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalBll>().Hide();
        }

        public void DisableAnimator()
        {
            FatherAnimator.enabled = false;
        }

        public void HideBaby()
        {
            GameObject.FindGameObjectWithTag("Baby").GetComponent<BabyBll>().Hide();
        }

        public void KneesCrying()
        {
            FatherAnimator.SetBool("KneesCrying", true);
        }
    }
}