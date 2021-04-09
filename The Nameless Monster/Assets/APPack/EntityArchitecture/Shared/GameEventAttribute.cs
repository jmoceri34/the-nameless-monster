namespace APPack
{
    using System;

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class GameEventAttribute : Attribute
    {
        public readonly GameEventType EventType;

        public GameEventAttribute(GameEventType eventType)
        {
            EventType = eventType;
        }
    }
}