using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Timers;

using Garkanoid.Aux;
using Garkanoid.Base;
using Garkanoid.Interfaces;

namespace Garkanoid.Entities
{
    public class cBalls : iDrawable
    {
        #region Constants

        private const int SLOW_BALL_TIME = 12000;
        private const int DEMOLITION_BALL_TIME = 12000;

        #endregion

        #region Enumerations

        public enum eInputType { Keyboard = 4, Mouse = 6 }

        #endregion

        private static List<cBall> lstBalls;
        private static int iDemolitionBallTime = 0;

        private eInputType m_eType;
        private int iSlowBallTime = 0;
        private Timer tmrSlowBall, tmrDemolitionBall;

        public cBalls(eInputType eType)
        {
            m_eType = eType;

            cBall oBall = new cBall((int)m_eType);

            lstBalls = new List<cBall>();
            lstBalls.Add(oBall);

            // register for collisions checks (ball allways in first place within the list)
            cCollisionsSystem.RegisterItemForCollision(oBall, 0);
        }

        public static void RemoveBall(cBall oBall)
        {
            cCollisionsSystem.RemoveItemForCollision(oBall);
            lstBalls.Remove(oBall);

            if (lstBalls.Count == 0)
                cGameControl.bLoseLive = true;
        }

        public void Reset()
        {
            iSlowBallTime = 0;
            iDemolitionBallTime = 0;

            // remove all balls from collision detection
            foreach (cBall o in lstBalls)
                cCollisionsSystem.RemoveItemForCollision(o);

            cBall oBall = new cBall((int)m_eType);

            lstBalls = new List<cBall>();
            lstBalls.Add(oBall);

            // register for collisions checks (ball allways in first place within the list)
            cCollisionsSystem.RegisterItemForCollision(oBall, 0);
        }

        public void Move()
        {
            bool bNormalMovement = (iSlowBallTime <= 0) ? true : false;

            foreach (cBall oBall in lstBalls)
                oBall.Move(bNormalMovement);
        }

        public void Draw(Graphics oGraphics)
        {
            foreach (cBall oBall in lstBalls)
                oBall.Draw(oGraphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }

        public static bool IsDemolitionBallState()
        {
            return (iDemolitionBallTime > 0) ? true : false;
        }

        #region Reward events handlers

        public void SlowBallEvent(object sender, EventArgs e)
        {
            if (iSlowBallTime == 0)
            {
                tmrSlowBall = new Timer(1000);
                tmrSlowBall.Elapsed += new ElapsedEventHandler(UndoSlowBall);
                tmrSlowBall.Start();
            }

            iSlowBallTime += SLOW_BALL_TIME;
        }

        private void UndoSlowBall(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (iSlowBallTime <= 0)
            {
                tmrSlowBall.Stop();
                tmrSlowBall.Close();
            }

            iSlowBallTime -= 1000;
        }

        public void DemolitionBallEvent(object sender, EventArgs e)
        {
            if (iDemolitionBallTime == 0)
            {
                tmrDemolitionBall = new Timer(1000);
                tmrDemolitionBall.Elapsed += new ElapsedEventHandler(UndoDemolitionBall);
                tmrDemolitionBall.Start();
            }

            iDemolitionBallTime += DEMOLITION_BALL_TIME;
        }

        private void UndoDemolitionBall(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (iDemolitionBallTime <= 0)
            {
                tmrDemolitionBall.Stop();
                tmrDemolitionBall.Close();
            }

            iDemolitionBallTime -= 1000;
        }

        public void DoubleBallEvent(object sender, EventArgs e)
        {
            cBall oBall = new cBall((int)m_eType, lstBalls[0].Location, cBall.eOriginalDirection.UpperRight);

            lstBalls.Add(oBall);

            // register for collisions checks (ball allways in first place within the list)
            cCollisionsSystem.RegisterItemForCollision(oBall, 0);
        }

        public void TripleBallEvent(object sender, EventArgs e)
        {
            cBall oBall1 = new cBall((int)m_eType, lstBalls[0].Location, cBall.eOriginalDirection.UpperRight);
            cBall oBall2 = new cBall((int)m_eType, lstBalls[0].Location, cBall.eOriginalDirection.UpperLeft);

            lstBalls.Add(oBall1);
            lstBalls.Add(oBall2);

            // register for collisions checks (ball allways in first place within the list)
            cCollisionsSystem.RegisterItemForCollision(oBall1, 0);
            cCollisionsSystem.RegisterItemForCollision(oBall2, 0);
        }

        #endregion
    }
}