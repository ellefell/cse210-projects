using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService _keyboardService;
        private Point _player1direction = new Point(0, -Constants.CELL_SIZE);
        private Point _player2direction = new Point(0, -Constants.CELL_SIZE);

        private string _player1left = "a";
        private string _player1right = "d";
        private string _player1up = "w";
        private string _player1down = "s";

        private string _player2left = "j";
        private string _player2right = "l";
        private string _player2up = "i";
        private string _player2down = "k";

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this._keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {

            //set up players for controlling
            Player player1 = (Player)cast.GetFirstActor("player1");
            Player player2 = (Player)cast.GetFirstActor("player2");

            //set inital direction 
            if (_keyboardService.GetTotalKeystrokes() == 0)
            {
                player1.TurnCycle(_player1direction);
                player2.TurnCycle(_player2direction);
            }


            // left
            if (_keyboardService.IsKeyDown(_player1left))
            {
                _player1direction = new Point(-Constants.CELL_SIZE, 0);
                _keyboardService.IncrementTotalKeystrokes();
            }
            if (_keyboardService.IsKeyDown(_player2left))
            {
                _player2direction = new Point(-Constants.CELL_SIZE, 0);
                _keyboardService.IncrementTotalKeystrokes();
            }



            // right
            if (_keyboardService.IsKeyDown(_player1right))
            {
                _player1direction = new Point(Constants.CELL_SIZE, 0);
                _keyboardService.IncrementTotalKeystrokes();
            }

            if (_keyboardService.IsKeyDown(_player2right))
            {
                _player2direction = new Point(Constants.CELL_SIZE, 0);
                _keyboardService.IncrementTotalKeystrokes();
            }

            // up
            if (_keyboardService.IsKeyDown(_player1up))
            {
                
                _player1direction = new Point(0, -Constants.CELL_SIZE);
                _keyboardService.IncrementTotalKeystrokes();
            }

            if (_keyboardService.IsKeyDown(_player2up))
            {
                _player2direction = new Point(0, -Constants.CELL_SIZE);
                _keyboardService.IncrementTotalKeystrokes();

            }
            // down
            if (_keyboardService.IsKeyDown(_player1down))
            {
                _player1direction = new Point(0, Constants.CELL_SIZE);
                _keyboardService.IncrementTotalKeystrokes();
            }

            if (_keyboardService.IsKeyDown(_player2down))
            {
                _player2direction = new Point(0, Constants.CELL_SIZE);
                _keyboardService.IncrementTotalKeystrokes();
            }

            //turn players in the appropriate direction
            player1.TurnCycle(_player1direction);
            player2.TurnCycle(_player2direction);

        }
    }
}
