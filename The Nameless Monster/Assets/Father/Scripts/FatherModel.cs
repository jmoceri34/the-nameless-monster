namespace TheNamelessMonster.Father 
{
	using UnityEngine;
	using APPack;
	
	public class FatherModel : MonoBehaviour, IEntity
	{
        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float SprintMovementSpeed;

        public string Ending;
        public bool Grounded;
        public bool Sprinting;
        public float Speed { get { return Sprinting ? SprintMovementSpeed : MovementSpeed; } }
        public Transform FatherTransform { get; private set; }
        public Rigidbody2D FatherRigidbody { get; private set; }
        public Transform[] GroundPoints;
        public LayerMask GroundLayers;
        public bool Jumping;
        public float MaxJumpForce;
        public float CurrentJumpForce;

        public void SetMovementSpeed(float speed)
        {
            MovementSpeed = speed;
        }

        public void OnAwake()
        {
            FatherTransform = GetComponent<Transform>();
            FatherRigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnStart()
        {

        }
    }
}