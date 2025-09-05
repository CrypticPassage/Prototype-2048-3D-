using UnityEngine;

namespace Services.Impls
{
    public class InputService : MonoBehaviour, IInputService
    {
        public bool IsClickDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool IsClickUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public bool IsClickHeld()
        {
            return Input.GetMouseButton(0);
        }

        public Vector2 GetClickDelta()
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            return new Vector2(x, y);
        }
    }
}