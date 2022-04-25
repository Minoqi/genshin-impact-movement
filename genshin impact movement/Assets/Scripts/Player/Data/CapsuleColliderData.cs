using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CapsuleColliderData
    {
        // Variables
        public CapsuleCollider Collider { get; private set; }
        public Vector3 ColliderCenterInLocalSpace { get; private set; }

        public void Initialize(GameObject obj)
        {
            if (Collider != null)
            {
                return;
            }

            Collider = obj.GetComponent<CapsuleCollider>();
            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}
