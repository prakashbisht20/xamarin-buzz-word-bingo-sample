using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bingo
{
    class BingoSquare : ContentView
    {
        Label label;
        string normText;

        public BingoSquare(string normText)
        {
            this.normText = normText;

            // A Frame surrounding two Labels.
            label = new Label
            {
                Text = this.normText,
               // Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center,
                HeightRequest=400,
                WidthRequest=400,
                
            };

            //Label tinyLabel = new Label
            //{
            //    Text = (index + 1).ToString(),
            //    Font = Font.SystemFontOfSize(NamedSize.Micro),
            //    HorizontalOptions = LayoutOptions.End
            //};

            this.Padding = new Thickness(2);

            //this.Content = new Frame
            this.Content = new  StackLayout
            {
                BackgroundColor = Color.White,
                //HorizontalOptions=LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                //Padding = new Thickness(0, 0, 0, 0),
                Children =
                {

                    label 
                    //BackgroundColor=Color.Transparent,
                    //Padding = new Thickness(0, 0, 0, 0),
                     
                    //Content = new StackLayout
                    //new StackLayout
                    //{
                        
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    VerticalOptions = LayoutOptions.Center,
                    //    Spacing = 0,
                    //    Children = 
                    //    {
                    //        label,
                    //        //tinyLabel,
                    //    }
                    //}
                }
            };

            // Don't let touch pass us by.
            //this.BackgroundColor = Color.Transparent;
        }

        // Retain current Row and Col position.

        public int Row { set; get; }
        public int Col { set; get; }
        public bool Selected { set; get; }

        public Font Font
        {
            set
            {
                label.Font = value;
            }
        }
        public Color _BackgroundColor
        {
            get
            {
                return this.Content.BackgroundColor;
            }
            set
            {
                this.Content.BackgroundColor = value;
            }

        }

        public async Task AnimateWinAsync(bool isReverse)
        {
            uint length = 150;
            await Task.WhenAll(this.ScaleTo(3, length), this.RotateTo(180, length));
            label.Text = isReverse ? normText : normText;
            await Task.WhenAll(this.ScaleTo(1, length), this.RotateTo(360, length));
            this.Rotation = 0;
        }
    }
}
