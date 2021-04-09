using APPack;
using APPack.Effects;
using Assets.TheNamelessMonsterEventListenerService;
using System.Collections;
using TheNamelessMonster.Father;
using UnityEngine;

public class WalkWithBabyStopTrigger : MonoBehaviour
{
    private TheNamelessMonsterEventListenerService ListenerService;

    void Start()
    {
        ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.parent.parent.name == "Father")
        {
            collider.GetComponentInParent<FatherBll>().Stop();
            StartCoroutine(Scene1Timer());
        }
    }


    private IEnumerator Scene1Timer()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetGameObjectByTag("Canvas", "CupOfMoPresents").SetActive(true);

        yield return new WaitForSeconds(2f);
        var title = gameObject.GetGameObjectByTag("Canvas", "TheNamelessMonster");
        title.SetActive(true);
        title.GetComponent<APFade>().Play();
        title.GetComponent<APMove>().Play();

        yield return new WaitForSeconds(2.5f);
        gameObject.GetGameObjectByTag("Canvas", "Title").GetComponent<APFade>().Play();

        yield return new WaitForSeconds(2.5f);
        ListenerService.Invoke(GameEventType.MonsterActivate);

        gameObject.SetActive(false);
    }

}
