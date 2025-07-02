using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Bingo
{
    class HomePage : ContentPage
    {

        Button StartButton;
        Label timeLabel;
        Entry username;
        StackLayout stackLayout;
        AbsoluteLayout absoluteLayout;
        public const string UsernamePropertyName = "Username";

        public HomePage()
        {
            //int intRatio = 205;
            int HeightRatio = 30;
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.BackgroundImage = "ip5s_home.png";//"Home_iphone_vertical.png";//"iPhone_Backgound_Vertical.jpg";
                this.Padding = new Thickness(20, Device.OnPlatform(260, 0, 0), 20, 0);
            }
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.BackgroundImage ="ipad_home.png"; //"Home_ipad_Vertical.png";//"iPhone_Backgound_Vertical.jpg";
                //this.Padding = new Thickness(50, Device.OnPlatform(472, 0, 0), 50, 0);
                this.Padding = new Thickness(80, Device.OnPlatform(480, 0, 0), 80, 0); 
                HeightRatio = 60;
            }
            username = new Entry
            {
                Placeholder = "Username",
                WidthRequest = 20,
                HeightRequest =HeightRatio,
                
            };
            //username.Font = UIFont.FromName("Helvetica-Bold", 20f);
            //username.Width = username.Width / 2;   
            //username.SetBinding(Entry.TextProperty, HomePage.UsernamePropertyName) ;
            //username.AnchorX = 10;  
            //layout.Children.Add(username);

            
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
              
            //};
            NavigationPage.SetHasNavigationBar(this, false);
            // AbsoluteLayout to host the squares.
            absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // This is the "Randomize" button.
            StartButton = new Button
            {
                Text = "Start",
                WidthRequest = 3 ,
                TextColor = Color.White,
                HeightRequest = HeightRatio, 
                BackgroundColor = Color.FromHex("77D065")
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };
            StartButton.Clicked += OnStartButtonClicked;
             
            // Label to display elapsed time.
            timeLabel = new Label
            {
                //Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            
            
            // Put everything in a StackLayout.
            stackLayout = new StackLayout
            {
                Children = 
                {

                    new StackLayout
                    {
                        //VerticalOptions = LayoutOptions.FillAndExpand,
                        //HorizontalOptions = LayoutOptions.FillAndExpand,
                        //Spacing = 20, Padding = 50,
                        //VerticalOptions = LayoutOptions.Center,
                        Children = 
                        {
                            username, 
                            StartButton
                        }
                    },
                      
                }
            //    Children = {
            //new Entry { Placeholder = "Username" },
            ////new Entry { Placeholder = "Password", IsPassword = true },
            //new Button {
            //    Text = "Start",
            //    TextColor = Color.White,
            //    BackgroundColor = Color.FromHex("77D065") }
                            
            //    }
            };
            stackLayout.SizeChanged += OnStackSizeChanged;
             
            // And set that to the content of the page.
            

            //

            this.Content = stackLayout;



        }

        void OnStackSizeChanged(object sender, EventArgs args)
        {
            double width = stackLayout.Width;
            double height = stackLayout.Height;

            if (width <= 0 || height <= 0)
                return;

            // Orient StackLayout based on portrait/landscape mode.
            stackLayout.Orientation = (width < height) ? StackOrientation.Vertical : StackOrientation.Horizontal;

        }



        
        void OnStartButtonClicked(object sender, EventArgs args)
        {
            try
            {
                
                Button button = (Button)sender;
                button.IsEnabled = false;
                if (string.IsNullOrEmpty(username.Text))
                {
                    // var answer = DisplayAlert("Bingo!", "Please Enter Name","","");
                    button.IsEnabled = true;
                    return;
                }
                var todoPage = new BingoPage();
                this.Navigation.PushModalAsync(todoPage);
                button.IsEnabled = true;
            }
            catch (Exception ex)
            {
                var answer = DisplayAlert("Bingo!", ex.Message , "ok", "");
            }

        }



    }
}
