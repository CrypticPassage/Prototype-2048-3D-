using Objects;

namespace Signals
{
    public class SignalCubeItemCollisionWithOtherCubeItem
    {
        public CubeItem CubeItemThatEnteredCollision;
        public CubeItem OtherCubeItem;
        public float ImpactForce;
        
        public SignalCubeItemCollisionWithOtherCubeItem(CubeItem cubeItemThatEnteredCollision, CubeItem otherCubeItem, float impactForce)
        {
            CubeItemThatEnteredCollision = cubeItemThatEnteredCollision;
            OtherCubeItem = otherCubeItem;
            ImpactForce = impactForce;
        }
    }
}