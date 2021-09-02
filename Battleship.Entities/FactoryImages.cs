using System.Drawing;

namespace Battleship.Entities
{
    public static class FactoryImages
    {
        private static Image _empty = Properties.Resources.Waves;
        private static Image _shipSingle = Properties.Resources.ShipSingle;
        private static Image _shipHead = Properties.Resources.ShipHead;
        private static Image _shipBody = Properties.Resources.ShipBody;
        private static Image _shipHiden = Properties.Resources.Hidden;
        private static Image _rotate = Properties.Resources.Rotate;
        private static Image _delete = Properties.Resources.Delete;
        private static Image _start = Properties.Resources.Start;
        private static Image _hit = Properties.Resources.Hit;
        private static Image _missed = Properties.Resources.Missed;

        public static Image CreateShipImage(ShipType type, ShipPart part, ShipRotation rotation, Size size)
        {
            Image image = null;

            switch (type)
            {
                case ShipType.Hidden:
                    image = new Bitmap(_shipHiden, size);
                    break;
                case ShipType.Empty:
                    image = new Bitmap(_empty, size);
                    break;
                case ShipType.Visible:
                    image = CreateShipImage(part, rotation, size);
                    break;
                case ShipType.Hit:
                    if (part == ShipPart.None)
                    {
                        image = new Bitmap(_missed, size);
                    }
                    else
                    {
                        image = new Bitmap(_hit, size);
                    }
                    break;
            }

            return image;
        }

        public static Image CreateShipImage(ShipPart part, ShipRotation rotation, Size size)
        {
            Image image = null;

            switch (part)
            {
                case ShipPart.None:
                    break;
                case ShipPart.Single:
                    image = new Bitmap(_shipSingle, size);
                    ShipRotate(rotation, image, size);
                    break;
                case ShipPart.Head:
                    image = new Bitmap(_shipHead, size);
                    ShipRotate(rotation, image, size);
                    break;
                case ShipPart.Body:
                    image = new Bitmap(_shipBody, size);
                    ShipRotate(rotation, image, size);
                    break;
            }

            return image;
        }

        private static void ShipRotate(ShipRotation rotation, Image image, Size size)
        {
            switch (rotation)
            {
                case ShipRotation.None:
                    break;
                case ShipRotation.Right:
                    break;
                case ShipRotation.Down:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case ShipRotation.Left:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case ShipRotation.Up:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }

        public static Image CreateRotateImage(Size size)
        {
            return new Bitmap(_rotate, size);
        }

        public static Image CreateDeleteImage(Size size)
        {
            return new Bitmap(_delete, size);
        }

        public static Image CreateStartImage(Size size)
        {
            return new Bitmap(_start, size);
        }
    }

}
