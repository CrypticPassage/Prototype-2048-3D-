using Objects;

namespace Signals
{
    public class SignalCubeItemCollisionWithBorder
    {
        public CubeItem CubeItemThatEnteredCollision;
        
        public SignalCubeItemCollisionWithBorder(CubeItem cubeItemThatEnteredCollision)
        {
            CubeItemThatEnteredCollision = cubeItemThatEnteredCollision;
        }
    }
}