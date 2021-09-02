using Battleship.Entities;
using Battleship.SinglePlayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Battleship.UI
{
    public partial class Battleship : Form
    {
        public static int ComponentSize { get; set; }
        public List<Button> ControlButtons { get; set; }
        public SquareEntity LastPressedSquare { get; set; }
        public PlaygroundEntity PlaygroundPlayer { get; set; }
        public PlaygroundEntity PlaygroundOppenent { get; set; }
        public IPlayerController PlayerController { get; set; }
        public IOpponentController OpponentController { get; set; }
        public int SelectedLength { get; set; }
        public RichTextBox HistoryBox { get; set; }
        public List<Label> ShipLabels { get; set; }
        public int[] AIShips { get; set; }

        public Battleship()
        {
            InitializeComponent();

            FullScreen();
            ComponentSize = AdaptScreenSize();
            ControlButtons = FactoryUIComponents.CreateControlButtons(this);
            PlaygroundPlayer = FactoryUIComponents.CreatePlayground(this, false, 0, ComponentSize);
            PlaygroundOppenent = FactoryUIComponents.CreatePlayground(this, true, 12 * ComponentSize, ComponentSize);
            PlaygroundOppenent.Enabled(false);
            PlayerController = FactoryInstances.CreatePlayerController(PlaygroundPlayer, PlaygroundOppenent);
            OpponentController = FactoryInstances.CreateOpponentController(PlaygroundOppenent, PlaygroundPlayer);

            LastPressedSquare = null;

            this.KeyPreview = true;
            this.KeyDown += Menu;
        }

        private void FullScreen()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private int AdaptScreenSize()
        {
            Screen screenFormIsOn = Screen.FromControl(this);
            var width = screenFormIsOn.WorkingArea.Width;
            var height = screenFormIsOn.WorkingArea.Height;
            int x = (int)width / 23;
            int y = (int)height / 12;
            return Math.Min(x, y);
        }

        public void PlayerPlayground_Click(object sender, EventArgs e)
        {
            LastPressedSquare = (SquareEntity)sender;
            if (SelectedLength != 0)
            {
                if (PlayerController.AddShip(SelectedLength, LastPressedSquare.X, LastPressedSquare.Y, ShipType.Visible))
                {
                    CheckReady();
                    foreach (Button button in ControlButtons)
                    {
                        if (button.FlatAppearance.BorderColor == Color.Red)
                        {
                            int value = Convert.ToInt32(button.Text);
                            button.Text = "" + (--value);

                            if (value == 0)
                            {
                                button.PerformClick();
                                button.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        public void OpponentPlayground_Click(object sender, EventArgs e)
        {
            PlaygroundOppenent.Enabled(false);
            SquareEntity square = sender as SquareEntity;
            LastPressedSquare = square;
            if (square.Fire(50))
            {
                InformPlayer();
                if (PlayerController.AttackTurn() == Finished.PlayerWin)
                {
                    PlaygroundOppenent.Enabled(false);
                    PlaygroundPlayer.Enabled(false);
                    FactoryUIComponents.Finish(this, Finished.PlayerWin);
                    SendKeys.Send("{ESCAPE}");
                }
            }
            else
            {
                if (OpponentController.AttackTurn() == Finished.OpponentWin)
                {
                    PlaygroundOppenent.Enabled(false);
                    PlaygroundPlayer.Enabled(false);
                    FactoryUIComponents.Finish(this, Finished.OpponentWin);
                    SendKeys.Send("{ESCAPE}");
                    OpponentController.ShowShips();
                }
                else
                {
                    PlayerController.AttackTurn();
                }
            }
        }

        public void Admiral_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (color == Color.Empty)
            {
                button.FlatAppearance.BorderColor = Color.Red;
                button.FlatAppearance.BorderSize = 3;
                SelectedLength = 4;
            }
            else
            {
                button.FlatAppearance.BorderColor = Color.Empty;
                button.FlatAppearance.BorderSize = 1;
                SelectedLength = 0;
            }
        }

        public void Cruiser_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (color == Color.Empty)
            {
                button.FlatAppearance.BorderColor = Color.Red;
                button.FlatAppearance.BorderSize = 3;
                SelectedLength = 3;
            }
            else
            {
                button.FlatAppearance.BorderColor = Color.Empty;
                button.FlatAppearance.BorderSize = 1;
                SelectedLength = 0;
            }
        }

        public void Destroyer_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (color == Color.Empty)
            {
                button.FlatAppearance.BorderColor = Color.Red;
                button.FlatAppearance.BorderSize = 3;
                SelectedLength = 2;
            }
            else
            {
                button.FlatAppearance.BorderColor = Color.Empty;
                button.FlatAppearance.BorderSize = 1;
                SelectedLength = 0;
            }
        }

        public void Submarine_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (color == Color.Empty)
            {
                button.FlatAppearance.BorderColor = Color.Red;
                button.FlatAppearance.BorderSize = 3;
                SelectedLength = 1;
            }
            else
            {
                button.FlatAppearance.BorderColor = Color.Empty;
                button.FlatAppearance.BorderSize = 1;
                SelectedLength = 0;
            }
        }

        public void Rotate_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (LastPressedSquare != null)
            {
                PlayerController.RotateShip(LastPressedSquare);
            }

        }

        public void Delete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Color color = button.FlatAppearance.BorderColor;
            foreach (Button b in ControlButtons)
            {
                if (b == button)
                {
                    continue;
                }
                if (b.FlatAppearance.BorderColor == Color.Red)
                {
                    b.PerformClick();
                }
            }
            if (LastPressedSquare != null)
            {
                int value = PlayerController.DeleteShip(LastPressedSquare);
                CheckReady();
                LastPressedSquare = null;
                if (value != 0)
                {
                    value = Math.Abs(value - 4);
                    int number = Convert.ToInt32(ControlButtons[value].Text);
                    ControlButtons[value].Text = "" + (++number);
                    if (number == 1)
                    {
                        ControlButtons[value].Enabled = true;
                    }
                }
            }
        }

        private void CheckReady()
        {
            if (PlaygroundPlayer.Ships.Count == 10)
            {
                ControlButtons[6].Enabled = true;
            }
            else
            {
                ControlButtons[6].Enabled = false;
            }
        }

        public void Start_Click(object sender, EventArgs e)
        {
            PlaygroundPlayer.Enabled(false);
            for (int i = 4; i < 7; i++)
            {
                ControlButtons[i].Enabled = false;
            }
            OpponentController.PlaceShips();
            Random r = new Random();
            int turn = r.Next(0, 2);
            if (turn == 0)
                PlayerController.AttackTurn();
            else
                OpponentController.AttackTurn();
        }

        private void InformPlayer()
        {
            int length = LastPressedSquare.ShipBelongs.ShipParts.Count;
            string newLine = "";
            switch (length)
            {
                case 4:
                    if (LastPressedSquare.ShipBelongs.IsSank())
                    {
                        newLine = Resources.Strings.SunkAdmiral;
                        ShipLabels[0].Text = Resources.Strings.Admiral + " (XXXX): " + (--AIShips[0]);
                        if (AIShips[0] == 0)
                        {
                            Font font = new Font(ShipLabels[0].Font, FontStyle.Strikeout);
                            ShipLabels[0].Font = font;
                        }
                    }
                    else
                    {
                        newLine = Resources.Strings.HitAdmiral;
                    }
                    break;
                case 3:
                    if (LastPressedSquare.ShipBelongs.IsSank())
                    {
                        newLine = Resources.Strings.SunkCruiser;
                        ShipLabels[1].Text = Resources.Strings.Cruiser + " (XXX): " + (--AIShips[1]);
                        if (AIShips[1] == 0)
                        {
                            Font font = new Font(ShipLabels[1].Font, FontStyle.Strikeout);
                            ShipLabels[1].Font = font;
                        }
                    }
                    else
                    {
                        newLine = Resources.Strings.HitCruiser;
                    }
                    break;
                case 2:
                    if (LastPressedSquare.ShipBelongs.IsSank())
                    {
                        newLine = Resources.Strings.SunkDestroyer;
                        ShipLabels[2].Text = Resources.Strings.Destroyer + " (XX): " + (--AIShips[2]);
                        if (AIShips[2] == 0)
                        {
                            Font font = new Font(ShipLabels[2].Font, FontStyle.Strikeout);
                            ShipLabels[2].Font = font;
                        }
                    }
                    else
                    {
                        newLine = Resources.Strings.HitDestroyer;
                    }
                    break;
                case 1:
                    if (LastPressedSquare.ShipBelongs.IsSank())
                    {
                        newLine = Resources.Strings.SunkSubmarine;
                        ShipLabels[3].Text = Resources.Strings.Submarine + " (X): " + (--AIShips[3]);
                        if (AIShips[3] == 0)
                        {
                            Font font = new Font(ShipLabels[3].Font, FontStyle.Strikeout);
                            ShipLabels[3].Font = font;
                        }
                    }
                    else
                    {
                        newLine = Resources.Strings.HitSubmarine;
                    }
                    break;
            }
            HistoryBox.AppendText(newLine + "\n");
        }

        public void Menu(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormControlMenu form2 = new FormControlMenu(this);
                form2.ShowDialog();
            }
        }
    }
}
