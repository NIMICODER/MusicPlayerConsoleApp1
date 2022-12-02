using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace MusicPlayerConsoleApp1
{
    internal class Application
    {
        StringBuilder mainMenuBuilder = new StringBuilder();
        private readonly MusicPlayer _musicPlayer;
        private readonly Playlist _playlist;
        PlaylistMenuUtility menuUtility = new PlaylistMenuUtility();
        PlaylistDashboard _playlistDashboard = new();  
        
        public Application()
        {

        }

        public Application(MusicPlayer _musicPlayer, Playlist _playlist)
        {
            this._musicPlayer = _musicPlayer;
            this._musicPlayer.AddSongData();
            this._playlist = _playlist;
            this._playlistDashboard = _playlistDashboard;
        }

        public void RunApp()
        {
            DisplayMainMenu();
            var option = Console.ReadLine();

            while (!string.IsNullOrEmpty(option))
            {
                var isValidOption = int.TryParse(option, out int _);

                if (!isValidOption)
                {
                    Console.WriteLine("No character or String allowed!");
                    RunApp();
                }
                
                switch(Convert.ToInt32(option))
                {
                    case 1:
                        DisplaySongs(_musicPlayer.AllSongs());
                        RunApp();
                        break;
                    case 2:
                        _playlistDashboard.Run();
                        RunApp();
                        break;
                    case 3:
                        DisplaySongs(_musicPlayer.ShuffleMusic());
                        RunApp();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        RunApp();
                        break;

                }
                


            }




        }

        void DisplayMainMenu()
        {
            mainMenuBuilder.AppendLine($"Welcome to {_musicPlayer.AppName}. Choose an option");
            mainMenuBuilder.AppendLine("1. Display all Songs");
            mainMenuBuilder.AppendLine("2. Playlist Dashboard");
            mainMenuBuilder.AppendLine("3. Shuffle");
            mainMenuBuilder.AppendLine("4. Quit");

            Console.WriteLine(mainMenuBuilder.ToString());
            mainMenuBuilder.Clear();
        }

        void DisplaySongs(List<Music> songList)
        {
            if (songList == null) return;

            foreach (var song in songList)
            {
                Console.WriteLine($"Title: {song.Title} \n" +
                   $"Artist: {song.ArtistName}");
                Console.WriteLine();
            }
        }
    }
}
