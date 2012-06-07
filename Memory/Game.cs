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
using System.Collections.Generic;

namespace Memory
{
	public class Game
	{
		public static Game CurrentGame;
		public int Turns {get;private set;}
		public BoardView GameView;
		public Game (BoardView gameView)
		{
			GameView = gameView;
			CurrentGame = this;
		}
		
		CardView FlippedCard;
		public void CardFliped(CardView card)
		{
			if(FlippedCard == null)
			{
				FlippedCard = card;
				return;
			}
			if(card.Matches(FlippedCard))
			{
				Score ();
				FlippedCard.Locked = true;
				card.Locked = true;
				GameView.MatchedCards.Add(FlippedCard);
				GameView.MatchedCards.Add(card);
				
				GameView.RemainingCards.Remove(FlippedCard);
				GameView.RemainingCards.Remove(card);
			}
			else
			{
				NotMatched();
				FlippedCard.Flip(false);
				card.Flip(false);
			}
			
			FlippedCard = null;	
			flipped = 0;
		}
		int flipped;
		public void StartedFlipping()
		{
			flipped ++;
		}
		public bool CanFlip()
		{
			return flipped <= 1;
		}
		int score  = 0;
		int tries = 0 ;
		public void Score()
		{
			score += 50;
			GameView.SetScore(score);
		}
		public void NotMatched()
		{
			tries ++;
			GameView.SetTried(tries);
		}
		
		
	}
}

