using UnityEngine.EventSystems;

namespace UnityEngine.UI {
    [AddComponentMenu("Layout/Layout Match Size", 140)]
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class LayoutMatchSize : UIBehaviour {

        [SerializeField] RectTransform _target;
        public RectTransform Target { get { return _target; } set { _target = value; SetDirty(); } }
        RectTransform _transform;
        
        protected override void OnEnable() {
            base.OnEnable();
            _transform = transform as RectTransform;
            SetDirty();
        }

        protected override void OnTransformParentChanged() {
            SetDirty();
        }
        
        protected override void OnDidApplyAnimationProperties() {
            SetDirty();
        }

        protected override void OnRectTransformDimensionsChange() {
            SetDirty();
        }

        protected void SetDirty() {
            if (!IsActive())
                return;
            if (Target == null)
                return;
            Target.SetWidth(_transform.Width());
            Target.SetHeight(_transform.Height());
        }

#if UNITY_EDITOR
        protected override void OnValidate() {
            SetDirty();
        }

#endif
    }
}
