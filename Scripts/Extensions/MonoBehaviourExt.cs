using UnityEngine;
using System.Collections;

public static class MonoBehaviourExt {

	public static RectTransform RectTransform(this MonoBehaviour behavior) {
        return behavior.transform as RectTransform;
    }
}
