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
        private int _health;
        private float _timer = 0;
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

        //a player holder contructor for player
        public Player()
        {

        }

        //a constructor for player
        public Player(float x, float y, float z, float speed, int health, string name = "Player", Shape shape = Shape.CUBE)
            : base(x, y, z, speed, name, shape)
        {
            _speed = speed;
            Health = health;
        }


        public override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Updates the players infromation on the screen and console.
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            //get the player input direction
            int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
            + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int zDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            int rotatingActor = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE));

            //rotates the player
            Rotate(0, 10 * deltaTime, 0);

            //is the float that scales the player
            float _scaleUp = .50f;
            float _scaleDown = 1.50f;

            //if you press z 
            if (Raylib.IsKeyDown(KeyboardKey.KEY_Z))
                //scale up
                Scale(_scaleUp, _scaleUp, _scaleUp);
            //if you press x
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_X))
                //scale down
                Scale(_scaleDown, _scaleDown, _scaleDown);


            //gets the palyers input direction for the shoot by using arrow key
            int xDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                   + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int zDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            _timer += deltaTime;

            //takes in a direction and set sets a timer
            //if cooldowntimer is less than .05 then spawn if not then no spawn
            if ((xDirectionBullet != 0 && _timer >= .5 || zDirectionBullet != 0 && _timer >= .5))
            {
                //the bullet instence
                //changed the posision to localPosistion
                Bullet bullet = new Bullet(LocalPosistion, xDirectionBullet, zDirectionBullet, 40, "Bullet", Shape.SPHERE);
                bullet.SetScale(1, .5f, 1);
                bullet.SetColor(new Vector4(16, 45, 19, 255));
                currentScene.AddActor(bullet);

                //spawns the collider
                CircleCollider BulletCollider = new CircleCollider(1, bullet);
                //sets the collider
                bullet.Collider = BulletCollider;

                //sets timer back to 0
                _timer = 0;
            }

            //Create a vector tht stores the move input
            Vector3 moveDirection = new Vector3(xDiretion, 0, zDiretion);

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;
            //addes the velocity to the localPosistion
            LocalPosistion += Velocity;

            //update
            base.Update(deltaTime, currentScene);

        }

        /// <summary>
        /// uses the collider on the current actor
        /// </summary>
        /// <param name="actor"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            //if actor is touched by teh enenmy end the game
            if (actor is Enemey && _timer > 1)
            {
                //decrement health
                Health--;

                //when you collide go orange
                SetColor(Color.ORANGE);
                //closes window when player dies
                if (Health <= 0)
                {
                    Engine.CloseApplication();
                }

                //reset time
                _timer = 0;
            }

        }

        /// <summary>
        /// draws the the scene.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}