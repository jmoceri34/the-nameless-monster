namespace TheNamelessMonster.Baby
{
    using APPack;
    using APPack.Effects;
    using System.Collections;
    using TheNamelessMonster.Portal;
    using UnityEngine;

    public class BabyAnimationService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Animator BabyAnimator;

		public void OnAwake()
		{

		}

		public void OnStart()
		{

		}

        public IEnumerator MoveIntoPortal()
        {
            var apMove = GetComponent<APMove>();

            apMove.Play();

            while (apMove.NormalizedTimeValue < 0.5f)
            {
                yield return null;
            }

            GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalBll>().Hide();

            StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(5f);
            gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Play();
            yield return new WaitForSeconds(2f);

            gameObject.GetGameObjectByTag("Canvas", "MusicThankYou").GetComponent<APFade>().Play();
            gameObject.GetGameObjectByTag("Canvas", "MusicThankYou").GetComponent<APMove>().Play();

            gameObject.GetGameObjectByTag("Canvas", "play-again-button").GetComponent<APFade>().Play();
            gameObject.GetGameObjectByTag("Canvas", "play-again-button").GetComponent<APMove>().Play();
        }
    }
}