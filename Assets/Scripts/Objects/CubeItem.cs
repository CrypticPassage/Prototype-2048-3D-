using Signals;
using UnityEngine;
using Zenject;

namespace Objects
{
    public class CubeItem : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public int Number;
        public bool IsThrown;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.CompareTag("FrontBorder") || collision.gameObject.CompareTag("CubeItem")) && IsThrown)
            {
                IsThrown = false;
                _signalBus.Fire<SignalThrowedCubeCollissionStart>();
            }
        }
    }
}