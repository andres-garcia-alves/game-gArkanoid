using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

using gArkanoid.Aux;
using gArkanoid.Interfaces;

namespace gArkanoid.Entities
{
    public class Balls : iDrawable
    {
        #region Constants

        private const int SLOW_BALL_TIME = 12000;
        private const int DEMOLITION_BALL_TIME = 12000;

        #endregion

        #region Enumerations

        public enum eInputType { Keyboard = 4, Mouse = 6 }

        #endregion

        private static List<Ball> balls;
        private static int demolitionBallTime = 0;

        private eInputType inputType;
        private int slowBallTime = 0;
        private Timer tmrSlowBall, tmrDemolitionBall;

        public Balls(eInputType inputType)
        {
            this.inputType = inputType;

            Ball ball = new Ball((int)this.inputType);

            Balls.balls = new List<Ball>();
            Balls.balls.Add(ball);

            // register for collisions checks (ball allways in first place within the list)
            CollisionsSystem.RegisterItemForCollision(ball, 0);
        }

        public static void RemoveBall(Ball ball)
        {
            CollisionsSystem.RemoveItemForCollision(ball);
            Balls.balls.Remove(ball);

            if (balls.Count == 0)
                GameControl.loseLive = true;
        }

        public void Reset()
        {
            this.slowBallTime = 0;
            Balls.demolitionBallTime = 0;

            // remove all balls from collision detection
            foreach (Ball b in balls)
                CollisionsSystem.RemoveItemForCollision(b);

            Ball ball = new Ball((int)inputType);

            Balls.balls = new List<Ball>();
            Balls.balls.Add(ball);

            // register for collisions checks (ball allways in first place within the list)
            CollisionsSystem.RegisterItemForCollision(ball, 0);
        }

        public void Move()
        {
            bool normalMovement = (slowBallTime <= 0) ? true : false;

            foreach (Ball ball in balls)
                ball.Move(normalMovement);
        }

        public void Draw(Graphics graphics)
        {
            foreach (Ball ball in balls)
                ball.Draw(graphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }

        public static bool IsDemolitionBallState()
        {
            return (Balls.demolitionBallTime > 0) ? true : false;
        }

        #region Reward events handlers

        public void SlowBallEvent(object sender, EventArgs e)
        {
            if (slowBallTime == 0)
            {
                tmrSlowBall = new Timer(1000);
                tmrSlowBall.Elapsed += new ElapsedEventHandler(UndoSlowBall);
                tmrSlowBall.Start();
            }

            slowBallTime += SLOW_BALL_TIME;
        }

        private void UndoSlowBall(object sender, ElapsedEventArgs e)
        {
            if (slowBallTime <= 0)
            {
                tmrSlowBall.Stop();
                tmrSlowBall.Close();
            }

            slowBallTime -= 1000;
        }

        public void DemolitionBallEvent(object sender, EventArgs e)
        {
            if (demolitionBallTime == 0)
            {
                tmrDemolitionBall = new Timer(1000);
                tmrDemolitionBall.Elapsed += new ElapsedEventHandler(UndoDemolitionBall);
                tmrDemolitionBall.Start();
            }

            demolitionBallTime += DEMOLITION_BALL_TIME;
        }

        private void UndoDemolitionBall(object sender, ElapsedEventArgs e)
        {
            if (demolitionBallTime <= 0)
            {
                tmrDemolitionBall.Stop();
                tmrDemolitionBall.Close();
            }

            demolitionBallTime -= 1000;
        }

        public void DoubleBallEvent(object sender, EventArgs e)
        {
            Ball ball = new Ball((int)inputType, balls[0].Location, Ball.eOriginalDirection.UpperRight);
            Balls.balls.Add(ball);

            // register for collisions checks (ball allways in first place within the list)
            CollisionsSystem.RegisterItemForCollision(ball, 0);
        }

        public void TripleBallEvent(object sender, EventArgs e)
        {
            Ball ball1 = new Ball((int)inputType, balls[0].Location, Ball.eOriginalDirection.UpperRight);
            Ball ball2 = new Ball((int)inputType, balls[0].Location, Ball.eOriginalDirection.UpperLeft);

            Balls.balls.Add(ball1);
            Balls.balls.Add(ball2);

            // register for collisions checks (ball allways in first place within the list)
            CollisionsSystem.RegisterItemForCollision(ball1, 0);
            CollisionsSystem.RegisterItemForCollision(ball2, 0);
        }

        #endregion
    }
}