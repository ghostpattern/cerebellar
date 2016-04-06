using System;
using UnityEngine;

[Serializable]
public class StoryElementData : ScriptableObject
{
    public string Key;
    public string Name;

    public bool EqualsNameOrKey(string nameOrKey)
    {
        nameOrKey = nameOrKey.Trim(' ');
        return string.Equals(nameOrKey, Key, StringComparison.InvariantCultureIgnoreCase) ||
               string.Equals(nameOrKey, Name, StringComparison.InvariantCultureIgnoreCase);
    }
}