using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Conway_s_Game_Of_Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer appTimer = new DispatcherTimer();
        ColumnDefinition[] gridColumn = new ColumnDefinition[10];
        RowDefinition[] gridRow = new RowDefinition[10];
        LivingCell[] cell = new LivingCell[100];
        SolidColorBrush Alive = new SolidColorBrush(Colors.Black);
        SolidColorBrush Dead = new SolidColorBrush(Colors.WhiteSmoke);
        Int32 columnCount;
        Int32 rowCount;
        Int32 numberOfCells;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Start / Stop program button
        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {

            if (lblStartStop.Content.ToString() == "Offline")
            {
                //Initiate Timer
                appTimer.Tick += new EventHandler(TimerExecute);
                appTimer.Interval = new TimeSpan(0, 0, 2);
                
                //Switch on
                lblStartStop.Content = "Online";
                cell[21].IsAlive = true;
                cell[22].IsAlive = true;
                cell[23].IsAlive = true;
                cell[27].IsAlive = true;
                cell[28].IsAlive = true;
                cell[29].IsAlive = true;
                cell[30].IsAlive = true;
                cell[31].IsAlive = true;
                cell[32].IsAlive = true;
                cell[33].IsAlive = true;

                //Start Timer
                appTimer.Start();
            }
            else
            {
                //Stop Timer
                appTimer.Stop();

                //Switch off
                lblStartStop.Content = "Offline";
                for (var c = 0; c < numberOfCells; c++)
                {
                    cell[c].IsAlive = false;
                    cell[c].LifeValue = 0;
                }
                CheckLifeState();
            }
        }

        //The main logic of the program
        public void LivingApplication()
        {
            columnCount = Convert.ToInt32(tbColumnCount.Text);
            rowCount = Convert.ToInt32(tbRowCount.Text);
            numberOfCells = columnCount * rowCount;

            // Initialise
            for (var c = 0; c < numberOfCells; c++)
            {
                cell[c].LifeValue = 0;
            }
            

            //Check each LivingCell in all directions to determine it's total value of adjacent life
            for (var c = 0; c < numberOfCells; c++)
            {
                var isColumnIndexStart = false;
                var isColumnIndexEnd = false;
                var isRowIndexStart = false;
                var isRowIndexEnd = false;

                if(c % rowCount == 0)
                { isColumnIndexStart = true; }
                if(c + 1 % rowCount == 0)
                { isColumnIndexEnd = true; }
                if (c / rowCount == 0)
                { isRowIndexStart = true; }
                if (c / rowCount == rowCount)
                { isRowIndexEnd = true; }

                //c - rowCount - 1 = TopLeftCell(Except Row 0 && Column 0)
                if (isColumnIndexStart == false && isRowIndexStart == false)
                {
                    if (cell[c - rowCount - 1].IsAlive == true)
                    {
                        cell[c].LifeValue += 1;
                    }
                    else
                    {
                        //TopLeftCell is not alive
                    }
                }
                //c - rowCount = TopCell (Except Row 0 )
                if (isRowIndexStart == false)
                {
                    if (cell[c - rowCount].IsAlive == true)
                    {
                        cell[c].LifeValue += 1;
                    }
                    else
                    {
                        //TopCell is not alive
                    }
                }
                //c - rowCount + 1 = TopRightCell (Except Row 0 && Column == ColumnCount)
                if (isRowIndexStart == false && isColumnIndexEnd == false)
                {
                    if (cell[c - rowCount + 1].IsAlive == true)
                    {
                        cell[c].LifeValue += 1;
                    }
                    else
                    {
                        //TopRightCell is not alive
                    }
                }
                //c - 1 = LeftCell (Except Column 0)
                if (isColumnIndexStart == false)
                {
                    if (cell[c - 1].IsAlive == true)
                    {
                        cell[c].LifeValue += 1;
                    }
                    else
                    {
                        //LeftCell is not alive
                    }
                }
                //c + 1 = RightCell (Except Column == ColumnCount)
                if (isColumnIndexEnd == false)
                {
                    if (c < numberOfCells - columnCount)
                    {
                        if (cell[c + 1].IsAlive == true)
                        {
                            cell[c].LifeValue += 1;
                        }
                        else
                        {
                            //RightCell is not alive
                        }
                    }
                }
                //c + rowCount - 1 = BottomLeftCell (Except Row == RowCount && Column 0 )
                if (isColumnIndexStart == false && isRowIndexEnd == false)
                {
                    if (c < numberOfCells - columnCount - 1)
                    {
                        if (cell[c + rowCount - 1].IsAlive == true)
                        {
                            cell[c].LifeValue += 1;
                        }
                        else
                        {
                            //BottomLeftell is not alive
                        }
                    }
                }
                //c + rowCount = BottomCell (Except Row == RowCount)
                if (isRowIndexEnd == false)
                {
                    if (c < numberOfCells - columnCount - 1)
                    {
                        if (cell[c + rowCount].IsAlive == true)
                        {
                            cell[c].LifeValue += 1;
                        }
                        else
                        {
                            //BottomCell is not alive
                        }
                    }
                }
                //c + rowCount + 1 = BottomRightCell (Except Column == ColumnCount && Row == RowCount)
                if (isColumnIndexEnd == false && isRowIndexEnd == false)
                {
                    if(c < numberOfCells - columnCount - 1 )
                    {
                        if (cell[c + rowCount + 1].IsAlive == true)
                        {
                            cell[c].LifeValue += 1;
                        }
                        else
                        {
                            //BottomRightCell is not alive
                        }
                    }
                }

            }

            //Alter each cell based on its LifeValue
            for (var c = 0; c < numberOfCells; c++)
            {
                if (cell[c].IsAlive == true)
                //LivingCell.IsALive = true
                //If LivingCell has <2 Living Neighbors set IsAlive to false
                //If LivingCell has 2 or 3 Living Neighbors remain unchanged
                //If LivingCell has >3 Living Neighbors set IsAlive to false
                {
                    if(cell[c].LifeValue < 2)
                    {
                        cell[c].IsAlive = false;
                    }
                    if(cell[c].LifeValue == 2 && cell[c].LifeValue == 3)
                    {
                       //Cell lives on unchanged!  
                    }
                    if(cell[c].LifeValue > 3)
                    {
                        cell[c].IsAlive = false;
                    }
                }
                //else
                //If LivingCell.IsAlive = false,  
                //If 3 Neightboring LivingCells IsAlive = true, set IsAlive to true
                else
                {
                    if(cell[c].LifeValue == 3)
                    {
                        cell[c].IsAlive = true;
                    }
                }
            }

        }

        // Checks the IsAlive bool of cell and returns Alive or Dead color
        public void CheckLifeState()
        {
            foreach (var c in cell)
            {
                if (c.IsAlive == true)
                {
                    c.Background = Alive;
                }
                else
                {
                    c.Background = Dead;
                }
            }
        }


        public void TimerExecute(Object source, EventArgs e)
        {
            CheckLifeState();
            LivingApplication();
        }

        public void GenerateCells()
        {

            //Take input to create the grid and generate number of cells and their container
            columnCount = Convert.ToInt32(tbColumnCount.Text);
            rowCount = Convert.ToInt32(tbRowCount.Text);
            numberOfCells = columnCount * rowCount;
            cell = new LivingCell[numberOfCells];
            ColumnDefinition[] gridColumn = new ColumnDefinition[columnCount];
            RowDefinition[] gridRow = new RowDefinition[rowCount];

            // Create cells in cell[] and default to IsActive = False; LifeValue = 0;
            for (var c = 0; c < numberOfCells; c++)
            {
                cell[c] = new LivingCell();
                cell[c].IsAlive = false;
                cell[c].LifeValue = 0;
            }

            //Create rows and columns for the gridColumn[] and gridRow[]
            for (var row = 0; row < rowCount; row++)
            {
                gridRow[row] = new RowDefinition();
                CellGrid.RowDefinitions.Add(gridRow[row]);
            }
            for (var column = 0; column < columnCount; column++)
            {
                gridColumn[column] = new ColumnDefinition();
                CellGrid.ColumnDefinitions.Add(gridColumn[column]);

            }

            // Initialise cells, assigning LivingCell[] to the gridColumn/Rows and providing LivingCell.RowNumber and ColumnNumber
            var counter1 = 0;
            for (var row = 0; row < rowCount; row++)
            {
                for (var column = 0; column < columnCount; column++)
                {
                    Grid.SetColumn(cell[counter1], column);
                    Grid.SetRow(cell[counter1], row);
                    CellGrid.Children.Add(cell[counter1]);
                    counter1++;
                }
            }
        }

        private void tbColumnCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            { Convert.ToInt32(tbColumnCount.Text); }
            catch
            { tbColumnCount.Text = ""; }
        }
        private void tbRowCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            { Convert.ToInt32(tbRowCount.Text); }
            catch
            { tbRowCount.Text = ""; }
        }

        private void btnCreateGrid_Click(object sender, RoutedEventArgs e)
        {
            GenerateCells();
        }
    }

    
    
}
