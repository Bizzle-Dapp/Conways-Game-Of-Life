using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;


//
//  This is one of the first functional programs I ever wrote.
//  Updated 31/12/2019 because: https://tvtropes.org/pmwiki/pmwiki.php/Main/OldShame
//
namespace Conway_s_Game_Of_Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _appTimer = new DispatcherTimer();
        private ColumnDefinition[] _gridColumn;
        private RowDefinition[] _gridRow;
        private LivingCell[] _cell;
        
        private int _columnCount;
        private int _rowCount;
        private int _numberOfCells;

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("WELCOME TO Bizzle_Dapp's version of Conway's Game of Life." +
                            Environment.NewLine +
                            "To begin, enter how many rows by columns you'd like for your grid, " +
                            "then click 'Generate'. Once you have a grid to work with, click on Cells to set them to alive. " +
                            "Once you've created your starting colony Start the application with the 'Start/Stop' " +
                            "button and watch the colony live it's life...");
        }

        //Start / Stop program button
        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (lblStartStop.Content.ToString() == "Offline")
            {
                //Initiate Timer
                _appTimer.Tick += new EventHandler(TimerExecute);
                _appTimer.Interval = new TimeSpan(0, 0, 2);
                
                //Switch on
                lblStartStop.Content = "Online";
                // This is how i should have done it from the start: cell[21].CellState(true);
                // This is how i was doing it before: cell[22].IsAlive = true;
                // I wrote the CellState(bool) method and never used it!

                //Start Timer
                _appTimer.Start();
            }
            else
            {
                //Stop Timer
                _appTimer.Stop();

                //Switch off
                lblStartStop.Content = "Offline";
                for (var c = 0; c < _numberOfCells; c++)
                {
                    _cell[c].IsAlive = false;
                    _cell[c].LifeValue = 0;
                }
            }
        }

        //The main logic of the program
        public void LivingApplication()
        {
            _columnCount = Convert.ToInt32(tbColumnCount.Text);
            _rowCount = Convert.ToInt32(tbRowCount.Text);
            _numberOfCells = _columnCount * _rowCount;

            // Initialise
            for (var c = 0; c < _numberOfCells; c++)
            {
                _cell[c].LifeValue = 0;
            }
            
            //Check each LivingCell in all directions to determine it's total value of adjacent life
            for (var c = 0; c < _numberOfCells; c++)
            {
                var isColumnIndexStart = false;
                var isColumnIndexEnd = false;

                if (c / _rowCount == 0)
                { isColumnIndexStart = true; }
                if (c / _rowCount == _rowCount)
                { isColumnIndexEnd = true; }

                try
                {
                    // Above and Left node is (c - (_rowCount + 1))
                    // But consider isColumnIndexStart
                    if (!isColumnIndexStart)
                    {
                        if (_cell[(c - (_rowCount + 1))].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }

                try
                {
                    // Above is (c - _columnCount)
                    if (_cell[(c - _columnCount)].IsAlive) _cell[c].LifeValue += 1;
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }

                try
                {
                    // Above and Right node is (c - (_rowCount - 1))
                    // But consider isColumnIndexEnd
                    if (!isColumnIndexEnd)
                    {
                        if(_cell[(c - (_rowCount - 1))].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }

                try
                {
                    // Left node is (c - 1)
                    // But consider isColumnIndexStart
                    if (!isColumnIndexStart)
                    {
                        if(_cell[(c - 1)].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }

                try
                {
                    // Right node is (c + 1)
                    // But consider isColumnIndexEnd
                    if (!isColumnIndexEnd)
                    {
                        if(_cell[(c + 1)].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }
                try
                {
                    // Bottom and Left node is ( c + (_rowCount - 1))
                    // But consider isColumnIndexStart
                    if (!isColumnIndexStart)
                    {
                        if(_cell[(c + (_rowCount - 1))].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }
                try
                {
                    // Bottom node is (c + _columnCount)
                    if (_cell[(c + _columnCount)].IsAlive) _cell[c].LifeValue += 1;
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }
                try
                {
                    // Bottom and Right node is (c + (_rowCount + 1))
                    // But consider isColumnIndexEnd
                    if (!isColumnIndexEnd)
                    {
                        if(_cell[(c + (_rowCount + 1))].IsAlive) _cell[c].LifeValue += 1;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    // Edge Cell - consider this a dead neighbor
                }
            }

            //Alter each cell based on its LifeValue
            for (var c = 0; c < _numberOfCells; c++)
            {
                if (_cell[c].IsAlive == true)
                //LivingCell.IsALive = true
                //If LivingCell has <2 Living Neighbors set IsAlive to false
                //If LivingCell has 2 or 3 Living Neighbors remain unchanged
                //If LivingCell has >3 Living Neighbors set IsAlive to false
                {
                    if(_cell[c].LifeValue < 2)
                    {
                        _cell[c].CellState(false);
                    }
                    if(_cell[c].LifeValue == 2 && _cell[c].LifeValue == 3)
                    {
                       //Cell lives on unchanged!  
                    }
                    if(_cell[c].LifeValue > 3)
                    {
                        _cell[c].CellState(false);
                    }
                }
                //else
                //If LivingCell.IsAlive = false,  
                //If 3 neighboring LivingCells IsAlive = true, set IsAlive to true
                else
                {
                    if(_cell[c].LifeValue == 3)
                    {
                        _cell[c].CellState(true);
                    }
                }
            }
        }

        public void TimerExecute(object source, EventArgs e)
        {
            LivingApplication();
        }

        public void GenerateCells()
        {
            //Take input to create the grid and generate number of cells and their container
            _columnCount = Convert.ToInt32(tbColumnCount.Text);
            _rowCount = Convert.ToInt32(tbRowCount.Text);
            _numberOfCells = _columnCount * _rowCount;
            _cell = new LivingCell[_numberOfCells];
            _gridColumn = new ColumnDefinition[_columnCount];
            _gridRow = new RowDefinition[_rowCount];

            // Create cells in cell[] and default to IsActive = False; LifeValue = 0;
            for (var c = 0; c < _numberOfCells; c++)
            {
                _cell[c] = new LivingCell
                {
                    IsAlive = false,
                    LifeValue = 0
                };
            }

            //Create rows and columns for the gridColumn[] and gridRow[]
            for (var row = 0; row < _rowCount; row++)
            {
                _gridRow[row] = new RowDefinition();
                CellGrid.RowDefinitions.Add(_gridRow[row]);
            }
            for (var column = 0; column < _columnCount; column++)
            {
                _gridColumn[column] = new ColumnDefinition();
                CellGrid.ColumnDefinitions.Add(_gridColumn[column]);

            }

            // Initialise cells, assigning LivingCell[] to the gridColumn/Rows and providing LivingCell.RowNumber and ColumnNumber
            var counter1 = 0;
            for (var row = 0; row < _rowCount; row++)
            {
                for (var column = 0; column < _columnCount; column++)
                {
                    Grid.SetColumn(_cell[counter1], column);
                    Grid.SetRow(_cell[counter1], row);
                    CellGrid.Children.Add(_cell[counter1]);
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
