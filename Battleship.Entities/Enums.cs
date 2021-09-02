namespace Battleship.Entities
{
    public enum ShipType
    {
        Hidden,
        Visible,
        Empty,
        Hit
    }

    public enum ShipPart
    {
        None,
        Single,
        Head,
        Body
    }

    public enum ShipRotation
    {
        None,
        Right,
        Down,
        Left,
        Up,
    }

    public enum Finished
    {
        Continue,
        PlayerWin,
        OpponentWin
    }
}
