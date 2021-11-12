using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibaray;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();
        private Camera3D _camera = new Camera3D();
        private Player _cameraPlayer;
        private Player _player;




        /// <summary>
        /// is the call to start the application
        /// </summary>
        public void Run()
        {
            //calles the entrire application
            Start();

            //made the three float for delta time to function
            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;


            //loops till application is done
            while (!_applicationShouldClose || Raylib.WindowShouldClose())
            {
                //getss the time from the Stopwatch timer
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //uses the last time that is at the end of the loop to subtact from the currentTime...
                //... to get the deltaTime.
                deltaTime = currentTime - lastTime;

                Update(deltaTime);

                Draw();

                //gets the currentTime and saves it
                lastTime = currentTime;
            }


            //is the call to end the entire appliction
            End();
        }

        private void InitializeCamera()
        {
            // Camera position
            _camera.position = new System.Numerics.Vector3(15, 15, 5);
            //Point the camera is focused on
            _camera.target = new System.Numerics.Vector3(0, 0, 0);
            //Camera up vector (roation towards target)
            _camera.up = new System.Numerics.Vector3(0, 10, 0);
            // The point of view of the camera
            _camera.fovy = 90;
            //Camera mode type
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE;


        }

        /// <summary>
        /// Called when the applicaiton starts
        /// </summary>
        private void Start()
        {
            //created a window using raylib
            Raylib.InitWindow(800, 450, "The math for game. ");
            Raylib.SetTargetFPS(60);

            InitializeCamera();

            _stopwatch.Start();

            InitializeActor();

            _scenes[_currentSceneIndex].Start();

        }

        /// <summary>
        /// end the appliction 
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
        }


        private void InitializeActor()
        {
            Scene scene = new Scene();


            //This is all player
            Player player = new Player(5, 5, 10, 50,10, "Player1", Shape.CUBE);
            _cameraPlayer = player;


            

            Actor actor1 = new Actor(0, 1, 0, 0, "Actor", Shape.CUBE);
            Actor actor2 = new Actor(0, 1, 0, 0, "Actor1", Shape.CUBE);
            Actor actor4 = new Actor(1, 1, 1, 0, "Actor3", Shape.CUBE);


            player.AddChild(actor1);
            player.AddChild(actor4);
            actor1.AddChild(actor2);

            player.SetScale(10, 3, 10);
            player.SetTranslation(0, 0, 0);
            player.SetColor(new Vector4(255, 0, 255, 255));


            actor1.SetScale(.75f, .75f, 1);
            actor1.SetColor(new Vector4(200, 0, 255, 255));

            actor2.SetScale(.50f, .50f, 1);
            actor2.SetColor(new Vector4(200, 100, 255, 255));


            actor4.SetScale(.050f, .050f, .050f);
            actor4.SetColor(new Vector4(200, 50, 255, 255));

            scene.AddActor(player);
            //scene.AddActor(enemey1);
            scene.AddActor(actor1);
            scene.AddActor(actor2);
            scene.AddActor(actor4);

            //adds the collision to the player
            CircleCollider playerCollider = new CircleCollider(5, player);
            AABBCollider playerBoxCollider = new AABBCollider(34, 42,44, player);

            player.Collider = playerBoxCollider;



            Enemey enemey1 = new Enemey(40, 1, 40, 30, 2, player, "Enemy1", Shape.SPHERE);
            enemey1.SetScale(5, 3, 5);
            enemey1.SetTranslation(-10, 1, -15);
            enemey1.LookAt(player.WorldPosistion);
            enemey1.SetColor(new Vector4(255, 0, 255, 255));



            CircleCollider enemyCircleCollider = new CircleCollider(5, enemey1);
            AABBCollider enemyBoxCollider = new AABBCollider(34, 42,44, enemey1);
            enemey1.Collider = enemyCircleCollider;

            EnemySpawner enemySpawner1 = new EnemySpawner(new Vector3(40, 1, 20), enemey1, Shape.CUBE);

            //This is all player
            Player player2 = new Player(10, 5, 20, 50, 10, "Player2", Shape.CUBE);

            Actor actor5 = new Actor(0, 1, 0, 0, "Actor", Shape.CUBE);
            Actor actor6 = new Actor(0, 1, 0, 0, "Actor1", Shape.CUBE);
            Actor actor8 = new Actor(1, 1, 1, 0, "Actor3", Shape.CUBE);

            player2.AddChild(actor5);
            player2.AddChild(actor6);
            actor6.AddChild(actor8);

            player2.SetScale(10, 3, 10);
            player2.SetTranslation(20, 0, -20);
            player2.SetColor(new Vector4(255, 0, 50, 255));


            actor5.SetScale(.75f, .75f, 1);
            actor5.SetColor(new Vector4(200, 0, 255, 255));

            actor6.SetScale(.50f, .50f, 1);
            actor6.SetColor(new Vector4(200, 100, 255, 255));

            actor8.SetScale(.050f, .050f, .050f);
            actor8.SetColor(new Vector4(200, 50, 255, 255));

            scene.AddActor(player2);
            scene.AddActor(actor5);
            scene.AddActor(actor6);
            scene.AddActor(actor8);
            scene.AddActor(enemySpawner1);

            //adds the collision to the player
            CircleCollider player2Collider = new CircleCollider(5, player2);
            AABBCollider player2BoxCollider = new AABBCollider(34, 42,44, player2);
            player2.Collider = playerBoxCollider;

            UIText Ui = new UIText(0, 10,10, "Health", Color.DARKBLUE,0,150, 80, 30, "Player 1 Health " + player.Health);
            UIText Ui2 = new UIText(600, 10, 10, "Health", Color.DARKBLUE, 0, 150, 80, 30, "Player 2 Health " + player2.Health);

            //adds the collsion to the enemy
            scene.AddUIElement(Ui);
            scene.AddUIElement(Ui2);
            _currentSceneIndex = AddScene(scene);
        }



        /// <summary>
        /// Updates the Engine when it is called
        /// </summary>
        private void Update(float deltaTime)
        {
            

            _camera.position = new System.Numerics.Vector3(_cameraPlayer.WorldPosistion.X, _cameraPlayer.WorldPosistion.Y + 40, _cameraPlayer.WorldPosistion.Z + 40);
            // Point the camera is focused on
            _camera.target = new System.Numerics.Vector3(_cameraPlayer.WorldPosistion.X, _cameraPlayer.WorldPosistion.Y, _cameraPlayer.WorldPosistion.Z);

            _scenes[_currentSceneIndex].Update(deltaTime, _scenes[_currentSceneIndex]);


            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        ///call the current Scene.
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.DARKGREEN);
            Raylib.DrawGrid(500, 10);


            //add all of the icons back to the buffer
            _scenes[_currentSceneIndex].Draw();


            Raylib.EndMode3D();

            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndDrawing();

        }


 

        /// <summary>
        /// Creats a array that is teparay then adds all the old arrays vaules to it..
        /// then sets the last index of that array to be the scene.
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {

            //craets a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //copy all values of old array then...
            for (int i = 0; i < _scenes.Length; i++)
            {
                //puts it in side the array.
                tempArray[i] = _scenes[i];
            }

            //set the last index to be the scene.
            tempArray[_scenes.Length] = scene;

            //set the old array to e the new array
            _scenes = tempArray;

            //returns the last array.
            return _scenes.Length - 1;
        }

        /// <summary>
        /// get the next key that was typed in the input stream.
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNewtKey()
        {
            //if there are no keys being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;
            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }
        /// <summary>
        /// Closes the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
