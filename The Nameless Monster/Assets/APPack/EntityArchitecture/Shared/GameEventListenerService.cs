namespace APPack
{
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEventListenerService : MonoBehaviour, IEntity
    {
        // Add as many game-wide events as required here
        [GameEvent(GameEventType.OnGameStart)]
        private UnityEvent OnGameStart = new UnityEvent();
        [GameEvent(GameEventType.OnGameOver)]
        private UnityEvent OnGameOver = new UnityEvent();

        public void OnAwake()
        {

        }

        public void OnStart()
        {

        }

        public void AddListener(GameEventType type, UnityAction action)
        {
            var field = typeof(GameEventListenerService).GetField(type.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            ((UnityEvent)field).AddListener(action);
        }

        public void Invoke(GameEventType type)
        {
            var field = typeof(GameEventListenerService).GetField(type.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            ((UnityEvent)field).Invoke();
        }
    }
}