using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;
using System.Threading;
using System.Diagnostics;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private int _health = 5;
        private float _cooldownTimer;
        private bool _ifTimeTrue;
        private Vector3 _velocity;
        public Scene _scene;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public Player(float x, float y, float z, float speed, string name = "Player", Shape shape = Shape.CUBE)
            : base(x, y, z, speed, name, shape)
        {
            _speed = speed;
        }


        public override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Updates the players infromation on the screen and console.
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {

            _cooldownTimer += deltaTime;

            //get the player input direction
            int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int zDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            int yDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE));
                

            //Create a vector tht stores the move input
            Vector3 moveDirection = new Vector3(xDiretion, yDiretion , zDiretion);



            //gets the palyers input direction for the shoot by using arrow key
            int xDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                   + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int yDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_P));
            int zDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));


            //caculates the veclocity 
            Velocity = moveDirection * Speed * deltaTime;

            LocalPosistion += Velocity;
            base.Update(deltaTime);

            //takes ina direction and set sets a timer
            //if cooldowntimer is less than .05 then spawn if not then no spawn
            if ((xDirectionBullet != 0  || yDirectionBullet != 0  || zDirectionBullet != 0 ))
            {
                //the bullet instence
                //changed the posision to localPosistion
                Bullet bullet = new Bullet(LocalPosistion, 100, xDirectionBullet, zDirectionBullet, yDirectionBullet, "Bullet", Shape.SPHERE);

                //spawns the collider
                CircleCollider BulletCollider = new CircleCollider(1, bullet);
                //sets the collider
                bullet.Collider = BulletCollider;
                //addes the actor bullet to the scene
                //error of null
                _scene.AddActor(bullet);


            }
        }

        /// <summary>
        /// uses the collider on the current actor
        /// </summary>
        /// <param name="actor"></param>
        public override void OnCollision(Actor actor)
        {
            //if actor is touched by teh enenmy end the game
            if (actor is Enemey)
            {

            }
        }

        /// <summary>
        /// draws the the scene.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }
    }
}

