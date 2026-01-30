using UnityEngine;

namespace ProjectTetrad.PlayerFeatures
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] Transform target;

        [SerializeField] Vector3 minBounds = new Vector3(-45f, 0f, -45f);
        [SerializeField] Vector3 maxBounds = new Vector3(45f, 0f, 45f);

        bool isTransitioning;

        void LateUpdate()
        {
            if (isTransitioning)
            {
                TransitionUpdate();
            }
            else
            {
                NormalUpdate();
            }
        }

        void NormalUpdate()
        {
            if (target != null)
            {
                float x = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
                float y = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);
                float z = Mathf.Clamp(target.position.z, minBounds.z, maxBounds.z);

                transform.position = new Vector3(x, y, z);
            }
        }

        void TransitionUpdate()
        {

        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void SetBounds(Vector3 min, Vector3 max)
        {
            minBounds = min;
            maxBounds = max;
        }

        public void SetTranstioning()
        {
            isTransitioning = true;
        }
    }
}
