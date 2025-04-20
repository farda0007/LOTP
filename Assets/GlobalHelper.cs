using UnityEngine;

public static class GlobalHelper
{
    public static string GenerateUniqueID(GameObject gameObject)
    {
        // Generate a unique ID based on the GameObject's name and its instance ID
        return $"{gameObject.name}_{gameObject.GetInstanceID()}";
    }
}
