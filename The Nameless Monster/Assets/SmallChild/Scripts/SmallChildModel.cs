namespace TheNamelessMonster.SmallChild 
{
	using UnityEngine;
	using APPack;
	
	public class SmallChildModel : MonoBehaviour, IEntity
	{
        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float SprintMovementSpeed;

        public bool Grounded;
        public bool Sprinting;
        public float Speed { get { return Sprinting ? SprintMovementSpeed : MovementSpeed; } }
        public Transform SmallChildTransform { get; private set; }
        public Rigidbody2D SmallChildRigidbody { get; private set; }
        public Transform[] GroundPoints;
        public LayerMask GroundLayers;
        public bool Jumping;
        public float MaxJumpForce;
        public float CurrentJumpForce;

        public void OnAwake()
        {
            SmallChildTransform = GetComponent<Transform>();
            SmallChildRigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnStart()
        {

        }
    }
}