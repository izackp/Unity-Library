using UnityEngine;

public static class GameObjectExt {

    static Dictionary<GameObject> _dicPrefabCache = new Dictionary<GameObject>();
	public static GameObject Instance(string prefabName)
    {
        string path = prefabName;
        GameObject prefab = _dicPrefabCache.GetValueSafe(path);
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>(path);
            if (prefab == null) {
                try {
                    path = ResourceDB.GetAsset(prefabName).ResourcesPath;
                }
                catch {
                    Debug.LogWarning("There was a problem reading the Resource Database. Please update the db.");
                    return null;
                }
                prefab = Resources.Load<GameObject>(path);
            }

            if (prefab == null) {
                Debug.LogWarning(prefabName + " prefab does not exisit.");
                return null;
            }
            _dicPrefabCache[prefabName] = prefab;
        }
        return Object.Instantiate(prefab);
    }
}
