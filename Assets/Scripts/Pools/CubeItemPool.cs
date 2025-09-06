using Objects;
using UnityEngine;
using Zenject;

namespace Pools
{
    public class CubeItemPool : MonoMemoryPool<CubeItem>
    {
        protected override void OnSpawned(CubeItem item)
        {
            base.OnSpawned(item);
            
            var rb = item.Rigidbody;

            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(CubeItem item)
        {
            base.OnDespawned(item);

            var rb = item.Rigidbody;

            if (rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            item.transform.rotation = Quaternion.Euler(90f, 90f, 90f);

            item.gameObject.SetActive(false);
        }
    }
}