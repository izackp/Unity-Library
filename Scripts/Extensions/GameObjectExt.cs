using UnityEngine;
using System.Collections;

public static class GameObjectExt {

    static Dictionary<GameObject> _dicPrefabCache = new Dictionary<GameObject>();
	public static GameObject Instance(string prefabName)
    {
        GameObject prefab = _dicPrefabCache.GetValueSafe(prefabName);
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>(prefabName);
            if (prefab == null)
            {
                Debug.LogWarning(prefabName + " prefab does not exisit.");
                return null;
            }
            _dicPrefabCache[prefabName] = prefab;
        }
        return Object.Instantiate(prefab);
    }
}
