using UnityEngine;
using CSharp_Library.Extensions;
using UnityEngine.UI;

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

    public static GameObject CreatePanel(string name, Color color, Transform parent, RectAnchor anchorX, RectAnchor anchorY) {
        GameObject go = new GameObject(name);
        go.AddComponent<CanvasRenderer>();
        RectTransform trans = go.AddComponent<RectTransform>();
        Image img = go.AddComponent<Image>();
        img.color = color;

        trans.SetParent(parent);
        trans.SetAnchor(anchorX);
        trans.SetAnchor(anchorY);
        trans.offsetMin = Vector2.zero;
        trans.offsetMax = Vector2.zero;
        return go;
    }
}
