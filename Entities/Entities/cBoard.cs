using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Garkanoid.Aux;
using Garkanoid.Interfaces;

namespace Garkanoid.Entities
{
    public class cBoard : iDrawable
    {
        private static List<cBrick> lstBricks;
        private static List<cReward> lstRewards;
        public static List<cShot> lstShots;

        #region Constructors

        public cBoard(int iLevel)
        {
            string sLevelNum = iLevel.ToString().PadLeft(2, '0');

            try
            {
                string sPath = ConfigurationManager.AppSettings["pathLevels"];

                // list of bricks within the level
                FileStream fs = new FileStream(sPath + sLevelNum + ".lvl", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                lstBricks = (List<cBrick>)bf.Deserialize(fs);
                fs.Close();

                // new empty list of rewards & fires
                lstShots = new List<cShot>();
                lstRewards = new List<cReward>();

                // register for collisions checks
                cCollisionsSystem.RegisterListForCollision(lstBricks);
            }

            catch (FileNotFoundException) { throw new ApplicationException("File for level " + sLevelNum + " not found."); }
            catch (Exception ex) { throw ex; }
        }

        public cBoard(string sCustom)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathLevels"];

                // list of bricks within the level
                FileStream fs = new FileStream(sPath + sCustom, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                lstBricks = (List<cBrick>)bf.Deserialize(fs);
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
                lstShots = new List<cShot>();
                lstRewards = new List<cReward>();

                // register for collisions checks
                cCollisionsSystem.RegisterListForCollision(lstBricks);
            }

            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Properties

        public List<cBrick> Bricks
        {
            get { return lstBricks; }
        }

        public List<cReward> Rewards
        {
            get { return lstRewards; }
        }

        public List<cShot> Shots
        {
            get { return lstShots; }
        }

        #endregion

        #region Bricks list methods

        public static void RemoveBrick(cBrick oBrick)
        {
            cCollisionsSystem.RemoveItemForCollision(oBrick);
            lstBricks.Remove(oBrick);
        }

        public static void ResetBricks()
        {
            foreach (cBrick oBrick in lstBricks)
                cCollisionsSystem.RemoveItemForCollision(oBrick);

            lstBricks.Clear();
        }

        #endregion

        #region Rewards list methods

        public static void AddReward(cReward oReward)
        {
            lstRewards.Add(oReward);
        }

        public static void RemoveReward(cReward oReward)
        {
            cCollisionsSystem.RemoveItemForCollision(oReward);
            lstRewards.Remove(oReward);
        }

        public static void ResetRewards()
        {
            foreach (cReward oReward in lstRewards)
                cCollisionsSystem.RemoveItemForCollision(oReward);

            lstRewards.Clear();
        }

        #endregion

        #region Shots list methods

        public static void AddShot(cShot oFire)
        {
            lstShots.Add(oFire);
        }

        public static void RemoveShot(cShot oShot)
        {
            cCollisionsSystem.RemoveItemForCollision(oShot);
            lstShots.Remove(oShot);
        }

        public static void ResetShots()
        {
            foreach (cShot oShot in lstShots)
                cCollisionsSystem.RemoveItemForCollision(oShot);

            lstShots.Clear();
        }

        #endregion

        public void Draw(Graphics oGraphics)
        {
            for (int i = 0; i < lstBricks.Count; i++)
                lstBricks[i].Draw(oGraphics);

            for (int i = 0; i < lstRewards.Count; i++)
                lstRewards[i].Draw(oGraphics);

            for (int i = 0; i < lstShots.Count; i++)
                lstShots[i].Draw(oGraphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
