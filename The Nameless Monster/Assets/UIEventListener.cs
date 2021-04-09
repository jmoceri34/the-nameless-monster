using APPack;
using APPack.Effects;
using Assets.TheNamelessMonsterEventListenerService;
using System.Collections;
using TheNamelessMonster.Father;
using UnityEngine;

public class UIEventListener : MonoBehaviour
{
    private TheNamelessMonsterEventListenerService ListenerService;
    
    void Start()
    {
        ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
    }

    public void Choice1(int choice)
    {
        PlayerPrefs.SetInt("Choice-1", choice);
        // Transition to next scene
        gameObject.GetGameObjectByTag("Canvas", "Choice-1").SetActive(false);
        ListenerService.Invoke(GameEventType.MonsterActivate);
        ListenerService.Invoke(GameEventType.MonsterTurnOnBaby);
        ListenerService.Invoke(GameEventType.FatherTurnOffBaby);
        ListenerService.Invoke(GameEventType.MonsterMoveRight);
    }

    public void Choice2(int choice)
    {
        PlayerPrefs.SetInt("Choice-2", choice);
        // Transition to next scene
        gameObject.GetGameObjectByTag("Canvas", "Choice-2").SetActive(false);

        if (choice == 1)
        {
            StartCoroutine(Choice2Timer());
        }
        else if (choice == 2)
        {
            ListenerService.Invoke(GameEventType.FatherSetMovementSpeed);
            ListenerService.Invoke(GameEventType.FatherMoveRight);
        }
    }

    private IEnumerator Choice2Timer()
    {
        gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Play();
        yield return new WaitForSeconds(2.5f);

        ListenerService.Invoke(GameEventType.SwitchCameraScene3);

        var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject.FindGameObjectWithTag("Player").transform.localPosition = new Vector3(34f, pos.y, pos.z);

        gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Reverse();
        yield return new WaitForSeconds(2.5f);
        ListenerService.Invoke(GameEventType.FatherSetMovementSpeed);
        ListenerService.Invoke(GameEventType.FatherMoveRight);
    }

    public void Choice3(int choice)
    {
        PlayerPrefs.SetInt("Choice-3", choice);

        gameObject.GetGameObjectByTag("Canvas", "Choice-3").SetActive(false);

        if(choice == 1)
            GameObject.FindGameObjectWithTag("Blanket").SetActive(false);

        StartCoroutine(Choice3Timer());
    }

    private IEnumerator Choice3Timer()
    {
        ListenerService.Invoke(GameEventType.FatherSetMovementSpeed);
        ListenerService.Invoke(GameEventType.FatherMoveRight);

        yield return new WaitForSeconds(2f);
        gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Play();
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FatherBll>().Stop();
        yield return new WaitForSeconds(1.5f);

        ListenerService.Invoke(GameEventType.SwitchCameraScene3);
        var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject.FindGameObjectWithTag("Player").transform.localPosition = new Vector3(34f, pos.y, pos.z);
        gameObject.GetGameObjectByTag("Canvas", "ScreenFader").GetComponent<APFade>().Reverse();
        yield return new WaitForSeconds(3f);
        ListenerService.Invoke(GameEventType.FatherMoveRight);
    }
}
