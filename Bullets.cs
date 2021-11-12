using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;
using System.Diagnostics;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private float _collisionRaidus;
        private Scene _scene;
        private float _timer = 0;

        public float Speed
        {
            get { return _speed; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Bullet()
        {

        }

        /// <summary>
        /// is the construct of bullet
        /// </summary>
        /// <param name="Icon">The </param>
        /// <param name="color"></param>
        /// <param name="posistion"></param>
        /// <param name="speed"></param>
        /// <param name="xDirection"></param>
        /// <param name="CollisionRadius"></param>
        /// <param name="yDirection"></param>
        /// <param name="name"></param>
        public Bullet(Vector3 posistion, float velocityX, float velocityZ, float speed,string name = "Bullet", Shape shape = Shape.CUBE)
            : base(posistion, name)
        {
            _speed = speed;
            _velocity.X = velocityX;
            _velocity.Z = velocityZ;


        }

        public override void Update(float deltaTime, Scene currentScene)
        {

            _moveDirection = new Vector3(Velocity.X, Velocity.Y, Velocity.Z);

            _velocity = _moveDirection.Normalized * Speed * deltaTime;

            LocalPosistion += _velocity;

            base.Update(deltaTime, currentScene);

            _timer += deltaTime;

            
            //if (_timer >= 3)
                //currentScene.RemoveActor(this);

        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemey)
            {
                currentScene.RemoveActor(this);
            }
        }
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
