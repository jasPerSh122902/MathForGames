using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        public float _height;
        public float _lenght;

        /// <summary>
        /// The size of ths collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of this collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Lenght
        {
            get { return _lenght; }
            set { _lenght = value; }
        }

        /// <summary>
        /// The farthest left x position of the collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.LocalPosistion.X + -(Width / 2);
            }
        }

        /// <summary>
        /// The farthest right x posistion of the collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.LocalPosistion.X + (Width / 2);
            }
        }

        /// <summary>
        /// The fartheset top y position of the collider
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.LocalPosistion.Y  + -(Height / 2);
            }
        }

        /// <summary>
        /// The farthesest bottom y posistion of the collider
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.LocalPosistion.Y + (Height / 2);
            }
        }

        public float Front
        {
            get
            {
                return Owner.LocalPosistion.Y + -(Lenght / 2);
            }
        }

        public float Back
        {
            get
            {
                return Owner.LocalPosistion.Y + (Lenght / 2);
            }
        }

        /// <summary>
        /// The constructor for AABB collider
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="owner"></param>
        public AABBCollider(float width, float height,float length, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            _lenght = length;
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Return false if this owner is checking for a collision against itself
            if (other.Owner == Owner)
                return false;

            //This checkes each oppossit side is lest than a nother (normaly the first two are there for the Other or second object is less than the main object...
            //... The last two is to check if the main object is lest than the second object.
            //Return true if There is an overlap between boxes.
            if (other.Left <= Right &&
               other.Top <= Bottom &&
               Left <= other.Right &&
               Top <= other.Bottom &&
               other.Front <= Back &&
               Front <= other.Back)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Is the draw function
        /// </summary>
        public override void Draw()
        {
            //is meant to draw the collider that is the rectangle
            Raylib.DrawCube(new System.Numerics.Vector3(Owner.WorldPosistion.X, Owner.WorldPosistion.Y, Owner.WorldPosistion.Z), 5, 5, 5, Color.BROWN);
        }

        public override void Update()
        {
            _height = Owner.Size.X;
            _width = Owner.Size.Y;
            _lenght = Owner.Size.Z;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }
    }
}

