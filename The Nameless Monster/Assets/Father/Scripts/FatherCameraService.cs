using APPack;
using System.Collections;
using TheNamelessMonster.Father;
using UnityEngine;

namespace Assets.Father.Scripts
{
    public class FatherCameraService : MonoBehaviour, IEntity
    {
        private Vector2 Velocity;
        private Vector2 OriginalOffset;
        private float? LastCaveGuyXPositionLeft;
        private float? LastCaveGuyXPositionRight;

        public Vector2 Offset;
        public float SmoothTimeX;
        public float SmoothTimeY;
        private bool FollowTargetX;
        private bool FollowTargetY;
        private float XDistance;

        private float NewY;
        private Transform CameraTransform;
        private Camera Cam;
        private Coroutine UnfollowYCouroutine;

        public void OnAwake()
        {
            CameraTransform = GetComponent<Transform>();
            var entityPosition = GetComponentInParent<FatherController>().transform.position;
            CameraTransform.position = new Vector3(entityPosition.x, entityPosition.y, CameraTransform.position.z);
            Cam = GetComponent<Camera>();
            transform.parent = null;
            OriginalOffset = Offset;
        }

        public void OnStart()
        {

        }

        #region Camera Follow Methods        
        public void ActivateDeadzoneX(FatherModel model)
        {
            FollowTargetX = false;
            if ((int)Mathf.Sign(model.FatherTransform.localScale.x) == 1) // This is to figure out where the camera should pick up when going back in the opposite direction
                LastCaveGuyXPositionLeft = model.FatherTransform.position.x;
            else
                LastCaveGuyXPositionRight = model.FatherTransform.position.x;
        }

        public void FollowTarget(FatherModel model)
        {
            Cam.orthographicSize = (Screen.height / 100f) / 4f; // Maintain size when changing resolution

            var x = CameraTransform.position.x;
            var y = CameraTransform.position.y;
            var offsetX = (Offset.x * Mathf.Sign(model.FatherTransform.localScale.x)); // scale determines direction, and creates an implicit deadzone based on it's sign

            var caveGuyViewportPosition = Camera.main.WorldToViewportPoint(model.FatherTransform.position);

            if (!FollowTargetX)
            {
                if (caveGuyViewportPosition.x <= 0.25f || model.FatherTransform.position.x < LastCaveGuyXPositionLeft) // Don't do equals otherwise it'll snap every direction change
                {
                    FollowTargetX = true;
                }
                else if (caveGuyViewportPosition.x >= 0.75f || model.FatherTransform.position.x > LastCaveGuyXPositionRight) // Same as above
                {
                    FollowTargetX = true;
                }
            }

            if (FollowTargetX) // apply the new x transformation
            {
                x = Mathf.MoveTowards(CameraTransform.position.x, model.FatherTransform.position.x + offsetX, SmoothTimeX);
            }

            if (FollowTargetY) // apply the new y transformation
            {
                y = Mathf.MoveTowards(CameraTransform.position.y, NewY, SmoothTimeY);
            }


            CameraTransform.position = new Vector3(x, y, CameraTransform.position.z); // finally, update the cameras position regardless
        }
        #endregion

        #region Camera Transformation Methods 
        public void SetCameraOffset(float? x, float? y)
        {
            Offset = new Vector2(x ?? Offset.x, y ?? Offset.y);
        }

        public void SetYPosition(FatherModel model, Vector2 relativeTo)
        {
            NewY = relativeTo.y + Offset.y;

            if (UnfollowYCouroutine != null)
                StopCoroutine(UnfollowYCouroutine);

            UnfollowYCouroutine = StartCoroutine(UnfollowYAfterReset());
        }

        private IEnumerator UnfollowYAfterReset()
        {
            yield return new WaitForFixedUpdate();

            while (CameraTransform.position.y != NewY)
            {
                FollowTargetY = true;
                yield return null;
            }
            FollowTargetY = false;
        }

        public void SetCameraFollowY(bool choice)
        {
            FollowTargetY = choice;
        }
        #endregion

    }
}
