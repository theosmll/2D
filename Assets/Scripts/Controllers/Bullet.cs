using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Bullet : MonoBehaviour
    {
        private BulletView _view;

        public Bullet(BulletView view)
        {
            _view = view;
            _view.SetVisible(false);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view.SetVisible(false);
            _view.Transform.position = position;
            _view._rigidbody.velocity = Vector2.zero;
            _view._rigidbody.angularVelocity = 0;
            _view._rigidbody.AddForce(velocity, ForceMode2D.Impulse);
            _view.SetVisible(true);
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}