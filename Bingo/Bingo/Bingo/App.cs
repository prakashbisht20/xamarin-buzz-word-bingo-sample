using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Bingo
{
    public class App
    {
        ////public static Page GetMainPage()
        ////{
        ////    return new HomePage();  //BingoPage();
        ////}


        public static Page GetMainPage()
        {
            var label = new Label { Text = "content" };

            return new NavigationPage(new HomePage());//BingoPage());

            //return new NavigationPage(new ContentPage
            //{
            //    Title = "mypage",
            //    ToolbarItems = {
            //    new ToolbarItem {
            //        Name = "Save",
            //        Command = new Command (() => {
            //            label.Text = "saved";
            //        }),
            //    }
            //},
            //    Content = label,
            //});
        }
    }
}
