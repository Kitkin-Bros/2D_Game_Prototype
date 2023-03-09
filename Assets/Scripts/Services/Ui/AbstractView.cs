using System;
using UnityEngine;

namespace Services.Ui {
    public abstract class AbstractView : MonoBehaviour {
        
        private const string ContentGoName = "Content";
        private GameObject _content;
        
        private void Start() {
            _content = GameObject.Find(ContentGoName);
            if (_content == null) {
                throw new Exception($"\"Content\" element was not found in the hierarchy for {gameObject.name}");
            }
        }
        
        public void Show() {
            _content.SetActive(true);
        }
        
        public void Hide() {
            _content.SetActive(false);
        }
    }
}