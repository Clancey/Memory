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
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

namespace Memory
{
	public class CardView : UIView
	{
		public int Row;
		public int Column;
		public Card Card {get;private set;}
		private UIView BackView;
		private UIView DisplyView;
		bool isFlipped;
		public bool Locked;
		public CardView (Card card, string display) : base()
		{
			Card = card;
			this.BackgroundColor = UIColor.White;
			this.AddGestureRecognizer(new UITapGestureRecognizer(Tapped));
			BackView = new UIView(){BackgroundColor = UIColor.Blue};
			DisplyView = new UILabel(){
				Text = display,
				BackgroundColor = UIColor.White,
				TextColor = UIColor.Black,
				AdjustsFontSizeToFitWidth = true,
				Font = UIFont.BoldSystemFontOfSize(100f),
				TextAlignment = UITextAlignment.Center,
			};
			this.AddSubview(BackView);
		}
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			BackView.Frame = this.Bounds;
			DisplyView.Frame = this.Bounds;
		}
		
		private void Tapped(UITapGestureRecognizer gesture)
		{
			if(isFlipped || !Game.CurrentGame.CanFlip())
				return;
			Game.CurrentGame.StartedFlipping();
			Flip(true);
			
				
		}
		public void Flip(bool faceUp)
		{
			if(Locked || isFlipped == faceUp)
				return;
			UIView.BeginAnimations ("Flipper");
			UIView.SetAnimationDuration (1.25);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("flippingFinished"));
			
			if (!isFlipped) {
				UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromRight, this, true);
				BackView.RemoveFromSuperview();
				this.AddSubview (DisplyView);
			
			} else {
				UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromLeft, this, true);
				DisplyView.RemoveFromSuperview ();
				this.AddSubview (BackView);
			}
			UIView.CommitAnimations ();
			
			isFlipped = faceUp;
		}
		[Export("flippingFinished")]
		private void Finished()
		{
			if(isFlipped)
				Game.CurrentGame.CardFliped(this);
		}
		
		
		public virtual bool Matches(CardView cardView)
		{
			return this.Card == cardView.Card;
		}
	}
}

