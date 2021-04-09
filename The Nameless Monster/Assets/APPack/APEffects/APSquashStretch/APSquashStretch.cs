namespace APPack.Effects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    public class APSquashStretch : APEffectBase
    {
        [Range(0.1f, 3f)]
        public float Stretchiness;
        [Range(1f, 15f)]
        public float Denseness;

        public override APEffect Effect()
        {
            return APEffect.SquashStretch;
        }
    
        protected override void Activate()
        {
            var rects = Target.GetListOfComponents<Transform>(AffectChildren);
            StartCoroutine(EffectTimer(Length));
            StartCoroutine(ApplyEffect(rects));
        }

        private IEnumerator ApplyEffect(IList<Transform> graphics)
        {
            while (Running)
            {
                var curveValue = Curve.Evaluate(NormalizedTimeValue);
                UpdateEffect(graphics, curveValue);
                yield return null;
            }
        }

        protected void UpdateEffect(IList<Transform> graphics, float curveValue)
        {
            for (int i = 0; i < graphics.Count; i++)
            {
                var positive = curveValue > 0;

                var x = 1f + (positive ? -1f * curveValue / Denseness : Mathf.Abs(curveValue) * Stretchiness);
                var y = 1f + (positive ? curveValue * Stretchiness : -1f * Mathf.Abs(curveValue) / Denseness);
                graphics[i].localScale = new Vector3(Mathf.Clamp(x, Mathf.Epsilon, Mathf.Infinity), Mathf.Clamp(y, Mathf.Epsilon, Mathf.Infinity), 1f);
            }
        }
    }
}