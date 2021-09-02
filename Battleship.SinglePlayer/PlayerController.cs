using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public class PlayerController : PlayerControllerBase
    {
        public PlayerController(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground) : base(playerPlayground, opponentPlayground)
        {
        }

        public override Finished AttackTurn()
        {
            if (OpponentPlaygroud.IsFinished())
            {
                return Finished.PlayerWin;
            }
            else
            {
                OpponentPlaygroud.Enabled(true);
                PlayerPlayground.Enabled(false);
                return Finished.Continue;
            }
        }

        public override int DeleteShip(SquareEntity square)
        {
            ShipEntity ship = square.ShipBelongs;
            ship.Clear();
            PlayerPlayground.Ships.Remove(ship);
            return ship.Length;
        }

    }
}
