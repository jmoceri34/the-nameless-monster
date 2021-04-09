namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APScale : APEffectBase
    {
        public bool ScaleX;
        public bool ScaleY;
        public bool ScaleZ;

        public override APEffect Effect()
        {
            return APEffect.Scale;
        }

        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        protected IEnumerator ApplyEffect(IList<Transform> graphics)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                UpdateEffect(graphics, curveValue);
                yield return null;
            }
        }

        private Vector3 GetScale(Transform rect, float curveValue, bool positive)
        {
            var x = ScaleX ? 1f + curveValue : rect.localScale.x;
            var y = ScaleY ? 1f + curveValue : rect.localScale.y;
            var z = ScaleZ ? 1f + curveValue : rect.localScale.z;
        
            return new Vector3(x, y, z);
        }

        protected void UpdateEffect(IList<Transform> rects, float curveValue)
        {
            for (int i = 0; i < rects.Count; i++)
                rects[i].localScale = GetScale(rects[i], curveValue, curveValue > 0);
        }
    }
}