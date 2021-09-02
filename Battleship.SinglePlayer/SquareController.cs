using Battleship.Entities;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Battleship.SinglePlayer
{
    public static class SquareController
    {
        public static void ResetState(this SquareEntity square)
        {
            square.ShipBelongs = null;
            square.Type = ShipType.Empty;
            square.Part = ShipPart.None;
            square.Rotation = ShipRotation.None;
            square.Image = null;
        }

        public static void SetState(this SquareEntity square, ShipType type, ShipPart part, ShipRotation rotation)
        {
            square.Type = type;
            square.Part = part;
            square.Rotation = rotation;
            square.Image = FactoryImages.CreateShipImage(square.Type, square.Part, square.Rotation, square.Size);
        }

        public static bool Fire(this SquareEntity square, int sleepTime)
        {
            bool output = false;
            square.FlatAppearance.BorderSize = 5;
            square.FlatAppearance.BorderColor = Color.Red;
            square.Refresh();
            Wait(sleepTime);
            square.FlatAppearance.BorderSize = 1;
            square.FlatAppearance.BorderColor = Color.Black;
            square.Type = ShipType.Hit;
            if (square.ShipBelongs != null)
            {
                square.Image = null;
                square.BackgroundImage = FactoryImages.CreateShipImage(square.Type, square.Part, square.Rotation, square.Size);
                square.ShipBelongs.Length--;
                output = true;
            }
            else
            {
                square.BackgroundImage = null;
                square.BackColor = Color.White;
                square.Image = FactoryImages.CreateShipImage(square.Type, square.Part, square.Rotation, square.Size);
            }

            square.Enabled = false;
            return output;
        }

        private static void Wait(int time)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(time);
            });
            thread.Start();
            while (thread.IsAlive)
                Application.DoEvents();
        }
    }
}
