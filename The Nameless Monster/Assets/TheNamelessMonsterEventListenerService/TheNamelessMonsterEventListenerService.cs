using APPack;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.TheNamelessMonsterEventListenerService
{
    public class TheNamelessMonsterEventListenerService : MonoBehaviour, IEntity
    {
        // Add as many game-wide events as required here
        [GameEvent(GameEventType.OnGameStart)]
        private UnityEvent OnGameStart = new UnityEvent();
        [GameEvent(GameEventType.OnGameOver)]
        private UnityEvent OnGameOver = new UnityEvent();
        [GameEvent(GameEventType.FatherTurnOffBaby)]
        private UnityEvent FatherTurnOffBaby = new UnityEvent();
        [GameEvent(GameEventType.MonsterActivate)]
        private UnityEvent MonsterActivate = new UnityEvent();
        [GameEvent(GameEventType.MonsterTurnOnBaby)]
        private UnityEvent MonsterTurnOnBaby = new UnityEvent();
        [GameEvent(GameEventType.MonsterMoveRight)]
        private UnityEvent MonsterMoveRight = new UnityEvent();
        [GameEvent(GameEventType.SmallChildMoveRight)]
        private UnityEvent SmallChildMoveRight = new UnityEvent();
        [GameEvent(GameEventType.FatherMoveRight)]
        private UnityEvent FatherMoveRight = new UnityEvent();
        [GameEvent(GameEventType.FatherSetMovementSpeed)]
        private UnityEvent FatherSetMovementSpeed = new UnityEvent();
        [GameEvent(GameEventType.SwitchCameraScene2)]
        private UnityEvent SwitchCameraScene2 = new UnityEvent();
        [GameEvent(GameEventType.SwitchCameraScene3)]
        private UnityEvent SwitchCameraScene3 = new UnityEvent();

        public void PlayAgainPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnAwake()
        {

        }

        public void OnStart()
        {

        }

        public void AddListener(GameEventType type, UnityAction action)
        {
            var field = typeof(TheNamelessMonsterEventListenerService).GetField(type.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            ((UnityEvent)field).AddListener(action);
        }

        public void AddListener<T>(GameEventType type, UnityAction<T> action)
        {
            var field = typeof(TheNamelessMonsterEventListenerService).GetField(type.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            ((UnityEvent<T>)field).AddListener(action);
        }

        public void Invoke(GameEventType type)
        {
            var field = typeof(TheNamelessMonsterEventListenerService).GetField(type.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            ((UnityEvent)field).Invoke();
        }

        private class UnityEventGeneric<T> : UnityEvent<T> { }
    }
}
