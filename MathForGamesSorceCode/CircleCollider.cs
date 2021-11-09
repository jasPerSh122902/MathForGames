using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;
using Raylib_cs;

namespace MathForGames
{
    class CircleCollider : Collider
    {
        private float _coolisionRadius;

        public float CollisionRadius
        {
            get { return _coolisionRadius; }
            set { _coolisionRadius = value; }
        }

        public CircleCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.CIRCLE)
        {
            _coolisionRadius = collisionRadius;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            if (other.Owner == Owner)
                return false;

            //fings the distance between the two actors
            float distance = Vector3.Distance(other.Owner.LocalPosistion, Owner.LocalPosistion);
            //Find the length of the radii combined
            float combinedRadii = other.CollisionRadius + CollisionRadius;


            return combinedRadii >= distance;

        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //returns false if the owners collision is hitting it self
            if (other.Owner == Owner)
                return false;


            //gets the direction from thsi collier to the AABB
            Vector3 direction = Owner.LocalPosistion - other.Owner.LocalPosistion;

            //clamp the direciton vector to be within the bounds of the AABB
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);

            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);

            //add the direction vector to the AABB centrer to get the closest point to the circle
            Vector3 closestPoint = other.Owner.LocalPosistion + direction;

            //find the diestance from the circles center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.LocalPosistion, closestPoint);

            //returns whether or not the diestace is less than the circles radius
            return distanceFromClosestPoint <= CollisionRadius;
        }

        public override void Draw()
        {
            base.Draw();
            Raylib.DrawCircleLines((int)Owner.LocalPosistion.X, (int)Owner.LocalPosistion.Y, CollisionRadius, Color.GOLD);
        }
    }
}
