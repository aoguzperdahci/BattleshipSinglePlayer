using Battleship.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Battleship.UI
{
    public static class FactoryUIComponents
    {
        public static List<Button> CreateControlButtons(Battleship form)
        {
            int height;
            int width;
            int x = Battleship.ComponentSize;
            int y = 0;
            List<Button> ControlButtons = new List<Button>();
            int fontSize = 15;
            Font labelFont;
            Font btnFont = new Font("Rockwell", fontSize, FontStyle.Regular);

            while (true)
            {
                labelFont = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular);
                Size[] sizse = { TextRenderer.MeasureText(Resources.Strings.Admiral, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Cruiser, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Destroyer, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Submarine, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Rotate, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Delete, labelFont),
                                TextRenderer.MeasureText(Resources.Strings.Start, labelFont) };

                int maxIndex = 0;
                for (int i = 1; i < sizse.Length; i++)
                {
                    if (sizse[i].Width > sizse[maxIndex].Width)
                    {
                        maxIndex = i;
                    }
                }

                Size maxSize = sizse[maxIndex];

                width = maxSize.Width;
                height = maxSize.Height;

                if (width < x)
                    break;
                else
                    fontSize--;
            }


            Size labelSize = new Size(x, height);
            Size btnSize = new Size(x - height, x - height);
            Size btnImageSize = new Size(x - (int)(height * 1.5), x - (int)(height * 1.5));

            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.Location = new Point(x * (i + 2), y + 5);
                label.Font = labelFont;
                label.ForeColor = Color.Blue;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.AutoSize = false;
                label.Size = labelSize;

                Button btnControl = new Button();
                btnControl.Location = new Point((x * (i + 2)) + (height / 2), y + height + 5);
                btnControl.Size = btnSize;
                btnControl.AutoSize = false;
                btnControl.FlatStyle = FlatStyle.Flat;
                btnControl.FlatAppearance.BorderSize = 1;
                btnControl.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnControl.FlatAppearance.MouseOverBackColor = Color.Transparent;
                ControlButtons.Add(btnControl);

                switch (i)
                {
                    case 0:
                        label.Text = Resources.Strings.Admiral;
                        btnControl.Text = "1";
                        btnControl.Font = btnFont;
                        btnControl.Click += new EventHandler(form.Admiral_Click);
                        break;
                    case 1:
                        label.Text = Resources.Strings.Cruiser;
                        btnControl.Text = "2";
                        btnControl.Font = btnFont;
                        btnControl.Click += new EventHandler(form.Cruiser_Click);
                        break;
                    case 2:
                        label.Text = Resources.Strings.Destroyer;
                        btnControl.Text = "3";
                        btnControl.Font = btnFont;
                        btnControl.Click += new EventHandler(form.Destroyer_Click);
                        break;
                    case 3:
                        label.Text = Resources.Strings.Submarine;
                        btnControl.Text = "4";
                        btnControl.Font = btnFont;
                        btnControl.Click += new EventHandler(form.Submarine_Click);
                        break;
                    case 4:
                        label.Text = Resources.Strings.Rotate;
                        btnControl.Image = FactoryImages.CreateRotateImage(btnImageSize);
                        btnControl.Click += new EventHandler(form.Rotate_Click);
                        break;
                    case 5:
                        label.Text = Resources.Strings.Delete;
                        btnControl.Image = FactoryImages.CreateDeleteImage(btnImageSize);
                        btnControl.Click += new EventHandler(form.Delete_Click);
                        break;
                    case 6:
                        label.Text = Resources.Strings.Start;
                        btnControl.Image = FactoryImages.CreateStartImage(btnImageSize);
                        btnControl.Click += new EventHandler(form.Start_Click);
                        btnControl.Enabled = false;
                        break;
                }
                form.Controls.Add(label);
                form.Controls.Add(btnControl);
            }

            List<Label> shipLabels = new List<Label>();
            form.ShipLabels = shipLabels;
            int[] aIShips = new int[4];
            form.AIShips = aIShips;
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label();
                label.Location = new Point(x * 20, y + (i * height) + 5);
                label.Font = labelFont;
                label.AutoSize = true;
                label.ForeColor = Color.Red;
                label.TextAlign = ContentAlignment.MiddleLeft;
                shipLabels.Add(label);
                aIShips[i] = i + 1;
                switch (i)
                {
                    case 0:
                        label.Text = Resources.Strings.Admiral + " (XXXX): " + aIShips[i];
                        break;
                    case 1:
                        label.Text = Resources.Strings.Cruiser + " (XXX): " + aIShips[i];
                        break;
                    case 2:
                        label.Text = Resources.Strings.Destroyer + " (XX): " + aIShips[i];
                        break;
                    case 3:
                        label.Text = Resources.Strings.Submarine + " (X): " + aIShips[i];
                        break;
                }
                form.Controls.Add(label);
            }

            RichTextBox textBox = new RichTextBox();
            textBox.Location = new Point(x * 17, y + 5);
            textBox.Size = new Size(x * 3, x);
            textBox.Font = labelFont;
            textBox.ForeColor = Color.Red;
            textBox.ReadOnly = true;
            form.HistoryBox = textBox;
            form.HistoryBox.Cursor = Cursors.Arrow;
            form.HistoryBox.BorderStyle = BorderStyle.None;
            form.HistoryBox.BackColor = Color.LightGray;
            form.Controls.Add(textBox);
            return ControlButtons;
        }


        public static PlaygroundEntity CreatePlayground(Battleship form, bool isOpponent, int startingX, int startingY)
        {
            PlaygroundEntity playground = new PlaygroundEntity();
            int x = Battleship.ComponentSize;
            int y = Battleship.ComponentSize;
            int width;
            int height;
            Color color;
            EventHandler @event;
            if (isOpponent)
            {
                color = Color.Red;
                @event = form.OpponentPlayground_Click;
            }
            else
            {
                color = Color.Blue;
                @event = form.PlayerPlayground_Click;

            }

            Font font = new Font("Rockwell", (int)(y / 4), FontStyle.Regular);
            for (int i = 0; i < 11; i++)
            {
                height = startingY + i * y;

                for (int j = 0; j < 11; j++)
                {
                    width = startingX + j * x;
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    else if (i == 0)
                    {
                        Label label = new Label();
                        label.Location = new Point(width, height);
                        label.Size = new Size(x, y);
                        label.AutoSize = false;
                        label.Text = "" + (j);
                        label.Font = font;
                        label.ForeColor = color;
                        label.TextAlign = ContentAlignment.BottomCenter;
                        form.Controls.Add(label);
                    }
                    else if (j == 0)
                    {
                        Label label = new Label();
                        label.Location = new Point(width, height);
                        label.Size = new Size(x, y);
                        label.AutoSize = false;
                        label.Text = "" + (char)(64 + i);
                        label.Font = font;
                        label.ForeColor = color;
                        label.TextAlign = ContentAlignment.MiddleRight;
                        form.Controls.Add(label);
                    }
                    else
                    {
                        SquareEntity square = FactoryInstances.SquareInitialze(j - 1, i - 1);
                        playground.Squares[j - 1, i - 1] = square;
                        square.Location = new Point(width, height);

                        form.Controls.Add(square);
                        square.Click += @event;
                    }
                }
            }
            return playground;
        }

        public static void Finish(Battleship form, Finished finished)
        {
            Label label = new Label();
            label.AutoSize = false;
            label.Location = new Point(Battleship.ComponentSize * 10, 15);
            label.Font = new Font("Microsoft Sans Serif", Battleship.ComponentSize / 3, FontStyle.Regular);
            label.Size = new Size(Battleship.ComponentSize * 4, Battleship.ComponentSize);
            label.TextAlign = ContentAlignment.MiddleCenter;
            if (finished == Finished.PlayerWin)
            {
                label.Text = Resources.Strings.Won;
                label.ForeColor = Color.Blue;
            }
            else
            {
                label.Text = Resources.Strings.Lost;
                label.ForeColor = Color.Red;
            }
            form.Controls.Add(label);
        }
    }
}
