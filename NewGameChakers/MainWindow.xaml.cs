using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewGameChakers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board;
        public int count = 0;
        public bool move = true;
        public MainWindow()
        {
            InitializeComponent();
            this.board = new Board(MainGrid);
            board.CreateBoarderFigureBlack(MainGrid);
            board.CreateBoarderFigureWhite(MainGrid);
        }

        //private void KeyMouse(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.OriginalSource!=null)
        //    {
        //        if (move == false && count%2==0)
        //        {
        //            count++;
        //            move = true;
        //        }
        //        else if(move == false && count % 2 == 1 && board.BlockColor == Brushes.Chocolate && board.BlockControls[(int)result.X, (int)result.Y].Background != Brushes.White)
        //        {
        //            count++;
        //            move = true;
        //        }
        //        else if(move == true  && board.BlockColor == Brushes.Chocolate && board.BlockControls[(int)result.X, (int)result.Y].Background != Brushes.White)
        //        {
        //            move = false;
        //        }
        //    }
        //}
       
    }
}
