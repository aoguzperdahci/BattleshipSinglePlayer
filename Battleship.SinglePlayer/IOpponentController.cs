namespace Battleship.SinglePlayer
{
    public interface IOpponentController : IGameController
    {
        public bool Fire();
        public void PlaceShips();
        public void ShowShips();
    }
}
