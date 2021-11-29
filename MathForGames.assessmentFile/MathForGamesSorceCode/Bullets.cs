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

        /// <summary>
        /// empty cunstructor for the bullet
        /// </summary>
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
        public Bullet(Vector3 posistion, float velocityX, float velocityZ, float speed, string name = "Bullet", Shape shape = Shape.CUBE)
            : base(posistion, name)
        {
            _speed = speed;
            _velocity.X = velocityX;
            _velocity.Z = velocityZ;


        }

        /// <summary>
        /// Updates bullets
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="currentScene"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            //moves the bullet based on the velocity
            _moveDirection = new Vector3(Velocity.X, Velocity.Y, Velocity.Z);

            //then makes velocity = to the movediection times speed and deltaTIme...
            _velocity = _moveDirection.Normalized * Speed * deltaTime;

            //Then adds the localPosistion to the velocty...
            LocalPosistion += _velocity;

            //updtates 
            base.Update(deltaTime, currentScene);
            //meakse timer adds deltaTime to it
            _timer += deltaTime;

            //if the timer is greater than 3
            if (_timer >= 3)
            {
                //remove the actor
                currentScene.RemoveActor(this);
                //sets timer to 0 to reset
                _timer = 0;
            }


        }

        /// <summary>
        /// gives oncollision information
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="currentScene"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            //if actor hits the enemy
            if (actor is Enemey)
            {
                //remove that actor
                currentScene.RemoveActor(this);
            }

        }
        /// <summary>
        /// draws the screen
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}