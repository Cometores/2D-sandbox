using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird.Enemies.Movement
{
    public class SinMovement : MonoBehaviour
    {
        [Header("Movement Settings")] 
        [SerializeField] private float speed = 3;

        [Header("Vertical Motion")] 
        [SerializeField] private float amplitude = 2f;
        [SerializeField] private float frequency = 1f;
        [SerializeField] private float chaos = 1f;

        [Header("Rotation")] 
        [SerializeField] private float tiltSmooth = 5f;

        [Header("VFX")] 
        [SerializeField] private GameObject deathVFX;
        
        private float _timeOffset;
        private Vector3 _startPos;
        private Vector3 _prevPos;

        private void Start()
        {
            _timeOffset = Random.Range(0f, 100f);
            _startPos = transform.position;
            _prevPos = _startPos;
        }

        private void Update()
        {
            Move();
            RotateTowardsDirection();
        }

        public void Die()
        {
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


        private void Move()
        {
            float t = Time.time + _timeOffset;

            float vertical = Mathf.PerlinNoise(t * frequency, 0f) - 0.5f; // плавная "рваность"
            vertical *= amplitude * 2f;
            vertical += Mathf.Sin(t * 5f) * chaos; // добавляем хаос-синус

            Vector3 pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            pos.y = _startPos.y + vertical;

            transform.position = pos;
        }

        private void RotateTowardsDirection()
        {
            if (Time.timeScale == 0f) return;
            
            Vector3 velocity = (transform.position - _prevPos) / Time.deltaTime;
            _prevPos = transform.position;

            if (velocity.sqrMagnitude < 0.0001f) return;

            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + 180f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSmooth * Time.deltaTime);
        }
    }
}