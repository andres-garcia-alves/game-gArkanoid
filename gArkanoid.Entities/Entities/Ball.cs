using System;
using System.Configuration;
using System.Drawing;

using gArkanoid.Base;
using gArkanoid.Interfaces;

namespace gArkanoid.Entities
{
    public class Ball : CollisionBase, iDrawable
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

        private float mov_x = 1;
        private float mov_y = -1;

        private readonly int baseMovement;
        private int acelerationMovement = 0;
        private int ticksAcelerationMovement = 0;

        private readonly Random random;
        private readonly Image demolitionImage;

        public Ball(int baseMovement)
        {
            try
            {
                this.baseMovement = baseMovement;
                base.location = new Point(DEFAULT_X, DEFAULT_Y);

                string path = ConfigurationManager.AppSettings["pathImages"];
                base.image = Image.FromFile(path + "Ball.png", false);
                this.demolitionImage = Image.FromFile(path + "BallDemolition.png", false);

                this.random = new Random(DateTime.Now.Millisecond);
            }

            catch (Exception ex) { throw ex; }
        }

        public Ball(int baseMovement, Point location, eOriginalDirection direction)
        {
            try
            {
                this.baseMovement = baseMovement;
                base.location = location;

                if (direction == eOriginalDirection.UpperLeft) { mov_x = -1; mov_y = -1; }
                else if (direction == eOriginalDirection.UpperRight) { mov_x = 1; mov_y = -1; }
                else if (direction == eOriginalDirection.ButtomLeft) { mov_x = -1; mov_y = -1; }
                else if (direction == eOriginalDirection.ButtomRight) { mov_x = 1; mov_y = 1; }

                string path = ConfigurationManager.AppSettings["pathImages"];
                base.image = Image.FromFile(path + "Ball.png", false);
                this.demolitionImage = Image.FromFile(path + "BallDemolition.png", false);

                this.random = new Random(DateTime.Now.Millisecond);
            }

            catch (Exception ex) { throw ex; }
        }

        #region Properties

        public int X
        {
            get { return this.location.X; }
            set
            {
                if (value > 0 && value < (SCREEN_WIDTH - BALL_WIDTH))
                    this.location.X = value;
                else mov_x *= -1; // invert horizontal direction
            }
        }

        public int Y
        {
            get { return this.location.Y; }
            set
            {
                if (value > 0 && value < (SCREEN_HEIGHT - BALL_HEIGHT))
                    this.location.Y = value;
                else mov_y *= -1; // invert vertical direction
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

        public void Move(bool normalMovement)
        {
            double steps;

            if (normalMovement) {
                this.CheckAcelerationMovement();
                steps = baseMovement + acelerationMovement;

            } else { // slow movement
                steps = baseMovement / 2;
            }
            
            this.X += (int)Math.Round(mov_x * steps, 0);
            this.Y += (int)Math.Round(mov_y * steps, 0);
        }

        public void Draw(Graphics graphics)
        {
            if (Balls.IsDemolitionBallState())
                graphics.DrawImage(demolitionImage, location);
            else
                graphics.DrawImage(image, location);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(location.X - 4, location.Y - 4, BALL_WIDTH + 8, BALL_HEIGHT + 8);
        }

        public override void CollisionedBy(CollisionBase collision)
        {
            // ball out of screen
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.OutputLine"))
            {
                Balls.RemoveBall(this);
            }

            // ball hit playerpad: invert vertical direction
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.PlayerPad"))
            {
                // check into what part of cPlayerPad is the ball collisioning (left border, pad center, right border)
                if (this.X + BALL_WIDTH < collision.Location.X + 20) { // left border

                    mov_x -= 1; // fixed direction change
                    mov_x -= GenerateRandomFactor(); // plus little random direction change

                    if (mov_x < MIN_SIDE_ONE) mov_x = MIN_SIDE_ONE; // -1.41
                    
                    mov_y = -1 * CalculateSideTwo(mov_x);

                } else if (this.X > collision.Location.X + collision.GetWidth() - 20) { // right border

                    mov_x += 1; // fixed direction change
                    mov_x += GenerateRandomFactor(); // plus little random direction change
                    
                    if (mov_x > MAX_SIDE_ONE) mov_x = MAX_SIDE_ONE; // 1.41

                    mov_y = -1 * CalculateSideTwo(mov_x);

                } else { // pad center
                    mov_y *= -1;
                }
            }

            // ball hit a brick
            // detect brick face collisioned, invert ball direction acording to that
            // check 4 posibles directions of the ball (UpLeft, UpRight, DownLeft, DownRight)
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.Brick"))
            {
                // no change ball direction if the ball is in demolition state
                if (Balls.IsDemolitionBallState()) return;

                int delta_x, delta_y;

                if (mov_x < 0 && mov_y < 0) { // ball UpLeft

                    // posibles brick faces colisioned: right, buttom, right&buttom
                    delta_x = (collision.Location.X + collision.GetWidth()) - this.Location.X;
                    delta_y = (collision.Location.Y + collision.GetHeight()) - this.Location.Y;

                    if (delta_x > delta_y) { // buttom
                        mov_y *= -1;
                    } else if (delta_x < delta_y) { // right
                        mov_x *= -1;
                    } else { // both
                        mov_y *= -1; mov_x *= -1;
                    }
                }

                else if (mov_x >= 0 && mov_y < 0) // ball UpRight
                {
                    // posibles brick faces colisioned: left, buttom, left&buttom
                    delta_x = (this.Location.X + this.GetWidth()) - collision.Location.X;
                    delta_y = (collision.Location.Y + collision.GetHeight()) - this.Location.Y;

                    if (delta_x > delta_y) { // buttom
                        mov_y *= -1;
                    } else if (delta_x < delta_y) { // left
                        mov_x *= -1;
                    } else { // both
                        mov_y *= -1; mov_x *= -1;
                    }
                }

                else if (mov_x < 0 && mov_y >= 0) // ball DownLeft
                {
                    // posibles brick faces colisioned: right, top, right&top
                    delta_x = (collision.Location.X + collision.GetWidth()) - this.Location.X;
                    delta_y = (this.Location.Y + this.GetHeight()) - this.Location.Y;

                    if (delta_x > delta_y) { // top
                        mov_y *= -1;
                    } else if (delta_x < delta_y) { // right
                        mov_x *= -1;
                    } else { // both
                        mov_y *= -1; mov_x *= -1;
                    }
                }

                else if (mov_x >= 0 && mov_y >= 0) // ball DownRight
                {
                    // posibles brick faces colisioned: left, top, left&top
                    delta_x = (this.Location.X + this.GetWidth()) - collision.Location.X;
                    delta_y = (this.Location.Y + this.GetHeight()) - this.Location.Y;

                    if (delta_x > delta_y) { // top
                        mov_y *= -1;
                    } else if (delta_x < delta_y) { // right
                        mov_x *= -1;
                    } else { // both
                        mov_y *= -1; mov_x *= -1;
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
        private float CalculateSideTwo(float sideOne)
        {
            if (sideOne > 1.41421356F)  sideOne = 1.41421356F;
            if (sideOne < -1.41421356F) sideOne = -1.41421356F;

            return (float)Math.Sqrt(2 - Math.Pow(sideOne, 2));
        }

        /// <summary>
        /// Generate a number between -0.1 to 0.1
        /// </summary>
        private float GenerateRandomFactor()
        {
            return this.random.Next(-300, 300) / 1000;
        }

        public void Reset()
        {
            mov_x = 1;
            mov_y = -1;
            base.location = new Point(DEFAULT_X, DEFAULT_Y);

            this.acelerationMovement = 0;
            this.ticksAcelerationMovement = 0;
        }

        private void CheckAcelerationMovement()
        {
            this.ticksAcelerationMovement++;

            if (this.ticksAcelerationMovement > TICKS_ACELERATE_MOV) {
                this.ticksAcelerationMovement = 0;
                this.acelerationMovement++;
            }
        }
    }
}
