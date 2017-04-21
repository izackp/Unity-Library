using UnityEngine;
using CSharp_Library.Extensions;

public static class GameObjectExt {

    static Dictionary<GameObject> _dicPrefabCache = new Dictionary<GameObject>();
	public static GameObject Instance(string prefabName)
    {
        GameObject prefab = Prefab(prefabName);
        if (prefab == null)
            return null;
        return Object.Instantiate(prefab);
    }

    public static GameObject Prefab(string prefabName) {
        string path = prefabName;
        GameObject prefab = _dicPrefabCache.GetValueSafe(path);
        if (prefab == null) {
            prefab = Resources.Load<GameObject>(path);
            if (prefab == null) {
                try {
                    ResourceItem item = ResourceDB.GetAsset(prefabName);
                    if (item != null)
                        path = item.ResourcesPath;
                } catch {
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
        return prefab;
    }
}
