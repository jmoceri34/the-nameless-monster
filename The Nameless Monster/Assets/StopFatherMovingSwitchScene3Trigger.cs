using APPack;
using Assets.TheNamelessMonsterEventListenerService;
using TheNamelessMonster.Father;
using UnityEngine;

public class StopFatherMovingSwitchScene3Trigger : MonoBehaviour
{
    private TheNamelessMonsterEventListenerService ListenerService;

    void Start()
    {
        ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.parent.name == "Father")
        {
            //collider.GetComponentInParent<FatherBll>().Stop();
            //gameObject.SetActive(false);
            //ListenerService.Invoke(GameEventType.SwitchCameraScene3);
            //var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            //GameObject.FindGameObjectWithTag("Player").transform.localPosition = new Vector3(34f, pos.y, pos.z);
            //ListenerService.Invoke(GameEventType.FatherMoveRight);
        }
    }
}
