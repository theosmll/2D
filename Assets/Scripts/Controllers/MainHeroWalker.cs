using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class MainHeroWalker : MonoBehaviour
    {
        private const float _walkSpeed = 3f;
        private const float _animationsSpeed = 10f;
        private const float _jumpStartSpeed = 8f;
        private const float _movingThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _groundLevel = 0.5f;
        private const float _g = -10f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private bool _doJump;
        private float _xAxisInput;

        private LevelObjectView _view;
        private SpriteAnimator _spriteAnimator;

        public MainHeroWalker(LevelObjectView view, SpriteAnimator spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
        }

        public void Update()
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if (IsGrounded())
            {
                
                if (goSideWay) GoSideWay();
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, goSideWay ? Track.walk : Track.idle, true, _animationsSpeed);

                
                if (_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                
                else if (_yVelocity < 0)
                {
                    _yVelocity = float.Epsilon;
                    _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                
                if (goSideWay) GoSideWay();
                if (Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.jump, true, _animationsSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }

        private void GoSideWay()
        {
            _view.Transform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view.Transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public bool IsGrounded()
        {
            return _view.Transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }
    }
}