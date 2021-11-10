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

        public Player()
        {

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
        public override void Update(float deltaTime, Scene currentScene)
        {


            if (Name == "Player1")
            {
                //get the player input direction
                int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
                int zDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));



                float _scaleUp = .50f;
                float _scaleDown = 1.50f;

                if (Raylib.IsKeyDown(KeyboardKey.KEY_Z))
                    Scale(_scaleUp, _scaleUp, _scaleUp);

                else if (Raylib.IsKeyDown(KeyboardKey.KEY_X))
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
                    bullet.SetColor(new Vector4(16, 23, 19, 255));
                    currentScene.AddActor(bullet);

                    //spawns the collider
                    CircleCollider BulletCollider = new CircleCollider(1, bullet);
                    //sets the collider
                    bullet.Collider = BulletCollider;


                    _timer = 0;
                }

                //Create a vector tht stores the move input
                Vector3 moveDirection = new Vector3(xDiretion, 0, zDiretion);

                //caculates the veclocity 
                Velocity = moveDirection.Normalized * Speed * deltaTime;

                LocalPosistion += Velocity;

                base.Update(deltaTime, currentScene);
            }

            if (Name == "Player2")
            {
                //get the player input direction
                int xDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_I))
                    + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_P));
                int zDiretion = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_O))
                    + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_L));



                float _scaleUp = .50f;
                float _scaleDown = 1.50f;

                if (Raylib.IsKeyDown(KeyboardKey.KEY_M))
                    Scale(_scaleUp, _scaleUp, _scaleUp);

                else if (Raylib.IsKeyDown(KeyboardKey.KEY_N))
                    Scale(_scaleDown, _scaleDown, _scaleDown);


                //gets the palyers input direction for the shoot by using arrow key
                int xDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_KP_4))
                       + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_KP_6));
                int zDirectionBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_KP_8))
                    + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_KP_2));

                _timer += deltaTime;

                //takes in a direction and set sets a timer
                //if cooldowntimer is less than .05 then spawn if not then no spawn
                if ((xDirectionBullet != 0 && _timer >= .5 || zDirectionBullet != 0 && _timer >= .5))
                {
                    //the bullet instence
                    //changed the posision to localPosistion
                    Bullet bullet = new Bullet(LocalPosistion, xDirectionBullet, zDirectionBullet, 40, "Bullet", Shape.SPHERE);
                    bullet.SetScale(.5f, .5f, .5f);
                    bullet.SetColor(new Vector4(16, 23, 19, 255));
                    currentScene.AddActor(bullet);

                    //spawns the collider
                    CircleCollider BulletCollider = new CircleCollider(1, bullet);
                    //sets the collider
                    bullet.Collider = BulletCollider;


                    _timer = 0;
                }

                //Create a vector tht stores the move input
                Vector3 moveDirection = new Vector3(xDiretion, 0, zDiretion);

                //caculates the veclocity 
                Velocity = moveDirection.Normalized * Speed * deltaTime;

                LocalPosistion += Velocity;

                base.Update(deltaTime, currentScene);
            }

        }

        /// <summary>
        /// uses the collider on the current actor
        /// </summary>
        /// <param name="actor"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            //if actor is touched by teh enenmy end the game
            if (actor is Enemey)
            {
                _velocity *= .50f;
                LocalPosistion += _velocity;
                _health--;
            }

            if (actor is Bullet)
            {
                _health--;
                _velocity *= .50f;
                LocalPosistion += _velocity;
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

