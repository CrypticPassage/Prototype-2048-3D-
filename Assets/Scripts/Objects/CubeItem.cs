using Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Objects
{
    public class CubeItem : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private MeshRenderer cubeRenderer;
        [SerializeField] private TMP_Text[] numbersTexts;
        
        private SignalBus _signalBus;
        private int _number;
        private bool _isThrown;

        public Rigidbody Rigidbody => rigidbody;
        public int Number => _number;
        
        public bool IsThrown
        {
            get => _isThrown;
            set => _isThrown = value;
        }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void SetData(int number, Color color)
        {
            _number = number;
            cubeRenderer.material.color = color;

            foreach (var text in numbersTexts)
                text.text = number.ToString();
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("FrontBorder"))
                _signalBus.Fire(new SignalCubeItemCollisionWithBorder(this));

            if (collision.gameObject.CompareTag("CubeItem"))
            {
                var otherCubeItem = collision.collider.GetComponent<CubeItem>();

                if (otherCubeItem == null)
                    return;
                
                var impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;
                
                _signalBus.Fire(new SignalCubeItemCollisionWithOtherCubeItem(this, otherCubeItem, impactForce));
            }
        }
    }
}