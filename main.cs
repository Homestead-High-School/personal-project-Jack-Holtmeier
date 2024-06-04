using Godot;
using System;

public partial class main : Node2D
{
	private string[] nums = {"2","3","4","5","6","7","8","9","10","Jack","Queen","King", "Ace"};
	private string[] suits = {"♥","♦","♣","♠"};
	private string[,] deck = new string[4,13];
	private int playerCount;
	private int dealerCount;
	private int newCard;
	private bool playerBust;
	private bool dealerBust;
	private bool playerAce;
	private bool dealerAce;
	private int dealerCard;
	private int money = 1000;
	private bool playerBlackjack;
	private bool dealerBlackjack;
	private bool win;
	private bool dd;
	
	private void OnStartButtonPressed()
	{
		for (int i = 0; i<4; i++){
			for(int j = 0; j<13; j++){
				deck[i,j] = nums[j];
			}
		}
		GetNode<Button>("StartButton").Hide();
		GetNode<Button>("Deal").Show();
		GetNode<Node2D>("Player").Hide();
		GetNode<Node2D>("Dealer").Hide();
		GetNode<ColorRect>("newCard1").Hide();
		GetNode<ColorRect>("newCard2").Hide();
		GetNode<ColorRect>("newDealerCard1").Hide();
		GetNode<ColorRect>("newDealerCard2").Hide();
		GetNode<Label>("end").Hide();
		GetNode<Label>("PlayerScore").Hide();
		GetNode<Label>("DealerScore").Hide();
		playerCount = 0;
		dealerCount = 0;
		newCard = 0;
		playerBust = false;
		dealerBust = false;
		playerAce = false;
		dealerAce = false;
		GetNode<Label>("PlayerScore").Text = playerCount.ToString();
		GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		dealerCard = 0;
		GetNode<ColorRect>("newCard3").Hide();
		GetNode<ColorRect>("newDealerCard3").Hide();
		GetNode<ColorRect>("newCard4").Hide();
		GetNode<ColorRect>("newDealerCard4").Hide();
		playerBlackjack = false;
		dealerBlackjack = false;
		win = false;
		dd = false;
	}
	
	private void OnDealPressed()
	{
		money -= 100;
		GetNode<Label>("Money").Text = money.ToString();
		var player = GetNode<Node2D>("Player");
		var dealer = GetNode<Node2D>("Dealer");
		player.Show();
		GetNode<Button>("Deal").Hide();
		dealer.Show();
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Hide();
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Hide();
		Random rnd = new Random();
		int x = rnd.Next(13); 
		if(x <= 8)
		{
			playerCount += x+2;
		}
		else if(x <= 11)
		{
			playerCount += 10;
		}
		else
		{
			playerAce = true;
			playerCount += 11;
		}
		int y = rnd.Next(4); 
		GetNode<Label>("Player/ColorRect1/Label1").Text = nums[x];
		GetNode<Label>("Player/ColorRect1/Label3").Text = suits[y];
		deck[y,x] = "0";
		do
		{
			x = rnd.Next(13);
			y = rnd.Next(4);	
		}
		while(deck[y,x] == "0");
		if(x <= 8)
		{
			playerCount += x+2;
		}
		else if(x <= 11)
		{
			playerCount += 10;
		}
		else
		{
			playerAce = true;
			if(playerCount + 11 > 21)
			{
				playerCount++;
			}
			else
			{
				playerCount += 11;
			}
		}
		GetNode<Label>("Player/ColorRect2/Label2").Text = nums[x];
		GetNode<Label>("Player/ColorRect2/Label4").Text = suits[y];
		deck[y,x] = "0";
		do
		{
			x = rnd.Next(13);
			y = rnd.Next(4);	
		}
		while(deck[y,x] == "0");
		if(x <= 8)
		{
			dealerCount += (x+2);
		}
		else if(x <= 11)
		{
			dealerCount += 10;
		}
		else
		{
			dealerAce = true;
			dealerCount += 11;
		}
		GetNode<Label>("Dealer/ColorRectdealer1/Labeldealer1").Text = nums[x];
		GetNode<Label>("Dealer/ColorRectdealer1/Labeldealer3").Text = suits[y];
		deck[y,x] = "0";
		do
		{
			x = rnd.Next(13);
			y = rnd.Next(4);	
		}
		while(deck[y,x] == "0");
		if(x <= 8)
		{
			dealerCount += (x+2);
		}
		else if(x <= 11)
		{
			dealerCount += 10;
		}
		else
		{
			dealerAce = true;
			if(dealerCount + 11 > 21)
			{
				dealerCount++;
			}
			else
			{
				dealerCount += 11;
			}
		}
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Text = nums[x];
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Text = suits[y];
		deck[y,x] = "0";
		if (playerCount == 21)
		{
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
			playerBlackjack = true;
			endGame();
		}
		else if(dealerCount == 21)
		{
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
			dealerBlackjack = true;
			endGame();
		}
		else
		{
			play();
		}
	}
	
	public void play(){
		var hit = GetNode<Button>("Hit");
		var stay = GetNode<Button>("Stay");
		var doubleDown = GetNode<Button>("Double down");
		var split = GetNode<Button>("Split");
		hit.Show();
		stay.Show();
		doubleDown.Show();
	}
	
	private void OnHitPressed()
	{
		GetNode<Button>("Double down").Hide();
		newCard ++;
		Random rnd = new Random();
		int x;
		int y;
		if(newCard == 1)
		{
			GetNode<ColorRect>("newCard1").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				playerCount += x+2;
			}
			else if(x <= 11)
			{
				playerCount += 10;
			}
			else
			{
				playerCount += 11;
				playerAce = true;
			}
			GetNode<Label>("newCard1/newCardLabel").Text = nums[x];
			GetNode<Label>("newCard1/newCardLabel2").Text = suits[y];
			deck[y,x] = "0";
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
			if (playerCount == 21)
			{
				if(dealerCount>17)
				{
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
					endGame();
				}
				else
				{
					dealerTurn();
				}
			}
		}
		else if(newCard == 2)
		{
			GetNode<ColorRect>("newCard2").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				playerCount += x+2;
			}
			else if(x <= 11)
			{
				playerCount += 10;
			}
			else
			{
				playerCount += 11;
				playerAce = true;
			}
			GetNode<Label>("newCard2/newCardLabel3").Text = nums[x];
			GetNode<Label>("newCard2/newCardLabel4").Text = suits[y];
			deck[y,x] = "0";
			if (playerCount == 21)
			{
				if(dealerCount>17)
				{
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
					endGame();
				}
				else
				{
					dealerTurn();
				}
			}
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		}
		else if(newCard == 3)
		{
			GetNode<ColorRect>("newCard3").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				playerCount += x+2;
			}
			else if(x <= 11)
			{
				playerCount += 10;
			}
			else
			{
				playerCount += 11;
				playerAce = true;
			}
			GetNode<Label>("newCard3/newCardLabel5").Text = nums[x];
			GetNode<Label>("newCard3/newCardLabel6").Text = suits[y];
			deck[y,x] = "0";
			if (playerCount == 21)
			{
				if(dealerCount>17)
				{
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
					endGame();
				}
				else
				{
					dealerTurn();
				}
			}
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		}
		else if(newCard == 4)
		{
			GetNode<ColorRect>("newCard4").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				playerCount += x+2;
			}
			else if(x <= 11)
			{
				playerCount += 10;
			}
			else
			{
				playerCount += 11;
				playerAce = true;
			}
			GetNode<Label>("newCard4/newCardLabel7").Text = nums[x];
			GetNode<Label>("newCard4/newCardLabel8").Text = suits[y];
			deck[y,x] = "0";
			if (playerCount == 21)
			{
				if(dealerCount>17)
				{
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
					GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
					endGame();
				}
				else
				{
					dealerTurn();
				}
			}
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		}
		if(playerCount > 21)
		{
			if(playerAce == true)
			{
				playerCount -= 10;
				playerAce = false;
			}
			else
			{
				playerBust = true;
				GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
				GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
				endGame();
			}
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		}
	}
	
	private void OnDoubleDownPressed()
	{
		dd = true;
		money -= 100;
		Random rnd = new Random();
		int x;
		int y;
		GetNode<ColorRect>("newCard1").Show();
		do
		{
			x = rnd.Next(13);
			y = rnd.Next(4);	
		}
		while(deck[y,x] == "0");
		if(x <= 8)
		{
			playerCount += x+2;
		}
		else if(x <= 11)
		{
			playerCount += 10;
		}
		else
		{
			playerCount += 11;
			playerAce = true;
		}
		GetNode<Label>("newCard1/newCardLabel").Text = nums[x];
		GetNode<Label>("newCard1/newCardLabel2").Text = suits[y];
		deck[y,x] = "0";
		if(playerCount > 21 && playerAce == true)
			{
				playerCount -= 10;
				playerAce = false;
			}
			GetNode<Label>("PlayerScore").Text = playerCount.ToString();
			GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		
		if(playerCount > 21){
			playerBust = true;
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
			endGame();
		}
		else 
		{
			OnStayPressed();
		}
	}
	
	private void OnStayPressed()
	{
		if(dealerCount < 17)
		{
			dealerTurn();
		}
		else
		{
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
			GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
			endGame();
		}
	}
	
	private void dealerTurn(){
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer2").Show();
		GetNode<Label>("Dealer/ColorRectdealer2/Labeldealer4").Show();
		Random rnd = new Random();
		int x;
		int y;
		GetNode<ColorRect>("newDealerCard1").Show();
		do
		{
			x = rnd.Next(13);
			y = rnd.Next(4);	
		}
		while(deck[y,x] == "0");
		if(x <= 8)
		{
			dealerCount += (x+2);
		}
		else if(x <= 11)
		{
			dealerCount += 10;
		}
		else
		{
			dealerAce = true;
			dealerCount += 11;
		}
		GetNode<Label>("newDealerCard1/newDealerLabel1").Text = nums[x];
		GetNode<Label>("newDealerCard1/newDealerLabel2").Text = suits[y];
		deck[y,x] = "0";
		if(dealerCount > 21 && dealerAce == true)
		{
			dealerCount -= 10;
			dealerAce = false;
		}
		else if(dealerCount >= 17)
		{
			if(dealerCount > 21)
			{
				dealerBust = true;
			}
			endGame();
		}
		else
		{
			dealerCard++;
		}
		if(dealerCard == 1)
		{
			GetNode<ColorRect>("newDealerCard2").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				dealerCount += (x+2);
			}
			else if(x <= 11)
			{
				dealerCount += 10;
			}
			else
			{
				dealerCount += 11;
				dealerAce = true;
			}
			GetNode<Label>("newDealerCard2/newDealerLabel3").Text = nums[x];
			GetNode<Label>("newDealerCard2/newDealerLabel4").Text = suits[y];
			deck[y,x] = "0";
		}
		if(dealerCount > 21 && dealerAce == true)
		{
			dealerCount -= 10;
			dealerAce = false;
			dealerCard++;
		}
		else if(dealerCount >= 17)
		{
			if(dealerCount > 21)
			{
				dealerBust = true;
			}
			endGame();
		}
		else
		{
			dealerCard++;
		}
		if(dealerCard == 2)
		{
			GetNode<ColorRect>("newDealerCard3").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				dealerCount += (x+2);
			}
			else if(x <= 11)
			{
				dealerCount += 10;
			}
			else
			{
				dealerCount += 11;
				dealerAce = true;
			}
			GetNode<Label>("newDealerCard3/newDealerLabel5").Text = nums[x];
			GetNode<Label>("newDealerCard3/newDealerLabel6").Text = suits[y];
			deck[y,x] = "0";
		}
		if(dealerCount > 21 && dealerAce == true)
		{
			dealerCount -= 10;
			dealerAce = false;
			dealerCard++;
		}
		else if(dealerCount >= 17)
		{
			if(dealerCount > 21)
			{
				dealerBust = true;
			}
			endGame();
		}
		else
		{
			dealerCard++;
		}
		if(dealerCard == 3)
		{
			GetNode<ColorRect>("newDealerCard4").Show();
			do
			{
				x = rnd.Next(13);
				y = rnd.Next(4);	
			}
			while(deck[y,x] == "0");
			if(x <= 8)
			{
				dealerCount += (x+2);
			}
			else if(x <= 11)
			{
				dealerCount += 10;
			}
			else
			{
				dealerCount += 11;
				dealerAce = true;
			}
			GetNode<Label>("newDealerCard4/newDealerLabel7").Text = nums[x];
			GetNode<Label>("newDealerCard4/newDealerLabel8").Text = suits[y];
			deck[y,x] = "0";
		}
		if(dealerCount > 21 && dealerAce == true)
		{
			dealerCount -= 10;
			dealerAce = false;
			dealerCard++;
		}
		else if(dealerCount >= 17)
		{
			if(dealerCount > 21)
			{
				dealerBust = true;
			}
			endGame();
		}
	}
	
	private void endGame(){
		GetNode<Label>("PlayerScore").Text = playerCount.ToString();
		GetNode<Label>("DealerScore").Text = dealerCount.ToString();
		GetNode<Label>("PlayerScore").Show();
		GetNode<Label>("DealerScore").Show();
		GetNode<Button>("Hit").Hide();
		GetNode<Button>("Stay").Hide();
		GetNode<Button>("Double down").Hide();
		GetNode<Label>("end").Show();
		if(playerBlackjack == true && dealerBlackjack == false)
		{
			GetNode<Label>("end").Text = "Blackjack!";
			if(dd == true)
			{
				money += 100;
			}
			else
			{
				money += 50;
			}
		}
		if(playerBlackjack == false && dealerBlackjack == true)
		{
			GetNode<Label>("end").Text = "Dealer Blackjack!";
		}
		else if(playerBust == true)
		{
			GetNode<Label>("end").Text = "You Busted!";
		}
		else if(dealerBust == true)
		{
			GetNode<Label>("end").Text = "You Win!";
			if(dd == true)
			{
				money += 100;
			}
			else
			{
				money += 50;
			}
		}
		else if(playerCount > dealerCount && dealerBust == false)
		{
			GetNode<Label>("end").Text = "You win!";
			if(dd == true)
			{
				money += 100;
			}
			else
			{
				money += 50;
			}
		}
		else if(playerCount < dealerCount)
		{
			GetNode<Label>("end").Text = "You Lose!";
		}
		else if(playerCount == dealerCount)
		{
			win = false;
			GetNode<Label>("end").Text = "You Push!";
			if(dd == true)
			{
				money += 200;
			}
			else
			{
				money += 100;
			}
		}
		GetNode<Label>("Money").Text = money.ToString();
		GetNode<Button>("StartButton").Text = "Play Again";
		GetNode<Button>("StartButton").Show();
	}
}
