using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        private string _text;
        private int _width;
        private int _height;
        private float _speed;
        public Font _font;
        public int _fontSize;



        /// <summary>
        /// Is the text that is located in the actor
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Is the width for the text
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// Is the hight for the text
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Speed
        {
            get
            {
                return _speed;
            }
        }


        /// <summary>
        /// Sets the starting values for the text box
        /// </summary>
        /// <param name="x">The x posistion of the text box</param>
        /// <param name="y">The y posistion of the text box</param>
        /// <param name="name">The name of the text box</param>
        /// <param name="color">The color that is uses for the words</param>
        /// <param name="width">long the text box is </param>
        /// <param name="height">how high you text box is</param>
        /// <param name="text">The words that is with in the UI</param>
        public UIText(float x, float y,float z, String name, Color color,float speed, int width, int height,int fontSize, string text = "")
            : base( x, y, z, speed)
        {
            Text = text;
            Width = width;
            Height = height;
            _font = Raylib.LoadFont("resources/fonts/alagard.png");
            _fontSize = fontSize;
        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosistion.X, LocalPosistion.Y, Width, Height);
            Raylib.DrawTextRec(_font, Text, textBox, _fontSize,1, true, Color.BLUE);
        }
    }
}
