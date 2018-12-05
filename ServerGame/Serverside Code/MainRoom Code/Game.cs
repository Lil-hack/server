using System;
using System.Collections.Generic;
using PlayerIO.GameLibrary;

namespace MushroomsUnity3DExample {

	public class Player : BasePlayer {
		public float posx = 0;
		 public float posy = 0;
        public float posz = 0;



    }

	
	[RoomType("MainRoom")]
	public class GameCode : Game<Player> {

		// This method is called when an instance of your the game is created
		public override void GameStarted() {
          
			// anything you write to the Console will show up in the 
			// output window of the development server
			Console.WriteLine("Game is started: " + RoomId);

			// spawn 10 toads at server start
			
			


		}

		

		
		// This method is called when the last player leaves the room, and it's closed down.
		public override void GameClosed() {
			Console.WriteLine("RoomId: " + RoomId);
		}

		// This method is called whenever a player joins the game
		public override void UserJoined(Player player) {
       

            foreach (Player pl in Players) {
				if(pl.ConnectUserId != player.ConnectUserId) {
					pl.Send("PlayerJoined", player.ConnectUserId, 0, 0,0);
					player.Send("PlayerJoined", pl.ConnectUserId, pl.posx,pl.posy, pl.posz);
				}
			}

			// send current toadstool info to the player
			//foreach(Toad t in Toads) {
			//	player.Send("Toad", t.id, t.posx, t.posz);
			//}
		}

		// This method is called when a player leaves the game
		public override void UserLeft(Player player) {
			Broadcast("PlayerLeft", player.ConnectUserId);
		}

		// This method is called when a player sends a message into the server code
		public override void GotMessage(Player player, Message message) {
			switch(message.Type) {
				// called when a player clicks on the ground
				case "Move":
					player.posx = message.GetFloat(0);
                    player.posy = message.GetFloat(1);
                    player.posz = message.GetFloat(2);
           
                    Broadcast("Move", player.ConnectUserId, player.posx, player.posy, player.posz);
					break;
               
                case "Rotation":
              

                    Broadcast("Rotation", player.ConnectUserId, message.GetFloat(0));
                    break;

                case "Skin":
          
                    Broadcast("Skin", player.ConnectUserId, message.GetInt(0));
                    break;


            }
		}
	}
}