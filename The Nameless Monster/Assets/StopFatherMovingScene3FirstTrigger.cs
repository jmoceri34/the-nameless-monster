using System;
using System.Collections;
using Assets.TheNamelessMonsterEventListenerService;
using TheNamelessMonster.Father;
using UnityEngine;
using APPack;

public class StopFatherMovingScene3FirstTrigger : MonoBehaviour
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
            gameObject.GetGameObjectByTag("Canvas", "Scene-3-Text-1").SetActive(true);
            StartCoroutine(MoveFatherRight());
        }
    }

    private IEnumerator MoveFatherRight()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetGameObjectByTag("Canvas", "Scene-3-Text-1").SetActive(false);
        ListenerService.Invoke(GameEventType.FatherMoveRight);
        gameObject.SetActive(false);
    }
}
