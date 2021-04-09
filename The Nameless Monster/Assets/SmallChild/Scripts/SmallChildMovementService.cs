using APPack;
using System.Linq;
using TheNamelessMonster.SmallChild;
using UnityEngine;

public class SmallChildMovementService : MonoBehaviour, IEntity
{
    public void OnAwake()
    {

    }

    public void OnStart()
    {

    }

    public void Move(SmallChildModel model, float xAxis)
    {
        var velocity = model.SmallChildRigidbody.velocity;
        model.SmallChildRigidbody.velocity = new Vector2(model.Speed * xAxis, velocity.y);
    }

    public bool OnGround(SmallChildModel model)
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
