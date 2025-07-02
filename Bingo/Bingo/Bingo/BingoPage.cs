using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Bingo
{
    class BingoPage : ContentPage
    {
        // Number of squares horizontally and vertically,
        //  but if you change it, some code will break.
        static readonly int NUM = 4;

        // Array of XuzzleSquare views, and empty row & column.
        BingoSquare[,] squares = new BingoSquare[NUM, NUM];
        int emptyRow = NUM - 1;
        int emptyCol = NUM - 1;

        StackLayout stackLayout;
        AbsoluteLayout absoluteLayout;
        Button StartButton;
        Label timeLabel;
        double squareSize;
        bool isBusy;
        bool isPlaying;

        public BingoPage()
        {
            Title = "Buzzword Bingo";

            // AbsoluteLayout to host the squares.
            absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Prepare for tap recognition
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
            {
                TappedCallback = OnSquareTapped
            };


            
            // Create BingoSquare's for all the rows and columns.
            List<string> WordCollection = new List<string>();
            WordCollection.Add("Scalable");
            WordCollection.Add("Risk Management");
            WordCollection.Add("Benchmark");
            WordCollection.Add("Paradigm");
            WordCollection.Add("Review");
            WordCollection.Add("Life Cycle");
            WordCollection.Add("Off-line");
            WordCollection.Add("Proactive");
            WordCollection.Add("Strategy");
            WordCollection.Add("Granular");
            WordCollection.Add("Markets");
            WordCollection.Add("Sales Driven");
            WordCollection.Add("Email");
            WordCollection.Add("Facilitate");
            WordCollection.Add("Timeline");
            WordCollection.Add("Penetration");
            WordCollection.Add("Customer Value");
            WordCollection.Add("Schedule");
            WordCollection.Add("Touch Base");
            WordCollection.Add("Restructuring");
            WordCollection.Add("Drop the Ball");
            WordCollection.Add("R.O.I.");
            WordCollection.Add("Cost");
            WordCollection.Add("Out of the Loop");
            WordCollection.Add("Information");

            int index = 0;
            for (int row = 0; row < NUM; row++)
            {
                for (int col = 0; col < NUM; col++)
                {
                    string nornormText = WordCollection[index];

                    // But skip the last one!//free cell into 5*5
                    //if (row == NUM / 2 && col == NUM / 2)
                    //{
                    //    nornormText = "Free"; //"Buzzword Bingo";
                    //}
                    // Instantiate BingoSquare.
                    BingoSquare square = new BingoSquare(nornormText)
                    {
                        Row = row,
                        Col = col
                    };
                    //free cell into 5*5
                    //if (row == NUM / 2 && col == NUM / 2)
                    //{
                    //    square.Selected = true;
                    //}
                    //if (nornormText != "Free")//unable to select middel square
                    square.GestureRecognizers.Add(tapGestureRecognizer);

                    // Add it to the array and the AbsoluteLayout.
                    squares[row, col] = square;
                    absoluteLayout.Children.Add(square);
                    index++;
                }
            }

            // This is the "Randomize" button.
            StartButton = new Button
            {
                Text = "Start",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            StartButton.Clicked += OnStartButtonClicked;

            // Label to display elapsed time.
            timeLabel = new Label
            {
                //Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.BackgroundImage = "Sheet_iphone.png";//"Sheet_iphone_vertical.png";//"iPhone_Backgound_Vertical.jpg";
            }
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.BackgroundImage = "Sheet_ipad.png";//"Sheet_ipad_Vertical.png";//"iPhone_Backgound_Vertical.jpg";
            }





           

            // Put everything in a StackLayout.
            stackLayout = new StackLayout
            {
                Children = 
                {
                
                    ////new StackLayout
                    ////{
                    ////    VerticalOptions = LayoutOptions.FillAndExpand,
                    ////    HorizontalOptions = LayoutOptions.FillAndExpand,
                    ////    Children = 
                    ////    {
                    ////        StartButton,
                    ////        timeLabel
                    ////    }
                    ////},
                    absoluteLayout
                }
            };
            stackLayout.SizeChanged += OnStackSizeChanged;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.Padding = new Thickness(20, Device.OnPlatform(80, 0, 0), 20, 0);
            }
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.Padding = new Thickness(50, Device.OnPlatform(200, 0, 0), 50, 0);
            }

            // And set that to the content of the page.
            

            //

            this.Content = stackLayout;

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("Settings", "Settings_Icon_32.png", () =>
               {
                   //var todoPage = new BingoPageItems();
                   //Navigation.PushAsync(todoPage);
               }, 0, 0);
            }

            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("Settings", "Settings_Icon_32.png", () =>
                {

                    // Navigation.PushAsync(todoPage);
                }, 0, 0);
            }

            if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                {


                    //Navigation.PushAsync(todoPage);
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);

        }

        void OnStackSizeChanged(object sender, EventArgs args)
        {
            double  intHeightRatio; 
            double width = stackLayout.Width;
            double height = stackLayout.Height;

            if (width <= 0 || height <= 0)
                return;

            // Orient StackLayout based on portrait/landscape mode.
            stackLayout.Orientation = (width < height) ? StackOrientation.Vertical : StackOrientation.Horizontal;

            // Calculate square size and position based on stack size.
            intHeightRatio =((width + height) / 2)/NUM ; 
            squareSize = Math.Min(width, height) / NUM;
            absoluteLayout.WidthRequest = NUM * squareSize;
            absoluteLayout.HeightRequest = NUM * intHeightRatio; //squareSize;
            Font font = Font.BoldSystemFontOfSize(0.1 * squareSize+2);
            // Font font = Font.SystemFontOfSize(8f);
            // Font font = Device.OnPlatform(iOS: Font.OfSize("MarkerFelt-Thin", NamedSize.Micro), Android: Font.OfSize("Droid Sans Mono", NamedSize.Medium), WinPhone: Font.OfSize("Comic Sans MS", NamedSize.Medium));


            foreach (View view in absoluteLayout.Children)
            {
                BingoSquare square = (BingoSquare)view;
                square.Font = font;

                AbsoluteLayout.SetLayoutBounds(square,
                    //new Rectangle(square.Col * squareSize,
                    //              square.Row * squareSize,
                    //              squareSize,
                    //              squareSize));
                    new Rectangle(square.Col * squareSize,
                                  square.Row * intHeightRatio,
                                  squareSize,
                                  intHeightRatio));
            }
        }

        async void OnSquareTapped(View view, object args)
        {
            if (isBusy)
                return;

            isBusy = true;
            BingoSquare tappedSquare = (BingoSquare)view;
            /// Mark As Selected
            if (tappedSquare.Selected)
            {
                tappedSquare.Selected = false;
                tappedSquare._BackgroundColor = Color.White;
            }
            else
            {
                tappedSquare.Selected = true;
                
                tappedSquare._BackgroundColor = Color.Red;
            }


            isBusy = false;


            // Check for Bingo

            int[] RowArray = new int[] { 0, 0, 0, 0 };
            int[] ColArray = new int[] { 0, 0, 0, 0 };
            int[] CrossArray = new int[] { 0, 0 };

            for (int row = 0; row < NUM; row++)
            {
                for (int col = 0; col < NUM; col++)
                {
                    BingoSquare square = squares[row, col];
                    if (square.Selected)
                    {
                        RowArray[row] = RowArray[row] + 1;
                        ColArray[col] = ColArray[col] + 1;

                        if (row == col)
                        {
                            CrossArray[0] = CrossArray[0] + 1;
                        }
                        if (col == (NUM - 1) - row)
                        {
                            CrossArray[1] = CrossArray[1] + 1;
                        }
                    }
                }
            }

            if (RowArray.Where(ra => ra >= 4).Count() > 0 ||
                ColArray.Where(ca => ca >= 4).Count() > 0 ||
                CrossArray.Where(cra => cra >= 4).Count() > 0)
            {
                await DoWinAnimation(1);
                var answer = await DisplayAlert("Bingo!", "Would you like to play again", "Yes", "No");
                //Debug.WriteLine("Answer: " + answer);
                if (answer)
                {
                     
                    //Random rand = new Random();
                    //// Simulate some fast crazy taps.
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    int tappedRow = rand.Next(NUM);
                    //    int tappedCol = rand.Next(NUM);

                    //    if (tappedRow != NUM / 2)
                    //        await ShiftIntoEmpty(tappedRow, emptyCol, 25);

                    //    if (tappedCol != NUM / 2)
                    //        await ShiftIntoEmpty(emptyRow, tappedCol, 25);
                    //}
                    foreach (View v in this.absoluteLayout.Children)
                    {
                        BingoSquare tappedSquareB = (BingoSquare)v;
                        /// Mark As Selected
                        if (tappedSquareB.Selected && tappedSquareB._BackgroundColor == Color.Red)//unable to select middel square
                        {
                            tappedSquareB.Selected = false;
                            tappedSquareB._BackgroundColor = Color.White;

                        }

                    }
                    
                }
                else
                {
                    //await DoWinAnimation(5);
                    var todoPage = new HomePage ();
                    await this.Navigation.PushModalAsync(todoPage);
                }

            }


            if (isPlaying)
            {

            }
        }

        async Task ShiftIntoEmpty(int tappedRow, int tappedCol, uint length = 100)
        {
            // Shift columns.
            if (tappedRow == emptyRow && tappedCol != emptyCol)
            {
                int inc = Math.Sign(tappedCol - emptyCol);
                int begCol = emptyCol + inc;
                int endCol = tappedCol + inc;

                for (int col = begCol; col != endCol; col += inc)
                {
                    await AnimateSquare(emptyRow, col, emptyRow, emptyCol, length);
                }
            }
            // Shift rows.
            else if (tappedCol == emptyCol && tappedRow != emptyRow)
            {
                int inc = Math.Sign(tappedRow - emptyRow);
                int begRow = emptyRow + inc;
                int endRow = tappedRow + inc;

                for (int row = begRow; row != endRow; row += inc)
                {
                    await AnimateSquare(row, emptyCol, emptyRow, emptyCol, length);
                }
            }
        }

        async Task AnimateSquare(int row, int col, int newRow, int newCol, uint length)
        {
            // The Square to be animated.
            BingoSquare animaSquare = squares[row, col];


            // The destination rectangle.
            Rectangle rect = new Rectangle(squareSize * emptyCol,
                                           squareSize * emptyRow,
                                           squareSize,
                                           squareSize);

            // This is the actual animation call.
            await animaSquare.LayoutTo(rect, length);

            // Set several variables and properties for new layout.
            squares[newRow, newCol] = animaSquare;
            animaSquare.Row = newRow;
            animaSquare.Col = newCol;

            emptyRow = row;
            emptyCol = col;
        }

        async void OnStartButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            isBusy = true;

            // Prepare for playing.
            DateTime startTime = DateTime.Now;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    // Round duration and get rid of milliseconds.
                    TimeSpan timeSpan = (DateTime.Now - startTime) +
                                            TimeSpan.FromSeconds(0.5);
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    // Display the duration.
                    if (isPlaying)
                        timeLabel.Text = timeSpan.ToString("t");
                    return isPlaying;
                });
            button.IsEnabled = true;
            isBusy = false;
            this.isPlaying = true;
        }

        async Task DoWinAnimation(int Cycle)
        {
            // Inhibit all input.
            StartButton.IsEnabled = false;
            isBusy = true;

            for (int cycle = 0; cycle < Cycle; cycle++)
            {
                foreach (BingoSquare square in squares)
                {
                    if (square != null && square.Selected)
                        await square.AnimateWinAsync(cycle == 1);
                }

                if (cycle == 0)
                    await Task.Delay(1500);
            }

            // All input.
            StartButton.IsEnabled = true;
            isBusy = false;
        }





        async void OnRandomizeButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            Random rand = new Random();

            isBusy = true;

            // Simulate some fast crazy taps.
            for (int i = 0; i < 100; i++)
            {
                await ShiftIntoEmpty(rand.Next(NUM), emptyCol, 25);
                await ShiftIntoEmpty(emptyRow, rand.Next(NUM), 25);
            }
            button.IsEnabled = true;

            isBusy = false;

            // Prepare for playing.
            DateTime startTime = DateTime.Now;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    // Round duration and get rid of milliseconds.
                    TimeSpan timeSpan = (DateTime.Now - startTime) +
                                            TimeSpan.FromSeconds(0.5);
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    // Display the duration.
                    if (isPlaying)
                        timeLabel.Text = timeSpan.ToString("t");
                    return isPlaying;
                });
            this.isPlaying = true;
        }





    }
}
