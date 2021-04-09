using APPack;
using Assets.TheNamelessMonsterEventListenerService;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private TheNamelessMonsterEventListenerService ListenerService;

    void Start ()
    {
		ListenerService = FindObjectOfType<TheNamelessMonsterEventListenerService>(); // Game state should always contain one and only one
        ListenerService.AddListener(GameEventType.SwitchCameraScene2, () => { transform.position = new Vector3(20f, 0.7f, -10); });
        ListenerService.AddListener(GameEventType.SwitchCameraScene3, () => { transform.position = new Vector3(39.5f, 0.7f, -10); });
        transform.position = new Vector3(0.5f, 0.7f, -10f);
    }
    
}
