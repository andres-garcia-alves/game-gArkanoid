using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using gArkanoid.Aux;
using gArkanoid.Interfaces;

namespace gArkanoid.Entities
{
    public class Board : iDrawable
    {
        private static List<Brick> bricks;
        private static List<Reward> rewards;
        public static List<Shot> shots;

        #region Constructors

        public Board(int level)
        {
            string levelNum = level.ToString().PadLeft(2, '0');

            try
            {
                string path = ConfigurationManager.AppSettings["pathLevels"];

                // list of bricks within the level
                FileStream fs = new FileStream(path + levelNum + ".lvl", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                Board.bricks = (List<Brick>)bf.Deserialize(fs);
                fs.Close();

                // new empty list of rewards & fires
                Board.shots = new List<Shot>();
                Board.rewards = new List<Reward>();

                // register for collisions checks
                CollisionsSystem.RegisterListForCollision(Board.bricks);
            }

            catch (FileNotFoundException) { throw new ApplicationException("File for level " + levelNum + " not found."); }
            catch (Exception ex) { throw ex; }
        }

        public Board(string custom)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathLevels"];

                // list of bricks within the level
                FileStream fs = new FileStream(path + custom, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                bricks = (List<Brick>)bf.Deserialize(fs);
                fs.Close();

                /*
                lstBricks = new List<cBrick>();
                lstBricks.Add(new cBrick(50, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(100, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(150, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(200, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(250, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(300, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(350, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(400, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(450, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(500, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));
                lstBricks.Add(new cBrick(550, 70, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Gray));

                lstBricks.Add(new cBrick(50, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(100, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(150, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(200, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(250, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(300, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(350, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(400, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(450, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(500, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));
                lstBricks.Add(new cBrick(550, 95, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Red));

                lstBricks.Add(new cBrick(50, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(100, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(150, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(200, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(250, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(300, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(350, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(400, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(450, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(500, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));
                lstBricks.Add(new cBrick(550, 120, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Yellow));

                lstBricks.Add(new cBrick(50, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(100, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(150, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(200, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(250, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(300, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(350, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(400, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(450, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(500, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));
                lstBricks.Add(new cBrick(550, 145, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Blue));

                lstBricks.Add(new cBrick(50, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(100, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(150, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(200, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(250, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(300, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(350, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(400, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(450, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(500, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));
                lstBricks.Add(new cBrick(550, 170, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Pink));

                lstBricks.Add(new cBrick(50, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(100, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(150, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(200, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(250, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(300, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(350, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(400, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(450, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(500, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));
                lstBricks.Add(new cBrick(550, 195, cBrick.eBrickType.Normal, cBrick.eRewardType.None, cBrick.eColor.Green));

                FileStream fs = new FileStream(sPath + "Default.lvl", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, lstBricks);
                fs.Close();
                */

                // new empty list of rewards & fires
                Board.shots = new List<Shot>();
                Board.rewards = new List<Reward>();

                // register for collisions checks
                CollisionsSystem.RegisterListForCollision(Board.bricks);
            }

            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Properties

        public List<Brick> Bricks
        {
            get { return Board.bricks; }
        }

        public List<Reward> Rewards
        {
            get { return Board.rewards; }
        }

        public List<Shot> Shots
        {
            get { return Board.shots; }
        }

        #endregion

        #region Bricks list methods

        public static void RemoveBrick(Brick brick)
        {
            CollisionsSystem.RemoveItemForCollision(brick);
            Board.bricks.Remove(brick);
        }

        public static void ResetBricks()
        {
            foreach (Brick brick in bricks)
                CollisionsSystem.RemoveItemForCollision(brick);

            Board.bricks.Clear();
        }

        #endregion

        #region Rewards list methods

        public static void AddReward(Reward reward)
        {
            Board.rewards.Add(reward);
        }

        public static void RemoveReward(Reward reward)
        {
            CollisionsSystem.RemoveItemForCollision(reward);
            Board.rewards.Remove(reward);
        }

        public static void ResetRewards()
        {
            foreach (Reward reward in rewards)
                CollisionsSystem.RemoveItemForCollision(reward);

            Board.rewards.Clear();
        }

        #endregion

        #region Shots list methods

        public static void AddShot(Shot fire)
        {
            Board.shots.Add(fire);
        }

        public static void RemoveShot(Shot shot)
        {
            CollisionsSystem.RemoveItemForCollision(shot);
            Board.shots.Remove(shot);
        }

        public static void ResetShots()
        {
            foreach (Shot shot in Board.shots)
                CollisionsSystem.RemoveItemForCollision(shot);

            Board.shots.Clear();
        }

        #endregion

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < bricks.Count; i++)
                bricks[i].Draw(graphics);

            for (int i = 0; i < rewards.Count; i++)
                rewards[i].Draw(graphics);

            for (int i = 0; i < shots.Count; i++)
                shots[i].Draw(graphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
