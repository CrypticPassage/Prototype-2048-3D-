using Objects;

namespace Services
{
    public interface ICubeItemsInteractService
    {
        void MergeCubeItems(CubeItem firstCubeItem, CubeItem secondCubeItem, float forceImpact);
    }
}