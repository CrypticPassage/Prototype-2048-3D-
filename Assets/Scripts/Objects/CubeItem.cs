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
            _signalBus.Fire(new SignalCubeItemCollision(this, collision));
        }
    }
}