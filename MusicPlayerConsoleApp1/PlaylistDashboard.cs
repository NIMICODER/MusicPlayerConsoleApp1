using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerConsoleApp1
{
    public class PlaylistDashboard
    {
        private readonly PlaylistMenuUtility _editplaylist;
        public PlaylistDashboard() {}
        public PlaylistDashboard(PlaylistMenuUtility _editPlaylist)
        {
            this._editplaylist = _editPlaylist;  
        }

        StringBuilder PlaylistMenuBuilder = new StringBuilder();    

        public void Run()
        {
            DisplayPlaylistMenu();
            var option = Console.ReadLine();    

            while (!string.IsNullOrEmpty(option))
            {
                var isValidOption = int.TryParse(option, out _);
                
                if (!isValidOption)
                {
                    Console.WriteLine("No character or String allowed!");
                    Run();
                }

                switch(Convert.ToInt32(option))
                {
                    case 1:
                        PlaylistDashboardUtility.CreatePlaylist();
                        Run();
                        break;
                    case 2:
                        PlaylistDashboardUtility.ShowAllPlaylist();
                        Run();
                        break;
                    case 3:
                        PlaylistDashboardUtility.DeletePlaylist();
                        Run();
                        break;
                    case 4:
                        _editplaylist.Run();
                        Run();
                        break;
                    case 5:
                        //
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Run();
                        break;
                }

            }
        }


        void DisplayPlaylistMenu()
        {
            PlaylistMenuBuilder.AppendLine($"Welcome to our Playlist Maker");
            PlaylistMenuBuilder.AppendLine("1. Create Playlist");
            PlaylistMenuBuilder.AppendLine("2. View All Playlist");
            PlaylistMenuBuilder.AppendLine("3. Delete Playlist");
            PlaylistMenuBuilder.AppendLine("4. Edit Playlist");
            PlaylistMenuBuilder.AppendLine("5. Main Menu");
            PlaylistMenuBuilder.AppendLine("6. Quit");

            Console.WriteLine(PlaylistMenuBuilder.ToString());
            PlaylistMenuBuilder.Clear();
        }

    }

    internal static class PlaylistDashboardUtility
    {
        public static void CreatePlaylist() 
        {
            Console.WriteLine("Enter your prefered playlist name");
            var playlistName = Console.ReadLine();

            while (String.IsNullOrWhiteSpace(playlistName))
            {
                Console.WriteLine("Playlist name cannot be null or empty space");
                Console.WriteLine("Enter prefered playlist name");
                playlistName = Console.ReadLine();
            }

            while(MusicPlayer.PlaylistDictionary.ContainsKey(playlistName))
            {
                Console.WriteLine($"Playlist with name: {playlistName} exits. Try another!!!");
                playlistName = Console.ReadLine();
            }

              var newPlaylist = new Playlist
            {
                PlaylistTitle = playlistName,
                PlaylistSongs = SelectedPlaylistSongs()
            };

            MusicPlayer.PlaylistDictionary.Add(playlistName, newPlaylist);

            Console.WriteLine($"{playlistName} playlist created");
            Console.WriteLine();

            return;
        }
        public static void ShowAllPlaylist()
        {

            if (MusicPlayer.PlaylistDictionary.Count <= 0)
            {
                Console.WriteLine("You don't have any playlist");
                Console.WriteLine();
                return;
            }

        ChoosePlaylist:

            Console.WriteLine("Available playlist(s)");

            var counter = 1;
            foreach (var kvp in MusicPlayer.PlaylistDictionary)
            {
                Console.WriteLine($"{counter}. {kvp.Key}");
                Console.WriteLine();
                counter++;
            }

            Console.WriteLine("Choose playlist to see songs, q to quit");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(choice)) goto ChoosePlaylist;


            while (!string.IsNullOrWhiteSpace(choice))
            {
                if (choice.Equals("Q") || choice.Equals("q")) break;

                if (!int.TryParse(choice, out int _))
                {
                    Console.WriteLine("Choose a number corresponding to a Playlist.");
                    Console.WriteLine();
                    goto ChoosePlaylist;
                }

                int intChoice = int.Parse(choice);
                int playlistIndex = int.Parse(choice);

                Console.WriteLine($"Music in playlist {MusicPlayer.PlaylistDictionary.ElementAt(--playlistIndex).Key}: ");

                var playlist = MusicPlayer.PlaylistDictionary.ElementAt(--intChoice).Value;

                foreach (var kvp in playlist.PlaylistSongs)
                {
                    Console.WriteLine($"Artist: {kvp.ArtistName}\n Music Title: {kvp.Title}");
                    Console.WriteLine();
                }

                goto ChoosePlaylist;
            }

        }
        public static void DeletePlaylist()
        {
            if (MusicPlayer.PlaylistDictionary.Count <= 0)
            {
                Console.WriteLine("You don't have any playlist");
                Console.WriteLine();
                return;
            }

        ChoosePlaylist:

            Console.WriteLine("Available playlist(s)");

            var counter = 1;
            foreach (var kvp in MusicPlayer.PlaylistDictionary)
            {
                Console.WriteLine($"{counter}. {kvp.Key}");
                counter++;
            }

            Console.WriteLine("Choose playlist to delete, q to quit");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(choice)) goto ChoosePlaylist;


            while (!string.IsNullOrWhiteSpace(choice))
            {
                if (choice.Equals("Q") || choice.Equals("q")) break;

                if (!int.TryParse(choice, out int _))
                {
                    Console.WriteLine("Choose a number corresponding to a Playlist.");
                    Console.WriteLine();
                    goto ChoosePlaylist;
                }

                var intChoice = int.Parse(choice);

                MusicPlayer.PlaylistDictionary.Remove(MusicPlayer.PlaylistDictionary.ElementAt(--intChoice).Key);

                Console.WriteLine("Playlist removed");

                goto ChoosePlaylist;
            }
        }

        static List<Music>SelectedPlaylistSongs()
        {
            var availableSongs = MusicPlayer.MusicList.ToList();
            var playlist = new List<Music>();


        DataEnry:
            Console.WriteLine("Choose number corresponding to music you'd like to add to playlist and press Enter key to add \n" +
            "Press q to quit\n");

            ShowMusic();

            var choice = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(choice)) goto DataEnry;

            while (!string.IsNullOrWhiteSpace(choice))
            {

                if (choice == "q") return playlist;

                if (!int.TryParse(choice, out _))
                {
                    Console.WriteLine("Choose a number corresponding to music and press Enter key.");
                    Console.WriteLine();
                    goto DataEnry;
                }

                var numberChoice = int.Parse(choice);


                playlist.Add(availableSongs[numberChoice]);
                Console.WriteLine("Music added");

                Console.WriteLine();
                availableSongs.RemoveAt(numberChoice);

                goto DataEnry;
            }

            return null;

            void ShowMusic()
            {
                int counter = 0;
                foreach (var songs in availableSongs)
                {
                    Console.WriteLine($"{counter}. {songs.Title}");
                    counter++;
                }

                Console.WriteLine();
            }
        }

    }
}
