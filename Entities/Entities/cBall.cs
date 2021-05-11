using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

using Garkanoid.Aux;
using Garkanoid.Base;
using Garkanoid.Interfaces;

namespace Garkanoid.Entities
{
    public class cBall : cCollisionBase, iDrawable
    {
        #region Constants

        public const int BALL_WIDTH = 12;
        public const int BALL_HEIGHT = 12;

        public const int DEFAULT_X = (SCREEN_WIDTH - BALL_WIDTH) / 2;
        public const int DEFAULT_Y = 416;

        private const int TICKS_ACELERATE_MOV = 1000;
        private const float MAX_SIDE_ONE = 1.2F; // teorical max: 1.41421356F;
        private const float MIN_SIDE_ONE = -1.2F; // teorical min: -1.41421356F

        #endregion

        #region Enumerations

        public enum eOriginalDirection { UpperLeft, UpperRight, ButtomLeft, ButtomRight }

        #endregion

        private float iMovX = 1;
        private float iMovY = -1;

        private int m_iBaseMovement;
        private int iAcelerationMovement = 0;
        private int iTicksAcelerationMovement = 0;

        private Random oRandom;
        private Image oDemolitionImage;

        public cBall(int iBaseMovement)
        {
            try
            {
                m_iBaseMovement = iBaseMovement;
                base.m_oLocation = new Point(DEFAULT_X, DEFAULT_Y);

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.m_oImage = Image.FromFile(@sPath + "Ball.png", false);
                oDemolitionImage = Image.FromFile(@sPath + "BallDemolition.png", false);

                oRandom = new Random(DateTime.Now.Millisecond);
            }

            catch (Exception ex) { throw ex; }
        }

        public cBall(int iBaseMovement, Point oLocation, eOriginalDirection eDir)
        {
            try
            {
                m_iBaseMovement = iBaseMovement;
                base.m_oLocation = oLocation;

                if (eDir == eOriginalDirection.UpperLeft) { iMovX = -1; iMovY = -1; }
                else if (eDir == eOriginalDirection.UpperRight) { iMovX = 1; iMovY = -1; }
                else if (eDir == eOriginalDirection.ButtomLeft) { iMovX = -1; iMovY = -1; }
                else if (eDir == eOriginalDirection.ButtomRight) { iMovX = 1; iMovY = 1; }

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.m_oImage = Image.FromFile(@sPath + "Ball.png", false);
                oDemolitionImage = Image.FromFile(@sPath + "BallDemolition.png", false);

                oRandom = new Random(DateTime.Now.Millisecond);
            }

            catch (Exception ex) { throw ex; }
        }

        #region Properties

        public int X
        {
            get { return this.m_oLocation.X; }
            set
            {
                if (value > 0 && value < (SCREEN_WIDTH - BALL_WIDTH))
                    this.m_oLocation.X = value;
                else iMovX *= -1; // invert horizontal direction
            }
        }

        public int Y
        {
            get { return this.m_oLocation.Y; }
            set
            {
                if (value > 0 && value < (SCREEN_HEIGHT - BALL_HEIGHT))
                    this.m_oLocation.Y = value;
                else iMovY *= -1; // invert vertical direction
            }
        }

        #endregion

        public override int GetWidth()
        {
            return BALL_WIDTH;
        }

        public override int GetHeight()
        {
            return BALL_HEIGHT;
        }

        public void Move(bool bNormalMovement)
        {
            double iSteps = 0;

            if (bNormalMovement) {
                CheckAcelerationMovement();
                iSteps = m_iBaseMovement + iAcelerationMovement;

            } else { // slow movement
                iSteps = m_iBaseMovement / 2;
            }
            
            this.X += (int)Math.Round(iMovX * iSteps, 0);
            this.Y += (int)Math.Round(iMovY * iSteps, 0);
        }

        public void Draw(Graphics oGraphics)
        {
            if (cBalls.IsDemolitionBallState())
                oGraphics.DrawImage(oDemolitionImage, m_oLocation);
            else
                oGraphics.DrawImage(m_oImage, m_oLocation);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(m_oLocation.X - 4, m_oLocation.Y - 4, BALL_WIDTH + 8, BALL_HEIGHT + 8);
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // ball out of screen
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cOutputLine"))
            {
                cBalls.RemoveBall(this);
            }

            // ball hit playerpad: invert vertical direction
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cPlayerPad"))
            {
                // check into what part of cPlayerPad is the ball collisioning (left border, pad center, right border)
                if (this.X + BALL_WIDTH < oCollision.Location.X + 20) { // left border

                    iMovX -= 1; // fixed direction change
                    iMovX -= GenerateRandomFactor(); // plus little random direction change

                    if (iMovX < MIN_SIDE_ONE) iMovX = MIN_SIDE_ONE; // -1.41
                    
                    iMovY = -1 * CalculateSideTwo(iMovX);

                } else if (this.X > oCollision.Location.X + oCollision.GetWidth() - 20) { // right border

                    iMovX += 1; // fixed direction change
                    iMovX += GenerateRandomFactor(); // plus little random direction change
                    
                    if (iMovX > MAX_SIDE_ONE) iMovX = MAX_SIDE_ONE; // 1.41

                    iMovY = -1 * CalculateSideTwo(iMovX);

                } else { // pad center
                    iMovY *= -1;
                }
            }

            // ball hit a brick
            // detect brick face collisioned, invert ball direction acording to that
            // check 4 posibles directions of the ball (UpLeft, UpRight, DownLeft, DownRight)
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cBrick"))
            {
                // no change ball direction if the ball is in demolition state
                if (cBalls.IsDemolitionBallState()) return;

                int iDeltaX, iDeltaY;

                if (iMovX < 0 && iMovY < 0) { // ball UpLeft

                    // posibles brick faces colisioned: right, buttom, right&buttom
                    iDeltaX = (oCollision.Location.X + oCollision.GetWidth()) - this.Location.X;
                    iDeltaY = (oCollision.Location.Y + oCollision.GetHeight()) - this.Location.Y;

                    if (iDeltaX > iDeltaY) { // buttom
                        iMovY *= -1;
                    } else if (iDeltaX < iDeltaY) { // right
                        iMovX *= -1;
                    } else { // both
                        iMovY *= -1; iMovX *= -1;
                    }
                }

                else if (iMovX >= 0 && iMovY < 0) // ball UpRight
                {
                    // posibles brick faces colisioned: left, buttom, left&buttom
                    iDeltaX = (this.Location.X + this.GetWidth()) - oCollision.Location.X;
                    iDeltaY = (oCollision.Location.Y + oCollision.GetHeight()) - this.Location.Y;

                    if (iDeltaX > iDeltaY) { // buttom
                        iMovY *= -1;
                    } else if (iDeltaX < iDeltaY) { // left
                        iMovX *= -1;
                    } else { // both
                        iMovY *= -1; iMovX *= -1;
                    }
                }

                else if (iMovX < 0 && iMovY >= 0) // ball DownLeft
                {
                    // posibles brick faces colisioned: right, top, right&top
                    iDeltaX = (oCollision.Location.X + oCollision.GetWidth()) - this.Location.X;
                    iDeltaY = (this.Location.Y + this.GetHeight()) - this.Location.Y;

                    if (iDeltaX > iDeltaY) { // top
                        iMovY *= -1;
                    } else if (iDeltaX < iDeltaY) { // right
                        iMovX *= -1;
                    } else { // both
                        iMovY *= -1; iMovX *= -1;
                    }
                }

                else if (iMovX >= 0 && iMovY >= 0) // ball DownRight
                {
                    // posibles brick faces colisioned: left, top, left&top
                    iDeltaX = (this.Location.X + this.GetWidth()) - oCollision.Location.X;
                    iDeltaY = (this.Location.Y + this.GetHeight()) - this.Location.Y;

                    if (iDeltaX > iDeltaY) { // top
                        iMovY *= -1;
                    } else if (iDeltaX < iDeltaY) { // right
                        iMovX *= -1;
                    } else { // both
                        iMovY *= -1; iMovX *= -1;
                    }
                }
            }
        }

        /// <summary>
        /// El desplazamiento de las componentes en X e Y de la pelota se calcula uno en
        /// relación al otro.
        /// Suponiendo que X=1 e Y=1 se puede ver como un triángulo de LadoA=1, LadoB=1
        /// e Hipotenusa=1,41421356 (Hip es la distancia recorrida por la pelota).
        /// Lo que hace este método, es calcular el valor correspondiente al 2do lado
        /// de forma que siempre la hipotenusa sea 1,41421356.
        /// El algoritmo se calcula del despeje de un lado de la ecuación Pitagórica.
        /// </summary>
        /// <param name="SideOne">LadoA del triangulo (+-1.41421356 máx)</param>
        /// <returns>Retorna el valor correspondiente al LadoB del triángulo</returns>
        private float CalculateSideTwo(float fSideOne)
        {
            if (fSideOne > 1.41421356F)  fSideOne = 1.41421356F;
            if (fSideOne < -1.41421356F) fSideOne = -1.41421356F;

            return (float)Math.Sqrt(2 - Math.Pow(fSideOne, 2));
        }

        /// <summary>
        /// Generate a number between -0.1 to 0.1
        /// </summary>
        private float GenerateRandomFactor()
        {
            return oRandom.Next(-300, 300) / 1000;
        }

        public void Reset()
        {
            iMovX = 1;
            iMovY = -1;
            base.m_oLocation = new Point(DEFAULT_X, DEFAULT_Y);

            iAcelerationMovement = 0;
            iTicksAcelerationMovement = 0;
        }

        private void CheckAcelerationMovement()
        {
            iTicksAcelerationMovement++;

            if (iTicksAcelerationMovement > TICKS_ACELERATE_MOV) {
                iTicksAcelerationMovement = 0;
                iAcelerationMovement++;
            }
        }
    }
}
