using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.Events {
    public static class UnityEventExt {
        public static void AddListenerWeak(this UnityEvent obj, Action call) {
            obj.AddListener(new WeakAction(call));
        }
    }
}
