using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public static class MonoBehaviourExt {

	public static RectTransform RectTransform(this MonoBehaviour behavior) {
        if (behavior == null)
            return null;
        return behavior.transform as RectTransform;
    }

    public static RectTransform RectTransform(this UIBehaviour behavior) {
        if (behavior == null)
            return null;
        return behavior.transform as RectTransform;
    }

    public static T FindOrAddComponent<T>(this GameObject parent) where T : Component {
        T comp = parent.GetComponent<T>();
        if (comp == null)
            comp = parent.AddComponent<T>();
        return comp;
    }
}
