using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UnityThreadDispatch : MonoBehaviour {

    readonly static Queue<Action> _actionQueue = new Queue<Action>();

    public void Update() {
        lock (_actionQueue) {
            while (_actionQueue.Count > 0) {
                _actionQueue.Dequeue().Invoke();
            }
        }
    }

    public void EnqueueAction(Action action) {
        lock (_actionQueue) {
            _actionQueue.Enqueue(action);
        }
    }

    //Ironically this code needs to run on the unity thread at some point
    public static void Load() {
        if (_instance != null)
            return;
        GameObject go = new GameObject("~UnityThreadDispatch");
        _instance = go.AddComponent<UnityThreadDispatch>();
    }

    static UnityThreadDispatch _instance = null;
    public static UnityThreadDispatch Instance() {
        return _instance;
    }

    static public void Enqueue(Action action) {
        _instance.EnqueueAction(action);
    }
}
