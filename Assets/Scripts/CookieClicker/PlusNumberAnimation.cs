using TMPro;
using UnityEngine;

namespace CookieClicker
{
    public class PlusNumberAnimation : MonoBehaviour
    {
        private bool _isTransp;
        private float _transp = 1;
        [SerializeField] private float moveSpeed = 20;
        private TextMeshProUGUI _txt;

        private void Start()
        {
            Invoke(nameof(SetTransp), 0.5f);
            _txt = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            transform.position += (Vector3.up * (Time.deltaTime * moveSpeed));
            if (_isTransp)
            {
                _transp -= Time.deltaTime;
                _txt.color = new Color(1, 1, 1, _transp);
            }
            if (_transp <= 0)
                Destroy(gameObject);
        }


        private void SetTransp()
        {
            _isTransp = true;
        }

    }
}
