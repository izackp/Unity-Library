using UnityEngine;
using System.Collections;

public static class MonoBehaviourExt {

	public static RectTransform RectTransform(this MonoBehaviour behavior) {
        if (behavior == null)
            return null;
        return behavior.transform as RectTransform;
    }
}
