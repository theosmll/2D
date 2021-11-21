using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        public Collider2D _collider;
        public Rigidbody2D _rigidbody;
        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var levelObject = collider.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }

    }

}