namespace TheNamelessMonster.Monster 
{
	using UnityEngine;
	using APPack;
	
	public class MonsterModel : MonoBehaviour, IEntity
	{
        [SerializeField]
        private float MovementSpeed;
        [SerializeField]
        private float SprintMovementSpeed;

        public bool Grounded;
        public bool Sprinting;
        public float Speed { get { return Sprinting ? SprintMovementSpeed : MovementSpeed; } }
        public Transform MonsterTransform { get; private set; }
        public Rigidbody2D MonsterRigidbody { get; private set; }
        public Transform[] GroundPoints;
        public LayerMask GroundLayers;
        public bool Jumping;
        public float MaxJumpForce;
        public float CurrentJumpForce;

        public void OnAwake()
        {
            MonsterTransform = GetComponent<Transform>();
            MonsterRigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnStart()
        {

        }
    }
}