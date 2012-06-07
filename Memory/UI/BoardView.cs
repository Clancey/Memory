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
using System.Drawing;
using System.Linq;

namespace Memory
{
	public class BoardView : UIView
	{
		List<CardView> AllCards;
		public List<CardView> MatchedCards;
		public List<CardView> RemainingCards;
		UILabel triesLbl;
		UILabel scoreLbl;
		public BoardView ()
		{
			Init();	
		}

		private void Init ()
		{
			AllCards = new List<CardView> ();
			MatchedCards = new List<CardView> ();
			RemainingCards = new List<CardView> ();
			triesLbl = new UILabel(new RectangleF(10,10,150,50)){
				Text = "Tries: 0",
				Font = UIFont.BoldSystemFontOfSize(20),
			};
			scoreLbl = new UILabel(new RectangleF(0,10,150,50)) {
				Text = "Score: 0",
				Font = UIFont.BoldSystemFontOfSize(20)
			};
			this.AddSubview(scoreLbl);
			this.AddSubview(triesLbl);
		}
		public void SetUpBoard()
		{
			UpdateLayoutVariables();
			foreach( var cardView in AllCards)
			{
				this.AddSubview(cardView);
			}
		}
		public override void LayoutSubviews ()
		{
			var frame = scoreLbl.Frame;
			frame.X = this.Bounds.Right - (frame.Width + 10);
			scoreLbl.Frame = frame;
			base.LayoutSubviews ();
			UpdateLayoutVariables ();
			LayoutCards();
		}
		public void StartGame(List<Card> cards)
		{
			foreach(var view in AllCards)
			{
				view.RemoveFromSuperview();
				view.Dispose();
			}
			
			MatchedCards.Clear();
			RemainingCards.Clear();
			AllCards.Clear();
			
			foreach(var card in cards)
			{
				AllCards.Add(new CardView(card,card.Value1));
				AllCards.Add (new CardView(card,card.Value2));
			}
			RemainingCards = AllCards.ToList();
			AllCards.Shuffle();
			SetUpBoard();
		}
		
		float rowHeight;
		float columnWidth;
		private void LayoutCards()
		{
			float width = columnWidth * .8f;
			float height = rowHeight * .8f;
			for(int i = 0; i< AllCards.Count;i++)
			{
				var coord = GetCoordinate(i);
				var center = GridCenter(coord.X,coord.Y);
				AllCards[i].Frame = new RectangleF(0,0,width,height);
				AllCards[i].Center = center;
			}
		}
		
		private void UpdateLayoutVariables ()
		{
			rows = GetNumberOfRows (AllCards.Count);
			columns = GetNumberOfColumns (AllCards.Count);
			columnWidth = this.Bounds.Width / (columns + 1);
			rowHeight = this.Bounds.Height / (rows + 1);
		}
		
		public PointF GridCenter (int column, int row)
		{
			return new PointF ((column + 1) * columnWidth, (row + 1) * rowHeight);
		}
		
		int rows;
		int columns;
		
		private int GetNumberOfRows (int itemCount)
		{
			int row = (int)Math.Floor (Math.Sqrt (itemCount));
			while (itemCount % row != 0)
				row --;
			return row;
		}

		private int GetNumberOfColumns (int itemCount)
		{
			return itemCount / GetNumberOfRows (itemCount);
		}
		
		private Point GetCoordinate (int index)
		{
			var row = (int)Math.Floor((double)(index/columns));
			var column = index % columns;
			
			return new Point(column,row);
		}
		
		public void SetScore(int score)
		{
			scoreLbl.Text = "Score: " + score;
		}
		public void SetTried(int tries)
		{
			triesLbl.Text = "Tries: " + tries;
		}
		
		
	}
	
}

