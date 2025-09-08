using Objects;

namespace Signals
{
    public class SignalCubeItemMerged
    {
        public CubeItem FirstCubeItem;
        public CubeItem SecondCubeItem;

        public SignalCubeItemMerged(CubeItem firstCubeItem, CubeItem secondCubeItem)
        {
            FirstCubeItem = firstCubeItem;
            SecondCubeItem = secondCubeItem;
        }
    }
}