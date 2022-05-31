using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewGameChakers
{
    public class Board
    {
        private int Rows;
        private int Columns;
        private bool isWhitesTurn = true;
        public Brush BlockColor { get; set; } = Brushes.Chocolate;
        public Brush FigureColor { get; set; } = Brushes.White;
        public List<Point> WhiteFigures { get; set; } = new();
        public List<Point> BlackFigures { get; set; } = new();
        public Label[,] BlockControls { get; set; }

        Point[] possWhiteFigure =
        {
                new Point(0, 5),
                new Point(2, 5),
                new Point(4, 5),
                new Point(6, 5),
                new Point(1, 6),
                new Point(3, 6),
                new Point(5, 6),
                new Point(7, 6),
                new Point(0, 7),
                new Point(2, 7),
                new Point(4, 7),
                new Point(6, 7)
        };


        Grid MyGrid;
        public Board(Grid BaseGrid)
        {
            MyGrid = BaseGrid;
            Rows = BaseGrid.RowDefinitions.Count;
            Columns = BaseGrid.ColumnDefinitions.Count;
            DrawCheckerBoard(BaseGrid);
        }

        private void ChocolateBoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickedBoardSquare = (Label)sender;
            if (clickedBoardSquare.Background == Brushes.Red)
            {
                var selectedEllipse = MyGrid.GetGreenEllipse();
                var col = Grid.GetColumn(clickedBoardSquare);
                var row = Grid.GetRow(clickedBoardSquare);
                Grid.SetRow(selectedEllipse, row);
                Grid.SetColumn(selectedEllipse, col);
                selectedEllipse.Fill = Brushes.White;
                if (isWhitesTurn)
                {
                    selectedEllipse.Fill = Brushes.White;
                    isWhitesTurn = false;
                }
                else
                {
                    selectedEllipse.Fill = Brushes.Black;
                    isWhitesTurn = true;
                }
                clickedBoardSquare.Background = Brushes.Chocolate;

            }
        }

        private void ColorGreenInBoardAllPossibleWhitesTurns(Ellipse selectedEllipse)
        {
            var col = Grid.GetColumn(selectedEllipse);
            var row = Grid.GetRow(selectedEllipse);
            if (col < MyGrid.RowDefinitions.Count - 1 && col >= 1)
            {
                for (int i = 0; i < MyGrid.Children.Count; i++)
                {
                    if (MyGrid.Children[i] is Ellipse)
                    {
                        var ellipse = (Ellipse)MyGrid.Children[i];
                        var newcol = Grid.GetColumn(ellipse);
                        var newrow = Grid.GetRow(ellipse);
                        if (newcol == col + 1 && newrow == row - 1)
                        {
                            BlockControls[row - 1, col + 1].Background = Brushes.Chocolate;
                        }
                        if (newcol == col - 1 && newrow == row - 1)
                        {
                            BlockControls[row - 1, col - 1].Background = Brushes.Chocolate;
                        }
                    }
                    else
                    {
                        BlockControls[row - 1, col + 1].Background = Brushes.Red;
                        BlockControls[row - 1, col - 1].Background = Brushes.Red;
                    }
                }
            }
            else if (col == MyGrid.RowDefinitions.Count - 1)
            {
                for (int i = 0; i < MyGrid.Children.Count; i++)
                {
                    if (MyGrid.Children[i] is Ellipse)
                    {
                        var ellipse = (Ellipse)MyGrid.Children[i];
                        var newcol = Grid.GetColumn(ellipse);
                        var newrow = Grid.GetRow(ellipse);
                        if (newrow == row - 1 && newcol == col - 1)
                        {
                            BlockControls[row - 1, col - 1].Background = Brushes.Chocolate;
                            EatBlackFigure(selectedEllipse);
                        }
                    }
                    else
                    {
                        BlockControls[row - 1, col - 1].Background = Brushes.Red;
                    }
                }
            }
            else if (col == 0)
            {
                for (int i = 0; i < MyGrid.Children.Count; i++)
                {
                    if (MyGrid.Children[i] is Ellipse)
                    {
                        var ellipse = (Ellipse)MyGrid.Children[i];
                        var newcol = Grid.GetColumn(ellipse);
                        var newrow = Grid.GetRow(ellipse);
                        if (newrow == row - 1 && newcol == col + 1)
                        {
                            BlockControls[row - 1, col + 1].Background = Brushes.Chocolate;
                        }
                    }
                    else
                    {
                        BlockControls[row - 1, col + 1].Background = Brushes.Red;
                    }
                }
            }
        }

        public void EatBlackFigure(Ellipse selectedEllipse)
        {
            var row = Grid.GetRow(selectedEllipse);
            var col = Grid.GetColumn(selectedEllipse);
            for (int i = 0; i < MyGrid.Children.Count; i++)
            {
                if (MyGrid.Children[i] is Ellipse)
                {
                    var ellipse = MyGrid.Children[i];
                    var rowRemoveFigureBlack = Grid.GetRow(ellipse);
                    var colRemoveFigureBlack = Grid.GetColumn(ellipse);
                    if (rowRemoveFigureBlack == row - 1 && colRemoveFigureBlack == col - 1||
                        rowRemoveFigureBlack == row - 1 && colRemoveFigureBlack == col+1)
                    {
                        if (BlockControls[rowRemoveFigureBlack-1,colRemoveFigureBlack-1].Content is Ellipse)
                        {
                            BlockControls[rowRemoveFigureBlack - 1, colRemoveFigureBlack - 1].Background = Brushes.Chocolate;
                        }
                        else
                        {
                            BlockControls[rowRemoveFigureBlack - 1, colRemoveFigureBlack - 1].Background = Brushes.Red;
                        }
                    }
                }
            }
        }

        private void ColorGreenInBoardAllPossibleBlackTurns(Ellipse selectedEllipse)
        {
            var col = Grid.GetColumn(selectedEllipse);
            var row = Grid.GetRow(selectedEllipse);
            if (col < MyGrid.RowDefinitions.Count - 1 && col >= 1)
            {
                BlockControls[row + 1, col + 1].Background = Brushes.Red;
                BlockControls[row + 1, col - 1].Background = Brushes.Red;
            }
            else if (col == MyGrid.RowDefinitions.Count - 1)
            {
                BlockControls[row + 1, col - 1].Background = Brushes.Red;
            }
            else if (col == 0)
            {
                BlockControls[row + 1, col + 1].Background = Brushes.Red;
            }
        }


        private void ClickWhiteFigure(object sender, MouseButtonEventArgs e)
        {
            if (isWhitesTurn)
            {
                var selectedEllipse = (Ellipse)sender;
                selectedEllipse.Fill = Brushes.Green;

                for (int i = 0; i < MyGrid.Children.Count; i++)
                {
                    if (MyGrid.Children[i] is Ellipse)
                    {
                        var ellipse = (Ellipse)MyGrid.Children[i];
                        if (ellipse != selectedEllipse)
                        {
                            if (ellipse.Fill != Brushes.Black)
                            {
                                ellipse.Fill = Brushes.White;
                            }
                        }
                    }
                }
                for (int i = 0; i < MyGrid.RowDefinitions.Count; i++)//karmir dasnerna sarqum sev 
                {
                    for (int j = 0; j < MyGrid.ColumnDefinitions.Count; j++)
                    {
                        if (BlockControls[i, j].Background == Brushes.Red)
                        {
                            BlockControls[i, j].Background = Brushes.Chocolate;
                        }
                    }
                }
                ColorGreenInBoardAllPossibleWhitesTurns(selectedEllipse);
                EatBlackFigure(selectedEllipse) ; //
            }
        }

        private void ClickBlackFigure(object sender, MouseButtonEventArgs e)
        {
            if (isWhitesTurn == false)
            {
                var selectedEllipse = (Ellipse)sender;
                selectedEllipse.Fill = Brushes.Green;

                for (int i = 0; i < MyGrid.Children.Count; i++)
                {
                    if (MyGrid.Children[i] is Ellipse)
                    {
                        var ellipse = (Ellipse)MyGrid.Children[i];
                        if (ellipse != selectedEllipse && ellipse.Fill != Brushes.White)
                        {
                            ellipse.Fill = Brushes.Black;
                        }

                    }
                }
                for (int i = 0; i < MyGrid.RowDefinitions.Count; i++)//karmir dashter@ sarqume sev
                {
                    for (int j = 0; j < MyGrid.ColumnDefinitions.Count; j++)
                    {
                        if (BlockControls[i, j].Background == Brushes.Red)
                        {
                            BlockControls[i, j].Background = Brushes.Chocolate;
                        }
                    }
                }
                ColorGreenInBoardAllPossibleBlackTurns(selectedEllipse);
            }
        }

        public void CreateBoarderFigureWhite(Grid BaseGrid)
        {
            for (int i = 0; i < possWhiteFigure.Length; i++)
            {
                Ellipse whiteFigure = new Ellipse();
                whiteFigure.Width = 40;
                whiteFigure.Height = 40;
                whiteFigure.Fill = Brushes.White;
                whiteFigure.StrokeThickness = 1;
                whiteFigure.MouseUp += ClickWhiteFigure;
                Grid.SetRow(whiteFigure, (int)possWhiteFigure[i].Y);
                Grid.SetColumn(whiteFigure, (int)possWhiteFigure[i].X);
                BaseGrid.Children.Add(whiteFigure);
                WhiteFigures.Add(possWhiteFigure[i]);
            }
        }

        public void CreateBoarderFigureBlack(Grid BaseGrid1)
        {
            for (int i = 0; i < possWhiteFigure.Length; i++)
            {
                Ellipse blackFigure = new Ellipse();
                blackFigure.Width = 40;
                blackFigure.Height = 40;
                blackFigure.Fill = Brushes.Black;
                blackFigure.StrokeThickness = 1;
                blackFigure.MouseUp += ClickBlackFigure;

                if ((int)possWhiteFigure[i].X % 2 == 0)
                {
                    Grid.SetColumn(blackFigure, (int)possWhiteFigure[i].X + 1);
                }
                else
                {
                    Grid.SetColumn(blackFigure, (int)possWhiteFigure[i].X - 1);
                }
                Grid.SetRow(blackFigure, (int)possWhiteFigure[i].Y - 5);
                BaseGrid1.Children.Add(blackFigure);
                if ((int)possWhiteFigure[i].X % 2 == 0)
                {
                    BlackFigures.Add(new Point(1 + possWhiteFigure[i].X, -5 + possWhiteFigure[i].Y));
                }
                else
                {
                    BlackFigures.Add(new Point(possWhiteFigure[i].X - 1, -5 + possWhiteFigure[i].Y));
                }
            }
        }

        private void DrawCheckerBoard(Grid BaseGrid)
        {
            BlockControls = new Label[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].BorderBrush = Brushes.Black;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    if ((i + j) % 2 == 1)
                    {
                        BlockControls[i, j].Background = Brushes.Chocolate;
                        BlockControls[i, j].MouseDown += ChocolateBoardGrid_MouseDown;
                    }
                    else
                    {
                        BlockControls[i, j].Background = Brushes.White;
                    }
                    Grid.SetRow(BlockControls[i, j], i);
                    Grid.SetColumn(BlockControls[i, j], j);
                    BaseGrid.Children.Add(BlockControls[i, j]);
                }
            }
        }
    }
}
