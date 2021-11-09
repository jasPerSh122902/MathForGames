using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibaray;

namespace MathForGames
{
    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {

        private string _name;
        private Vector2 _localPosistion;
        //made started a bool so we can see if actors is there or not.
        private bool _started;
        private float _speed;
        private bool _drawLines;


        private Vector3 _forward = new Vector3(0, 0, 1);
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _LocalTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;

        private Collider _coollider;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Shape _shape;
        private Color _color;

        /// <summary>
        /// returns the color
        /// </summary>
        public Color ShapeColor
        {
            get { return _color; }
        }

        //starts the actor
        public bool Started
        {
            get { return _started; }
        }

        //sets the name for the actor
        public string Name
        {
            get { return _name; }
        }

        //sets the speed
        public float Speed
        {
            get { return _speed; }
        }

        /// <summary>
        /// Returns the new Vector3 for the localPosistion and sets the Translation with the values of x, y , and Z.
        /// </summary>
        public Vector3 LocalPosistion
        {
            //takes in a posisition on the matrix...
            get { return new Vector3(_translation.M03, _translation.M13, _translation.M23); }
            set
            {
                //set that posistion on the matrix
                SetTranslation(value.X, value.Y, value.Z);
            }
        }

        /// <summary>
        /// The posistion of theis actor in the world
        /// </summary>
        public Vector3 WorldPosistion
        {
            //returns the globaal transform's T column
            get { return new Vector3(_globalTransform.M03, _globalTransform.M13, _globalTransform.M23); }
            set
            {
                //if the actor has a parent...
                if (Parent != null)
                {
                    //... convert the world coordinates into loval coooridinates and translate the actor
                    //needs the Z axis
                    float xoffset = (value.X - Parent.WorldPosistion.X) / new Vector3(_globalTransform.M00, _globalTransform.M10, _globalTransform.M20).Magnitude;
                    float yoffset = (value.Y - Parent.WorldPosistion.Y) / new Vector3(_globalTransform.M01, _globalTransform.M11, _globalTransform.M21).Magnitude;
                    float zoffset = (value.Z - Parent.WorldPosistion.Z) / new Vector3(_globalTransform.M02, _globalTransform.M12, _globalTransform.M22).Magnitude;
                    SetTranslation(xoffset, yoffset, zoffset);
                }
                //if theis actor doesn't have a parent
                else
                    //...sets the  local posisiton to be the given value
                    LocalPosistion = value; //set that posistion on the matrix


            }
        }
        /// <summary>
        /// The world transform meant for bigger movement.
        /// </summary>
        public Matrix4 GolbalTransform
        {
            get { return _globalTransform; }
            set { _globalTransform = value; }
        }

        /// <summary>
        /// The small or naborhood transform meant for smaller movement.
        /// </summary>
        public Matrix4 LocalTransform
        {
            get { return _LocalTransform; }
            set { _LocalTransform = value; }
        }
        /// <summary>
        /// is the parent for actor
        /// </summary>
        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// is the child for actor
        /// </summary>
        public Actor[] Children
        {
            get { return _children; }
        }
        /// <summary>
        /// Sizes the caractor or actor
        /// </summary>
        public Vector3 Size
        {
            get
            {
                //sets the x and y scale
                float xScale = new Vector3(GolbalTransform.M00, GolbalTransform.M10, GolbalTransform.M20).Magnitude;
                float yScale = new Vector3(GolbalTransform.M01, GolbalTransform.M11, GolbalTransform.M21).Magnitude;
                float zScale = new Vector3(GolbalTransform.M02, GolbalTransform.M12, GolbalTransform.M22).Magnitude;

                //returns the x and y
                return new Vector3(xScale, yScale, zScale);
            }
            set { SetScale(value.X, value.Y, value.Z); }
        }

        /// <summary>
        /// Is meant to indicate where the front of the actor is.
        /// </summary>
        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }
            //need a vector 3 set for forward
        }

        /// <summary>
        /// IS the collider for the actor 
        /// </summary>
        public Collider Collider
        {
            get { return _coollider; }
            set { _coollider = value; }
        }

        //emptyiy actor class
        public Actor() { }

        /// <summary>
        /// takes the Actor constructor and add the float x and y but takes out y
        /// </summary>
        /// <param name="x">is the replace the Vector2</param>
        /// <param name="y">is the replacement for the veoctor2</param>
        public Actor(float x, float y, float z, float speed, string name = "Actor", Shape shape = Shape.CUBE) :
            this(new Vector3 { X = x, Y = y, Z = z }, name, shape)
        { }




        /// <summary>
        /// Is a constructor for the actor that hold is definition.
        /// </summary>
        /// <param name="icon">The icon that all this information applies to</param>
        /// <param name="position">is the loctation that the icon is in</param>
        /// <param name="name">current Actor name</param>
        /// <param name="color">The color that the neame or icon will be</param>
        public Actor(Vector3 position, string name = "Actor", Shape shape = Shape.CUBE)
        {
            //updatede the Icon with the struct and made it take a symbol and a color
            LocalPosistion = position;
            _name = name;
            _shape = shape;

        }

        /// <summary>
        /// Updates the current transform (the scene).
        /// </summary>
        public void UpdateTransform()
        {
            _LocalTransform = _translation * _rotation * _scale;

            if (Parent != null)
                GolbalTransform = Parent.GolbalTransform * LocalTransform;
            else
                GolbalTransform = LocalTransform;

        }

        /// <summary>
        /// Adds the child to the scene
        /// </summary>
        /// <param name="child">is a array</param>
        public void AddChild(Actor child)
        {
            //makes a new array called temArray and mades it the lengh of actors + a nother spot
            Actor[] temArray = new Actor[_children.Length + 1];

            //incremens through the actors array
            for (int i = 0; i < _children.Length; i++)
            {
                temArray[i] = _children[i];
            }

            //sets temArray to the actors array and set it to actor
            temArray[_children.Length] = child;

            child.Parent = this;

            //then sets actors to temarray
            _children = temArray;


        }

        /// <summary>
        /// Removes child from the scene
        /// </summary>
        /// <param name="child">Is a array</param>
        /// <returns>true or false</returns>
        public bool RemoveChild(Actor child)
        {
            //create a varialbe to store if the removal was successful
            bool childRemoved = false;

            //created a new array that is small than the original array.
            Actor[] temArray = new Actor[_children.Length - 1];

            //is there to the second array and not have space from removed child.
            int j = 0;

            //incremens through the temArray
            for (int i = 0; i < temArray.Length; i++)
            {
                if (i > temArray.Length)
                    i--;

                //sais that if actor is not equal to the child that is choosen then dont go into but..
                if (_children[i] != child)
                {
                    //make temArray have j and make it equal to child with i so there is no left over space in the array.
                    temArray[j] = _children[i];
                    //increment j
                    j++;
                }
                //if none of that is needed return true.
                else
                    childRemoved = true;

            }

            //will only happen if the child is being removed and will the set actors with temArray.
            if (childRemoved)
            {
                //sets the children array to the now decremented temarray.
                _children = temArray;
                //makes sure that the child knows it has no parent
                Parent = null;
            }


            //...then returns
            return childRemoved;
        }

        /// <summary>
        /// Is the start of the actor
        /// </summary>
        public virtual void Start()
        {
            _started = true;
        }

        /// <summary>
        /// Updtated the position for the actor
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {

            UpdateTransform();
            Console.WriteLine(_name + ":" + WorldPosistion.X + ":" + WorldPosistion.Y);
        }

        /// <summary>
        /// Draw the actor and draws the collision for actors.
        /// </summary>
        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosistion.X, WorldPosistion.Y, WorldPosistion.Z);

            System.Numerics.Vector3 startPos = new System.Numerics.Vector3(WorldPosistion.X, WorldPosistion.Y, WorldPosistion.Z);
            System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosistion.X + Forward.X * 10, WorldPosistion.Y + Forward.Y * 10, WorldPosistion.Z + Forward.Z * 10);

            switch (_shape)
            {
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.X, Size.Y, Size.Z, ShapeColor);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.X, ShapeColor);
                    break;
            }

        }


        /// <summary>
        /// The end for actor
        /// </summary>
        public void End()
        {

        }

        /// <summary>
        /// Startes when the player hits a target.
        /// </summary>
        public virtual void OnCollision(Actor actor)
        {

        }

        /// <summary>
        /// Checks if theis actor collided with anoth actor
        /// </summary>
        /// <param name="other">The actor to check collision with</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Returns false if eithe actor dosen't have a collider.
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);


        }
        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY, float translationZ)
        {

            _translation = Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the yparam>
        public void Translate(float translationX, float translationY, float translationZ)
        {

            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Set the rotation of the actor.
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radiansX, float radiansY, float radiansZ)
        {

            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);

            _rotation = rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Adds a roation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);

            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Sets the scale of the actor.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Roatates the actor to face the given position
        /// </summary>
        /// <param name="position">The posistion the actor should be looking toward</param>
        public void LookAt(Vector3 position)
        {
            //got the direction the actor should look in
            Vector3 direction = (position - WorldPosistion).Normalized;

            //if the direciton has a length of Zero...
            if (direction.Magnitude == 0)
                //...set it to be the default forward
                direction = new Vector3(0, 0, 1);

            //A new constent vector 3 that is just up
            Vector3 alignAxis = new Vector3(0, 1, 0);

            //new Y axis as a Vector3
            Vector3 newYAxis = new Vector3(0, 1, 0);
            //new X axis as a Vector3
            Vector3 newXAxis = new Vector3(1, 0, 0);

            //if the direction vector is parallel to the alignAxis vector...
            if (Math.Abs(direction.Y) > 0 && direction.X == 0 && direction.Z == 0)
            {
                //...set the slignAxis vector to point ot the right
                alignAxis = new Vector3(1, 0, 0);

                //Gets the cross product of the direciton and the right to find the new y axis
                newYAxis = Vector3.CrossProduct(direction, alignAxis);
                //normalizes the distince to prevent the matrix from being scaled
                newYAxis.Normalize();

                // Gets the cross product of the newYAxis and the direction to find a new X axis
                newXAxis = Vector3.CrossProduct(newYAxis, direction);
                //normalizes the distince to prevent the matrix from being scaled
                newXAxis.Normalize();

            }
            //if it is not parellel
            else
            {

                // Gets the cross product of the alignAxis and the direction to find a new X axis
                newXAxis = Vector3.CrossProduct(alignAxis, direction);
                //normalizes the distince to prevent the matrix from being scaled
                newXAxis.Normalize();
                // Gets the cross product of the direction and the newXAxis to find a new X axis
                newYAxis = Vector3.CrossProduct(direction, newXAxis);
                //normalizes the distince to prevent the matrix from being scaled
                newYAxis.Normalize();
            }

            //rotaties the curretn Matrix4 on all values other than the W
            _rotation = new Matrix4(newXAxis.X, newYAxis.X, direction.X, 0,
                                    newXAxis.Y, newYAxis.Y, direction.Y, 0,
                                    newXAxis.Z, newYAxis.Z, direction.Z, 0,
                                    0, 0, 0, 1);

        }

        //sets the color based on the raylibs color class
        public void SetColor(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// is the overloat for SetColor
        /// </summary>
        /// <param name="colorValue"></param>
        public void SetColor (Vector4 colorValue)
        {
            // the x is red, y is green, Z is brown, W is alfa
            _color = new Color((int)colorValue.X, (int)colorValue.Y, (int)colorValue.Z, (int)colorValue.W);
        }

    }
}