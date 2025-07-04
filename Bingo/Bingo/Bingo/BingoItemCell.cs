﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace Bingo
{
    class BingoItemCell: ViewCell	{
        public BingoItemCell()		{			var label = new Label {				YAlign = TextAlignment.Center			};			label.SetBinding (Label.TextProperty, "Name");			var tick = new Image {				Source = FileImageSource.FromFile ("check.png"),			};			tick.SetBinding (Image.IsVisibleProperty, "Done");			var layout = new StackLayout {				Padding = new Thickness(20, 0, 0, 0),				Orientation = StackOrientation.Horizontal,				HorizontalOptions = LayoutOptions.StartAndExpand,				Children = {label, tick}			};			View = layout;		}		protected override void OnBindingContextChanged ()		{			// Fixme : this is happening because the View.Parent is getting 			// set after the Cell gets the binding context set on it. Then it is inheriting			// the parents binding context.			View.BindingContext = BindingContext;			base.OnBindingContextChanged ();		}	}
}
