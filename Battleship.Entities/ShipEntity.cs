using System.Collections.Generic;

namespace Battleship.Entities
{
    public class ShipEntity
    {
        public List<SquareEntity> ShipParts { get; set; }
        public int Length { get; set; }
        public ShipRotation Rotation { get; set; }
        public ShipType Type { get; set; }

        public ShipEntity()
        {
            ShipParts = new List<SquareEntity>();
            Rotation = ShipRotation.None;
            Length = 0;
            Type = ShipType.Hidden;
        }
    }
}
