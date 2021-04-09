using UnityEngine;
using System.Collections;
using Assets.TheNamelessMonsterEventListenerService;
using TheNamelessMonster.Father;
using TheNamelessMonster.Baby;
using TheNamelessMonster.Portal;

public class StopFatherMovingScene3SecondTrigger : MonoBehaviour 
{
    public Material spriteMaskMaterial;
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
            var choice1 = PlayerPrefs.GetInt("Choice-1");
            var choice2 = PlayerPrefs.GetInt("Choice-2");
            var choice3 = PlayerPrefs.GetInt("Choice-3");

            var fatherBll = GameObject.FindGameObjectWithTag("Player").GetComponent<FatherBll>();

            if (choice2 == 2 /*good*/ && choice3 == 2 /*bad*/)
            {
                StartCoroutine(BadEnding(fatherBll));
            } 
            else if(choice2 == 1 /*super bad ignored baby boy*/)
            {
                StartCoroutine(SuperBadEnding(fatherBll));
            }
            else if (choice2 == 2 && choice3 == 1) // good ending
            {
                StartCoroutine(GoodEnding(fatherBll));
            }
        }
    }

    private IEnumerator GoodEnding(FatherBll fatherBll)
    {
        fatherBll.SetEnding("Good");
        fatherBll.IdleToKnees();
        yield return null;
    }

    private IEnumerator BadEnding(FatherBll fatherBll)
    {
        fatherBll.SetEnding("Bad");
        fatherBll.IdleToKnees();
        yield return null;
    }

    private IEnumerator SuperBadEnding(FatherBll fatherBll)
    {
        var spriteMask = GameObject.FindGameObjectWithTag("sprite-mask-helper").GetComponent<SpriteMask>();

        var baby = GameObject.FindGameObjectWithTag("Baby");

        baby.GetComponent<BabyBll>().AnimationService.GetComponent<SpriteRenderer>().sharedMaterial = spriteMaskMaterial;

        spriteMask.maskedObjects.Add(baby.transform);

        var s = baby.AddComponent<SpriteMaskingComponent>();

        s.owner = spriteMask;

        spriteMask.update();

        fatherBll.SetEnding("SuperBad");
        GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalBll>().AnimateSmallToLarge();
        GameObject.FindGameObjectWithTag("Baby").GetComponent<BabyBll>().MoveIntoPortal();
        fatherBll.KneesToCryingEntry();
        yield return null;
    }
}
