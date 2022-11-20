using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A long limbless reptile.</para>
    /// <para>The responsibility of Cycle is to move itself.</para>
    /// </summary>
    public class Cycle : Actor
    {
        private List<Actor> _segments = new List<Actor>();

        /// <summary>
        /// Constructs a new instance of a Cycle.
        /// </summary>
        public Cycle()
        {
            PrepareTrail();
        }

        /// <summary>
        /// Gets the body segments.
        /// </summary>
        /// <returns>The body segments in a List.</returns>
        public List<Actor> GetTrail()
        {
            return new List<Actor>(_segments.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the head segment.
        /// </summary>
        /// <returns>The head segment as an instance of Actor.</returns>
        public virtual Actor GetCycle()
        {
            return _segments[0];
        }

        /// <summary>
        /// Gets the segments (including the head).
        /// </summary>
        /// <returns>A list of segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return _segments;
        }

        /// <summary>
        /// Grows the snake's tail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public virtual void GrowTrail(int numberOfSegments)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor trail = _segments.Last<Actor>();
                Point velocity = trail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = trail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(this.GetColor());
                _segments.Add(segment);
            }
        }

        
        

        /// <summary>
        /// Turns the head of the snake in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnCycle(Point direction)
        {
            _segments[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the snake body for moving.
        /// </summary>
        private void PrepareTrail()
        {
            int x = Constants.MAX_X / 2;
            int y = Constants.MAX_Y - (Constants.MAX_Y) / 3;

            for (int i = 0; i < Constants.CYCLE_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = i == 0 ? "@" : "#";
                

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                _segments.Add(segment);
            }
        }
    }
}