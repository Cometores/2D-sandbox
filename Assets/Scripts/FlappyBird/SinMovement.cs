using UnityEngine;

namespace FlappyBird
{
    public class SinMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed = 3.5f;
        [SerializeField] private float verticalAmplitude = 2f;
        [SerializeField] private float verticalFrequency = 1f;
        [SerializeField] private float chaosStrength = 1f; // насколько сильный шум

        private float _initialY;
        private float _timeOffset;

        private void Start()
        {
            _initialY = transform.position.y;
            _timeOffset = Random.Range(0f, 100f); // чтобы у каждой пилюли была своя траектория
        }

        private void Update()
        {
            float time = Time.time + _timeOffset;

            // PerlinNoise даёт "живое", но не полностью случайное движение
            float noise = Mathf.PerlinNoise(time * verticalFrequency, 0f) - 0.5f;

            Vector3 pos = transform.position;

            // Двигается влево
            pos.x -= horizontalSpeed * Time.deltaTime;

            // Колебания по Y — добавляем шум
            pos.y = _initialY + noise * verticalAmplitude * 2f + Mathf.Sin(time * 5f) * chaosStrength;

            transform.position = pos;
        }
    }
}