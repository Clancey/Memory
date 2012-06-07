// 
//  Copyright 2012  James Clancey, Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Memory
{
	public class BoardViewController : UIViewController
	{
		Game BoardGame;
		BoardView gameView;
		UIScrollView scrollView;
		public BoardViewController ()
		{
			
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			gameView = new BoardView();
			gameView.Frame = new System.Drawing.RectangleF(0,0,960,640);
			scrollView = new UIScrollView();
			scrollView.AddSubview(gameView);
			scrollView.BackgroundColor = UIColor.DarkGray;
			scrollView.MaximumZoomScale = 1f;
			scrollView.MinimumZoomScale = .5f;
			scrollView.ViewForZoomingInScrollView = delegate{
				return gameView;	
			};
			scrollView.ContentSize = gameView.Frame.Size;
			scrollView.SetZoomScale(.5f,false);
			BoardGame = new Game(gameView);
			
			this.View = scrollView;
			gameView.StartGame(new List<Card>{
				new Card{Value1 = "1",Value2 = "1"},
				new Card{Value1 = "2",Value2 = "2"},
				new Card{Value1 = "3",Value2 = "3"},
				new Card{Value1 = "4",Value2 = "4"},
				new Card{Value1 = "5",Value2 = "5"},
				new Card{Value1 = "6",Value2 = "6"},
				new Card{Value1 = "7",Value2 = "7"},
				new Card{Value1 = "8",Value2 = "8"},
				new Card{Value1 = "9",Value2 = "9"},
				new Card{Value1 = "10",Value2 = "10"},
				new Card{Value1 = "11",Value2 = "11"},
				new Card{Value1 = "12",Value2 = "12"},
				new Card{Value1 = "13",Value2 = "13"},
				new Card{Value1 = "14",Value2 = "14"},
				new Card{Value1 = "15",Value2 = "15"},
			});
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

