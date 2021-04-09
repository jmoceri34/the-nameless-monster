namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APRotate : APEffectBase
    {
        public bool RotateX;
        public bool RotateY;
        public bool RotateZ;

        public override APEffect Effect()
        {
            return APEffect.Rotate;
        }
    
        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        private IEnumerator ApplyEffect(IList<Transform> rects)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                UpdateEffect(rects, curveValue);
                yield return null;
            }
        }

        protected void UpdateEffect(IList<Transform> rects, float curveValue)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                rects[i].localRotation = GetRotation(rects[i], curveValue);
            }
        }

        private Quaternion GetRotation(Transform rect, float curveValue)
        {
            var rotateValue = curveValue * 360f;
            var x = RotateX ? 1 : 0;
            var y = RotateY ? 1 : 0;
            var z = RotateZ ? 1 : 0;
            var axis = new Vector3(x, y, z);
            return Quaternion.AngleAxis(rotateValue, axis);
        }
    }
}