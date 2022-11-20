using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the cycles 
    /// collide
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private string _winner;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleCycleCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// UEnds game if the cycles collide.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleCycleCollisions(Cast cast)
        {
            Player player1 = (Player)cast.GetFirstActor("player1");
            Player player2 = (Player)cast.GetFirstActor("player2");
            Actor cycle1 = player1.GetCycle();
            Actor cycle2 = player2.GetCycle();
            List<Actor> trail1 = player1.GetTrail();
            List<Actor> trail2 = player2.GetTrail();
            
            _winner = IdentifyTrailCollision("player 2", cycle2, trail1);
            _winner = IdentifyTrailCollision("player 1", cycle1, trail2);
        }
            
        

        /// <summary>
        /// idenifies if segemnt has collided with the opponents trail.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private string IdentifyTrailCollision(string player, Actor cycle, List<Actor> trail)
        {

            foreach (Actor segment in trail)
            {
                if (segment.GetPosition().Equals(cycle.GetPosition()))
                {
                    _isGameOver = true;
                    _winner = player.Contains("1") == true ? "Player 2" : "Player 1";
                }
            }
            return _winner;
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                
                // create a "game over" message
                int x = Constants.MAX_X / 2 - 50;
                int y = Constants.MAX_Y / 2 - 50;
                Point messagePosition = new Point(x, y);
                Point winnerPosition = new Point(x, y + 25);

                Actor message = new Actor();
                Actor winner = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(messagePosition);
                winner.SetText("The winner is: " + GetWinner());
                winner.SetPosition(winnerPosition);
                cast.AddActor("messages", message);
                cast.AddActor("messages", winner);

                // make everything white
                MakeEverthingWhite(cast);
            }
        }

        private void MakeEverthingWhite(Cast cast)
        {
            Player player1 = (Player)cast.GetFirstActor("player1");
            Player player2 = (Player)cast.GetFirstActor("player2");

            player1.PlayerGameOver();
            player2.PlayerGameOver();

            player1.SetPlayerColorWhite();
            player2.SetPlayerColorWhite();
        }
    private string GetWinner()
    {
        return _winner;
    }
    }
}