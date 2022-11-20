using System;
using System.Collections.Generic;
using System.Linq;


namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Player is to keep track of properties and actions associated with each player. 
    /// 
    /// </para>
    /// </summary>
    public class Player: Cycle
    {
        //changes where player starts on screen
        private int _playerXOffset; 
        private Color _playerColor;
        private bool _gameOver = false;
        
        private bool _whiteTrail = false;

        public Player(int playerXOffset)
        {
            //set player color
            Random random = new Random();
            int _red = random.Next(50, 255);
            int _green = random.Next(50, 255);
            int _blue = random.Next(50, 255);

            Color playerColor = new Color(_red, _green, _blue);
            _playerColor = playerColor;

            //set player location
            _playerXOffset = playerXOffset;

            SetPlayerPosition();

            //set Player color
            SetPlayerColor();
        }

        public override Actor GetCycle()
        {
            return this.GetSegments()[0];
        }
        
        //set player position

        public void SetPlayerPosition()
        {
            List<Actor> segments = this.GetSegments();
            for (int i = 0; i < segments.Count; i++)
            {
                Actor segment = segments[i];
                Point segmentLocation = new Point(((Constants.MAX_X / 2) + _playerXOffset) - i, (Constants.MAX_Y / 2));

                segment.SetPosition(segmentLocation);
            }
        }
        public override void GrowTrail(int numTrailSegments)
        {
            List<Actor> segments = this.GetSegments();

            Color trailColor = _whiteTrail == false ? _playerColor : Constants.WHITE;

            for (int i = 0; i < numTrailSegments; i++)
            {
                Actor trail = segments.Last<Actor>();
                Point velocity = trail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = trail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(trailColor);
                segments.Add(segment);
            }
        }
        public void SetPlayerColor()
        {
            List<Actor> segments = this.GetSegments();
            Actor cycle = this.GetCycle();

            cycle.SetColor(_playerColor);

            for (int i = 0; i < segments.Count; i++)
            {
                Actor segment = segments[i];
                segment.SetColor(_playerColor);
            }
        }

        //sets the color of the cycle and tail to white

        public void SetPlayerColorWhite()
        {
            List<Actor> segments = this.GetTrail();

            Actor cycle = this.GetCycle();

            cycle.SetColor(Constants.WHITE);

            _whiteTrail = true;

            for (int i = 0; i < segments.Count; i++)
            {
                Actor segment = segments[i];
                segment.SetColor(Constants.WHITE);
            }
        }

        public bool PlayerGameOver()
        {
            _gameOver = true;

            return _gameOver;
        }

        public override void MoveNext()
        {
            GrowTrail(1);

            List<Actor> segments = this.GetSegments();

            foreach (Actor segment in segments)
            {
                segment.MoveNext();
            }

            for (int i = segments.Count - 1; i > 0; i--)
            {
                Actor trailing = segments[i];
                Actor previous = segments[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }


    }
}

