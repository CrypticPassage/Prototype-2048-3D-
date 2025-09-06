using Objects;

namespace Services
{
    public interface ICubeItemsInteractService
    {
        void MergeCubeItems(CubeItem firstCube, CubeItem secondCube, float forceImpact);
    }
}