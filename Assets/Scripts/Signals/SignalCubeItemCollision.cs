using Objects;
using UnityEngine;

namespace Signals
{
    public class SignalCubeItemCollision
    {
        public CubeItem CubeItemThatEnteredCollision;
        public Collision Collision;

        public SignalCubeItemCollision(CubeItem cubeItemThatEnteredCollision, Collision collision)
        {
            CubeItemThatEnteredCollision = cubeItemThatEnteredCollision;
            Collision = collision;
        }
    }
}