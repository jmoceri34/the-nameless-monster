namespace TheNamelessMonster.SmallChild 
{
	using UnityEngine;
	using APPack;

	public class SmallChildAnimationService : MonoBehaviour, IEntity
	{
		// Create all related fields you need that are unrelated to the Model here
		public Animator SmallChildAnimator;

		public void OnAwake()
		{

		}

		public void OnStart()
		{

		}

        public void SetAnimatorMovementSpeed(float speed)
        {
            SmallChildAnimator.SetFloat("Speed", speed);
        }
        
        public void SetLayerWeight(int layer, float weight)
        {
            SmallChildAnimator.SetLayerWeight(layer, weight);
        }
    }
}