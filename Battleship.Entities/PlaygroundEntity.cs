using System.Collections.Generic;

namespace Battleship.Entities
{
    public class PlaygroundEntity
    {
        public SquareEntity[,] Squares { get; set; }
        public List<ShipEntity> Ships { get; set; }

        public PlaygroundEntity()
        {
            Squares = new SquareEntity[10, 10];
            Ships = new List<ShipEntity>();
        }
    }
}
