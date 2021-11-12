using System;
using System.Collections.Generic;
using System.Text;
using MathLibaray;


namespace MathForGames
{
    class EnemySpawner : Actor
    {
        int Spawns = 5;
        public Scene _scene;
        public Player _player;
        Enemey enemey1;

        public void Start()
        {

        }
        public void End()
        {

        }
       

        private void InitralizeEnemy()
        {
            Enemey enemey1 = new Enemey(40, 1, 40, 30, 2, _player, "Enemy1", Shape.SPHERE);
            enemey1.SetScale(5, 3, 5);
            enemey1.SetTranslation(-10, 1, -15);
            enemey1.LookAt(_player.WorldPosistion);
            enemey1.SetColor(new Vector4(255, 0, 255, 255));


            CircleCollider enemyCircleCollider = new CircleCollider(5, enemey1);
            AABBCollider enemyBoxCollider = new AABBCollider(34, 42, 44, enemey1);
            enemey1.Collider = enemyCircleCollider;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            InitralizeEnemy();
            for (int i = 0; i < Spawns; i++)
            {
                currentScene.AddActor(enemey1);
            }
        }

        private void Draw()
        {

        }
    }
}
