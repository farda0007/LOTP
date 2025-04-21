using UnityEngine;

public class PersistentUI : Singleton<PersistentUI>
{
    protected override void Awake()
    {
        base.Awake(); // Ensures Singleton behavior + DontDestroyOnLoad
    }
}
