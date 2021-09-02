using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public abstract class GameControllerBase : IGameController
    {
        public PlaygroundEntity PlayerPlayground { get; set; }
        public PlaygroundEntity OpponentPlaygroud { get; set; }

        public GameControllerBase(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground)
        {
            PlayerPlayground = playerPlayground;
            OpponentPlaygroud = opponentPlayground;
        }

        public bool AddShip(int shipLength, int headX, int headY, ShipType type)
        {
            ShipRotation rotation = ShipRotation.Right;
            for (int i = 0; i < 4; i++)
            {
                if (IsPlaceable(shipLength, headX, headY, rotation))
                {
                    CreateShip(shipLength, headX, headY, rotation, type);
                    return true;
                }
                else
                {
                    rotation++;
                    if (rotation > ShipRotation.Up)
                    {
                        rotation = ShipRotation.Right;
                    }
                }
            }

            return false;
        }

        public void RotateShip(SquareEntity square)
        {
            ShipEntity ship = square.ShipBelongs;
            if (ship == null)
            {
                return;
            }
            ShipRotation rotation = ship.Rotation;
            ShipType type = ship.Type;
            int shipLength = ship.Length;
            int headX = ship.ShipParts[0].X;
            int headY = ship.ShipParts[0].Y;
            PlayerPlayground.Ships.Remove(ship);
            ship.Clear();

            rotation++;
            if (rotation > ShipRotation.Up)
            {
                rotation = ShipRotation.Right;
            }

            for (int i = 0; i < 4; i++)
            {
                if (IsPlaceable(shipLength, headX, headY, rotation))
                {
                    break;
                }
                else
                {
                    rotation++;
                    if (rotation > ShipRotation.Up)
                    {
                        rotation = ShipRotation.Right;
                    }
                }
            }

            CreateShip(shipLength, headX, headY, rotation, type);

        }

        public bool IsPlaceable(int shipLength, int headX, int headY, ShipRotation rotation)
        {
            bool output = true;

            switch (rotation)
            {
                case ShipRotation.None:
                    output = false;
                    break;
                case ShipRotation.Right:
                    if (headX + shipLength > 10)
                        output = false;
                    else
                    {
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (PlayerPlayground.Squares[headX + i, headY].ShipBelongs != null)
                            {
                                output = false;
                            }
                        }
                    }
                    break;
                case ShipRotation.Down:
                    if (headY + shipLength > 10)
                        output = false;
                    else
                    {
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (PlayerPlayground.Squares[headX, headY + i].ShipBelongs != null)
                            {
                                output = false;
                            }
                        }
                    }
                    break;
                case ShipRotation.Left:
                    if (headX - shipLength < -1)
                        output = false;
                    else
                    {
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (PlayerPlayground.Squares[headX - i, headY].ShipBelongs != null)
                            {
                                output = false;
                            }
                        }
                    }
                    break;
                case ShipRotation.Up:
                    if (headY - shipLength < -1)
                        output = false;
                    else
                    {
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (PlayerPlayground.Squares[headX, headY - i].ShipBelongs != null)
                            {
                                output = false;
                            }
                        }
                    }
                    break;
            }

            return output;
        }

        private void CreateShip(int shipLength, int headX, int headY, ShipRotation rotation, ShipType type)
        {
            ShipEntity ship = new ShipEntity();
            PlayerPlayground.Ships.Add(ship);
            ship.Rotation = rotation;
            ship.Length = shipLength;
            ship.Type = type;

            for (int i = 0; i < shipLength; i++)
            {
                ShipPart shipPart;
                ShipRotation shipRotation = rotation;

                if (shipLength == 1)
                {
                    shipPart = ShipPart.Single;
                }
                else if (i == 0)
                {
                    shipPart = ShipPart.Head;
                    shipRotation += 2;
                    if (shipRotation > ShipRotation.Up)
                    {
                        shipRotation -= 4;
                    }
                }
                else if (i == shipLength - 1)
                {
                    shipPart = ShipPart.Head;
                }
                else
                {
                    shipPart = ShipPart.Body;
                }

                SquareEntity square = PlayerPlayground.Squares[headX, headY];
                square.SetState(type, shipPart, shipRotation);
                square.ShipBelongs = ship;
                ship.ShipParts.Add(square);

                switch (rotation)
                {
                    case ShipRotation.Right:
                        headX++;
                        break;
                    case ShipRotation.Down:
                        headY++;
                        break;
                    case ShipRotation.Left:
                        headX--;
                        break;
                    case ShipRotation.Up:
                        headY--;
                        break;
                }
            }
        }

        public abstract Finished AttackTurn();
    }
}
