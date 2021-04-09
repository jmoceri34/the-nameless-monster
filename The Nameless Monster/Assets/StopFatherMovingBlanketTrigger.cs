using APPack;
using Assets.TheNamelessMonsterEventListenerService;
using TheNamelessMonster.Father;
using UnityEngine;

public class StopFatherMovingBlanketTrigger : MonoBehaviour
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
            collider.GetComponentInParent<FatherBll>().Stop();
            gameObject.SetActive(false);
            gameObject.GetGameObjectByTag("Canvas", "Choice-3").SetActive(true);
        }
    }
}
