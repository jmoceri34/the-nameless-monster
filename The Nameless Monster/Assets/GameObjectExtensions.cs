using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject GetGameObjectByTag(this GameObject gameObject, string parentName, string tag)
    {
        return GameObject.Find(parentName).GetComponentsInChildren<Component>(true).First(rt => rt.tag == tag).gameObject;
    }
}
