using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibaray
{
    public struct Matrix3
    {
        public float M00, M01, M02, M10, M11, M12, M20, M21, M22;

        //The basic constrctor of the Matrix3
        public Matrix3(float m00, float m01, float m02,
                       float m10, float m11, float m12,
                       float m20, float m21, float m22)
        {
            //setting each of the M's to there respected M
            M00 = m00; M01 = m01; M02 = m02;
            M10 = m10; M11 = m11; M12 = m12;
            M20 = m20; M21 = m21; M22 = m22;
        }

        //Made the Matrix3 equal the Identity value do i dont have to call constucor over again.
        public static Matrix3 Identity
        {
            get
            {
                //is all of the places in the matrix;
                return new Matrix3(1, 0, 0,
                                   0, 1, 0,
                                   0, 0, 1);
            }
        }

        /// <summary>
        /// Creates a new matrix that has been rotated by the given value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        public static Matrix3 CreateRotation(float radians)
        {
            return new Matrix3((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                               -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                                0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix</param>
        public static Matrix3 CreateTranslation(float x, float y)
        {
            return new Matrix3(1, 0, x,
                               0, 1, y,
                               0, 0, 1);
        }

        public static Matrix3 CreateTranslation(Vector2 Position)
        {
            return new Matrix3(1, 0, Position.X,
                               0, 1, Position.Y,
                               0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix3 CreateScale(float x, float y)
        {
            return new Matrix3(x, 0, 0,
                               0, y, 0,
                               0, 0, 1);
        }

        /// <summary>
        /// Adds the Matrix3
        /// </summary>
        /// <param name="lhs">left hand Matrix</param>
        /// <param name="rhs">Right hand Matrix</param>
        /// <returns>the added Matriexes</returns>
        public static Matrix3 operator +(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02,
                               lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12,
                               lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22);
        }

        /// <summary>
        /// Subtracts the Matrix
        /// </summary>
        /// <param name="lhs">left hand Matrix</param>
        /// <param name="rhs">Right hand Matrix</param>
        /// <returns>returns the subtracted Matriexes</returns>
        public static Matrix3 operator -(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02,
                               lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12,
                               lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(
                (lhs.M00 * rhs.X) + (lhs.M01 * rhs.Y) + (lhs.M02 * rhs.Z),
                (lhs.M10 * rhs.X) + (lhs.M11 * rhs.Y) + (lhs.M12 * rhs.Z),
                (lhs.M20 * rhs.X) + (lhs.M21 * rhs.Y) + (lhs.M22 * rhs.Z)
                );
        }

        /// <summary>
        /// Multiplies the Matrixes but they order will allwayes be the left hand then the right
        /// </summary>
        /// <param name="lhs">Left hand is beeing scaled </param>
        /// <param name="rhs">the right hand is the scaler </param>
        /// <returns>The multipied Matrixes</returns>
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
                (
                    ///The temps multiypy values is rows by coloms
                    ///Row1, Colum1
                    (lhs.M00 * rhs.M00) + (lhs.M01 * rhs.M10) + (lhs.M02 * rhs.M20),
                    //Row1 , column2
                    (lhs.M00 * rhs.M01) + (lhs.M01 * rhs.M11) + (lhs.M02 * rhs.M21),
                    //Row1 , column 3
                    (lhs.M00 * rhs.M02) + (lhs.M01 * rhs.M12) + (lhs.M02 * rhs.M22),

                    //Row2, columns1
                    (lhs.M10 * rhs.M00) + (lhs.M11 * rhs.M10) + (lhs.M12 * rhs.M20),
                    //Row2, columns2
                    (lhs.M10 * rhs.M01) + (lhs.M11 * rhs.M11) + (lhs.M12 * rhs.M21),
                    //Row2, columns3
                    (lhs.M10 * rhs.M02) + (lhs.M11 * rhs.M12) + (lhs.M12 * rhs.M22),

                    //Row3, colum1
                    (lhs.M20 * rhs.M00) + (lhs.M21 * rhs.M10) + (lhs.M22 * rhs.M20),
                    //Row3, colum2
                    (lhs.M20 * rhs.M01) + (lhs.M21 * rhs.M11) + (lhs.M22 * rhs.M21),
                    //Row3, colum3
                    (lhs.M20 * rhs.M02) + (lhs.M21 * rhs.M12) + (lhs.M22 * rhs.M22)


                );
        }

       

    }
}
