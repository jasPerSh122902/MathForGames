using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// Array made for the actors in the scene
        /// </summary>
        private Actor[] _actors;
        private Actor[] _UIElements;

        /// <summary>
        /// Makes actor in a Scene
        /// </summary>
        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];

        }

        /// <summary>
        /// calls start for all of the actors in the actors array
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// calls the update for the actors in the actors array
        /// </summary>
        public virtual void Update(float deltaTime, Scene currentScene)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime, currentScene);

                //incremtns thorgh the actors array
                for (int j = 0; j < _actors.Length; j++)
                {
                    if(i < _actors.Length)
                    {
                        //sees if the position of the actor 1 and actor 2 are on the same...
                        //position but at the end it sais if actor 2 is actor 1...
                        if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                            //then start on Collision for actor 1 by making actor 2 be collied with.
                            _actors[i].OnCollision(_actors[j], currentScene);
                    }
                  
                }
            }
        }
        /// <summary>
        /// calls Draw for the actors in the actors array
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        //update the ui
        public virtual void UpdateUI(float deltatTime, Scene currentScene)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();

                _UIElements[i].Update(deltatTime, currentScene);
            }
        }

        //draws the ui to Raylib while usin the aray UIElements
        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                _UIElements[i].Draw();
            }
        }



        /// <summary>
        /// calls end for actors in the actors array
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
        }

        /// <summary>
        /// makes a array and adds it to the actors array 
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public void AddActor(Actor actor)
        {
            //makes a new array called temArray and mades it the lengh of actors + a nother spot
            Actor[] temArray = new Actor[_actors.Length + 1];

            //incremens through the actors array
            for (int i = 0; i < _actors.Length; i++)
            {
                temArray[i] = _actors[i];
            }

            //sets temArray to the actors array and set it to actor
            temArray[_actors.Length] = actor;

            //then sets actors to temarray
            _actors = temArray;

        }

        /// <summary>
        /// makes a new array then subtracts a existing actor form that array
        /// </summary>
        /// <param name="actor">the actor in that scene</param>
        /// <returns>returns a bool called actorRemoved</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //create a varialbe to store if the removal was successful
            bool actorRemoved = false;

            //created a new array that is small than the original array.
            Actor[] temArray = new Actor[_actors.Length - 1];

            //is there to the second array and not have space from removed actor.
            int j = 0;

            //incremens through the temArray
            for (int i = 0; i < _actors.Length; i++)
            {

                //sais that if actor is not equal to the actor that is choosen then dont go into but..
                if (_actors[i] != actor)
                {
                    //make temArray have j and make it equal to actors with i so there is no left over space in the array.
                    temArray[j] = _actors[i];
                    //increment j
                    j++;
                }
                //if none of that is needed return true.
                else
                    actorRemoved = true;
            }

            //will only happen if the actor is being removed and will the set actors with temArray.
            if (actorRemoved)
                _actors = temArray;

            //...then returns
            return actorRemoved;
        }

        /// <param name="UI">The actor to add to the scene</param>
        public virtual void AddUIElement(Actor UI)
        {
            //Create a new temp arary larger than the current one
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //Copy all values from old array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Set the old array to be the new array
            _UIElements = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="UI">The actor to remove</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveUIElement(Actor UI)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new temp arary smaller than the current one
            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            //Copy all values except the actor we dont want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the actor that the loop is on is not the temp array counter
                if (_UIElements[i] != UI)
                {
                    //...adds the actor into the new array and increments the tmep array counter
                    tempArray[j] = _UIElements[i];
                    j++;
                }
                //Otherwise if the actor is the one to remove...
                else
                {
                    //...set actorRemove to true
                    actorRemoved = true;
                }
            }

            //If the actorRemove was successful them
            if (actorRemoved)
                //Add the new array to the old array
                _UIElements = tempArray;

            return actorRemoved;
        }

    }
}
