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

        /// <summary>
        /// The farthest left x position of the collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.LocalPosistion.X + -(_width / 2);
            }
        }

        /// <summary>
        /// The farthest right x posistion of the collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.LocalPosistion.X + _width / 2;
            }
        }

        /// <summary>
        /// The fartheset top y position of the collider
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.LocalPosistion.Y + -(_height / 2);
            }
        }

        /// <summary>
        /// The farthesest bottom y posistion of the collider
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.LocalPosistion.Y + Height / 2;
            }
        }

        /// <summary>
        /// The constructor for AABB collider
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="owner"></param>
        public AABBCollider(float width, float height, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
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
                Top <= other.Bottom)
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
            Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.PINK);
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }
    }
}

