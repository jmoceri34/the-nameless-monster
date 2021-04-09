using TheNamelessMonster.SmallChild;
using UnityEngine;

public class SmallChildMoveRightTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.parent.name == "SmallChild")
        {
            collider.GetComponentInParent<SmallChildBll>().StopMoveRight();
            gameObject.SetActive(false);
        }
    }
}
