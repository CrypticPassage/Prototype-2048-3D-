using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        bool IsClickDown();
        bool IsClickUp();
        bool IsClickHeld();
        Vector2 GetClickDelta();
    }
}