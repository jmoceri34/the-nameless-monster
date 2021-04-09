using APPack;
using System.Linq;
using TheNamelessMonster.Father;
using TheNamelessMonster.Monster;
using UnityEngine;

namespace Assets.Father.Scripts
{
    public class MonsterMovementService : MonoBehaviour, IEntity
    {
        public void OnAwake()
        {

        }

        public void OnStart()
        {

        }
        
        public void Move(MonsterModel model, float xAxis)
        {
            var velocity = model.MonsterRigidbody.velocity;
            model.MonsterRigidbody.velocity = new Vector2(model.Speed * xAxis, velocity.y);
        }

        public bool OnGround(MonsterModel model)
        {
            foreach (var ground in model.GroundPoints)
            {
                var colliders = Physics2D.OverlapCircleAll(ground.position, 0.05f, model.GroundLayers);
                if (colliders.FirstOrDefault(c => c.gameObject != gameObject) != null)
                    return true;
            }
            return false;
        }
    }
}
