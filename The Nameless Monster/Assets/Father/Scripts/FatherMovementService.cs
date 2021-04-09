using APPack;
using System.Collections;
using System.Linq;
using TheNamelessMonster.Father;
using UnityEngine;

namespace Assets.Father.Scripts
{
    public class FatherMovementService : MonoBehaviour, IEntity
    {
        public void OnAwake()
        {

        }

        public void OnStart()
        {

        }

        #region Jumping
        private IEnumerator DoJumpTimer(FatherModel model)
        {
            model.Jumping = true;
            yield return new WaitForSeconds(0.3f); // 0.3 second max jump duration
            model.Jumping = false;
        }

        private IEnumerator ReduceJumpForce(FatherModel model)
        {
            model.CurrentJumpForce = model.MaxJumpForce;

            while (model.CurrentJumpForce > 0f) // reduce the jump force over time
            {
                var reduction = model.CurrentJumpForce / 10f; // exponential decay with 2f
                model.CurrentJumpForce = Mathf.Clamp(model.CurrentJumpForce - reduction - 0.05f, 0f, int.MaxValue); // Subtract 0.05f to always reach zero eventually
                yield return new WaitForFixedUpdate();
            }
        }

        public void AddForceToJump(FatherModel model)
        {
            var velocity = model.FatherRigidbody.velocity;
            model.FatherRigidbody.velocity = new Vector2(velocity.x, model.CurrentJumpForce); // Set velocity directly for more control
        }

        public void Jump(FatherModel model)
        {
            model.Jumping = true;
            model.Grounded = false;
            model.FatherRigidbody.velocity = Vector2.zero;
            StartCoroutine("ReduceJumpForce", model);
            StartCoroutine("DoJumpTimer", model);
        }

        public void StopJumping(FatherModel model)
        {
            StopCoroutine("ReduceJumpForce");
            StopCoroutine("DoJumpTimer");
            model.CurrentJumpForce = 0f;
            AddForceToJump(model); // Set the velocity back to zero
            model.Jumping = false;
        }
        #endregion

        public void Move(FatherModel model, float xAxis)
        {
            var velocity = model.FatherRigidbody.velocity;
            model.FatherRigidbody.velocity = new Vector2(model.Speed * xAxis, velocity.y);
        }

        public bool OnGround(FatherModel model)
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
