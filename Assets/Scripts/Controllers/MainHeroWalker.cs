using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class MainHeroWalker : MonoBehaviour
    {
        private const string _verticalAxisName = "Vertical";
        private const string _horizontalAxisName = "Horizontal";

        private const float _animationsSpeed = 10;
        private const float _walkSpeed = 150;
        private const float _jumpForse = 350;
        private const float _jumpThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _movingThresh = 0.1f;

        private bool _doJump;
        private float _goSideWay = 0;

        private readonly LevelObjectView _view;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly ContactsPoller _contactsPoller;

        public MainHeroWalker(LevelObjectView view, SpriteAnimator
            spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _contactsPoller = new ContactsPoller(_view._collider);
        }

        public void Update()
        {
            _doJump = Input.GetAxis(_verticalAxisName) > 0;
            _goSideWay = Input.GetAxis(_horizontalAxisName);
            _contactsPoller.Update();

            var walks = Mathf.Abs(_goSideWay) > _movingThresh;

            if (walks) _view.SpriteRenderer.flipX = _goSideWay < 0;
            var newVelocity = 0f;
            if (walks &&
                (_goSideWay > 0 || !_contactsPoller.HasLeftContacts) &&
                (_goSideWay < 0 || !_contactsPoller.HasRightContacts))
            {
                newVelocity = Time.fixedDeltaTime * _walkSpeed *
                   (_goSideWay < 0 ? -1 : 1);
            }
            _view._rigidbody.velocity = _view._rigidbody.velocity.Change(
                 x: newVelocity);
            if (_contactsPoller.IsGrounded && _doJump &&
                  Mathf.Abs(_view._rigidbody.velocity.y) <= _jumpThresh)
            {
                _view._rigidbody.AddForce(Vector3.up * _jumpForse);
            }

            //animations
            if (_contactsPoller.IsGrounded)
            {
                var track = walks ? Track.walk : Track.idle;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true,
                    _animationsSpeed);
            }
            else if (Mathf.Abs(_view._rigidbody.velocity.y) > _flyThresh)
            {
                var track = Track.jump;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true,
                    _animationsSpeed);
            }
        }

    }
}