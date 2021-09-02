using Battleship.Entities;
using System;
using System.Collections.Generic;

namespace Battleship.SinglePlayer
{
    public class AIController : OpponentControllerBase
    {
        private List<SquareEntity> _available;
        private List<List<SquareEntity>> _priority;
        private Random _random;
        private List<ShipEntity> _opponentShips;

        public AIController(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground) : base(playerPlayground, opponentPlayground)
        {
            _available = new List<SquareEntity>();
            foreach (SquareEntity square in OpponentPlaygroud.Squares)
            {
                _available.Add(square);
            }
            _opponentShips = new List<ShipEntity>();
            _priority = new List<List<SquareEntity>>();
            _random = new Random();
        }

        public override Finished AttackTurn()
        {
            if (Fire())
            {
                if (OpponentPlaygroud.IsFinished())
                {
                    return Finished.OpponentWin;
                }
                AttackTurn();
            }
            return Finished.Continue;
        }

        public override bool Fire()
        {
            if (_priority.Count > 0)
            {
                List<SquareEntity> prioritySquares = _priority[0];
                if (prioritySquares.Count == 0)
                {
                    _priority.RemoveAt(0);
                    return true;
                }
                int index = _random.Next(0, prioritySquares.Count);
                SquareEntity square = prioritySquares[index];
                _available.Remove(square);
                if (square.Fire(500))
                {
                    ShipEntity ship = square.ShipBelongs;

                    if (ship.Length <= 0)
                    {
                        _opponentShips.Remove(ship);
                    }

                    if (ship == null)
                    {
                        return false;
                    }

                    foreach (ShipEntity s in _opponentShips)
                    {
                        if (s == ship)
                        {
                            if (ship.Length == 0)
                            {
                                _priority.RemoveAt(0);
                                //_opponentShips.Remove(ship);
                            }
                            else
                            {
                                List<SquareEntity> newPriority = CreatePriorityListSecondTime(ship);
                                _priority[0] = newPriority;
                            }
                            return true;
                        }
                    }

                    _priority[0].Remove(square);
                    if (_priority[0].Count == 0)
                    {
                        _priority.RemoveAt(0);
                    }

                    if (ship.Length != 0)
                    {
                        _opponentShips.Add(ship);
                        List<SquareEntity> newSquares;
                        newSquares = CreatePriorityList(square.X, square.Y);
                        _priority.Add(newSquares);
                    }
                    return true;
                }
                else
                {
                    _priority[0].Remove(square);
                    if (_priority[0].Count == 0)
                    {
                        _priority.RemoveAt(0);
                    }
                    return false;
                }
            }
            else
            {
                int index = _random.Next(0, _available.Count);
                SquareEntity square = _available[index];
                _available.RemoveAt(index);
                if (square.Fire(500))
                {
                    ShipEntity s = square.ShipBelongs;
                    if (s == null)
                    {
                        return false;
                    }

                    if (s.Length > 0)
                    {
                        _opponentShips.Add(s);
                        List<SquareEntity> squares = CreatePriorityList(square.X, square.Y);
                        _priority.Add(squares);
                    }

                    return true;
                }
                return false;
            }
        }

        private List<SquareEntity> CreatePriorityListSecondTime(ShipEntity ship)
        {
            List<SquareEntity> squares = new List<SquareEntity>();

            //if both head and tail have already been hit then all ship parts have known
            if (ship.ShipParts[0].Type == ShipType.Hit && ship.ShipParts[ship.ShipParts.Count - 1].Type == ShipType.Hit)
            {
                foreach (var square in ship.ShipParts)
                {
                    if (square.Type != ShipType.Hit)
                    {
                        squares.Add(square);
                    }
                }
                return squares;
            }

            int headX = ship.ShipParts[0].X;
            int headY = ship.ShipParts[0].Y;
            int tailX = ship.ShipParts[ship.ShipParts.Count - 1].X;
            int tailY = ship.ShipParts[ship.ShipParts.Count - 1].Y;

            ShipRotation rotation = ship.Rotation;
            if (ship.ShipParts[0].Type == ShipType.Hit)
            {
                if (rotation == ShipRotation.Right || rotation == ShipRotation.Left)
                {
                    if (headX < 9)//Right
                    {
                        int x = headX + 1;
                        int y = headY;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                    if (headX > 0)//Left
                    {
                        int x = headX - 1;
                        int y = headY;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                }
                else
                {
                    if (headY < 9)//Down
                    {
                        int x = headX;
                        int y = headY + 1;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                    if (headY > 0)//Up
                    {
                        int x = headX;
                        int y = headY - 1;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                }
            }

            if (ship.ShipParts[ship.ShipParts.Count - 1].Type == ShipType.Hit)
            {
                if (rotation == ShipRotation.Right || rotation == ShipRotation.Left)//Rigth to Left
                {
                    if (tailX < 9)//Right
                    {
                        int x = tailX + 1;
                        int y = tailY;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                    if (tailX > 0)//Left
                    {
                        int x = tailX - 1;
                        int y = tailY;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                }
                else
                {
                    if (tailY < 9)//Down
                    {
                        int x = tailX;
                        int y = tailY + 1;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                    if (tailY > 0)//Up
                    {
                        int x = tailX;
                        int y = tailY - 1;
                        SquareEntity square = OpponentPlaygroud.Squares[x, y];
                        if (square.Type != ShipType.Hit)
                        {
                            squares.Add(square);
                        }
                    }
                }
            }

            foreach (SquareEntity square in ship.ShipParts)
            {
                if (square.Type != ShipType.Hit && squares.Contains(square) == false)
                {
                    squares.Add(square);
                }
            }

            return squares;
        }

        private List<SquareEntity> CreatePriorityList(int x, int y)
        {
            List<SquareEntity> squares = new List<SquareEntity>();

            if (x < 9)//Right
            {
                SquareEntity square = OpponentPlaygroud.Squares[x + 1, y];
                if (square.Type != ShipType.Hit)
                {
                    squares.Add(square);
                }
            }
            if (y < 9)//Down
            {
                SquareEntity square = OpponentPlaygroud.Squares[x, y + 1];
                if (square.Type != ShipType.Hit)
                {
                    squares.Add(square);
                }
            }
            if (x > 0)//Left
            {
                SquareEntity square = OpponentPlaygroud.Squares[x - 1, y];
                if (square.Type != ShipType.Hit)
                {
                    squares.Add(square);
                }
            }
            if (y > 0)//Up
            {
                SquareEntity square = OpponentPlaygroud.Squares[x, y - 1];
                if (square.Type != ShipType.Hit)
                {
                    squares.Add(square);
                }
            }
            return squares;
        }

        private void AddShip(int shipLength)
        {
            int x = _random.Next(0, 10);
            int y = _random.Next(0, 10);
            if (AddShip(shipLength, x, y, ShipType.Hidden))
            {
                int rotation = _random.Next(0, 4);
                for (int i = 0; i < rotation; i++)
                {
                    RotateShip(PlayerPlayground.Squares[x, y]);
                }
            }
        }

        public override void PlaceShips()
        {
            while (PlayerPlayground.Ships.Count != 1)
            {
                AddShip(4);
            }
            while (PlayerPlayground.Ships.Count != 3)
            {
                AddShip(3);
            }
            while (PlayerPlayground.Ships.Count != 6)
            {
                AddShip(2);
            }
            while (PlayerPlayground.Ships.Count != 10)
            {
                AddShip(1);
            }
        }

        public override void ShowShips()
        {
            foreach (ShipEntity ship in PlayerPlayground.Ships)
            {
                foreach (SquareEntity square in ship.ShipParts)
                {
                    if (square.Type != ShipType.Hit)
                    {
                        square.SetState(ShipType.Visible, square.Part, square.Rotation);
                    }
                }
            }
        }
    }
}
