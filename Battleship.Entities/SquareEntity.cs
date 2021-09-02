using System.Drawing;
using System.Windows.Forms;

namespace Battleship.Entities
{
    public class SquareEntity : Button
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ShipType Type { get; set; }
        public ShipPart Part { get; set; }
        public ShipRotation Rotation { get; set; }
        public ShipEntity ShipBelongs { get; set; }

        public SquareEntity(int x, int y, Size size) : base()
        {
            X = x;
            Y = y;
            ShipBelongs = null;
            Type = ShipType.Empty;
            Part = ShipPart.None;
            Rotation = ShipRotation.None;
            Size = size;
            AutoSize = false;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            BackgroundImage = FactoryImages.CreateShipImage(Type, Part, Rotation, size);
            ImageAlign = ContentAlignment.MiddleCenter;
            FlatStyle = FlatStyle.Flat;
        }




    }
}
