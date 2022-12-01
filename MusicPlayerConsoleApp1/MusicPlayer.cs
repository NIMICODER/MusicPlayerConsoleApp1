using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerConsoleApp1
{
    internal class MusicPlayer
    {
        public static List<Music> MusicList { get; set; } = new List<Music>();

        public static Dictionary<string, Playlist> PlaylistDictionary = new Dictionary<string, Playlist>();
        public string AppName { get; set; } = "Genesys Music Player";

        public List<Music> ShuffleMusic()
        {
            var shuffleList = MusicList.ToList();
            var songCount = MusicList.Count;

            var randomSong = new Random();

            //int n = list.Count;
            while (songCount > 1)
            {
                songCount--;

                int randNumber = randomSong.Next(songCount + 1);

                var music = shuffleList[randNumber];
                shuffleList[randNumber] = shuffleList[songCount];
                shuffleList[songCount] = music;

            }

            return shuffleList;
           
        }

        public void AddSongData()
        {
            MusicList.Clear();

            IEnumerable<Music> song2Add = new List<Music>()
            {
                new Music
                {
                    Title = "King's Dead",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Bloody Waters",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Maadd City",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Let Go My Hand",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = "Miss America",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = ".95 South",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = "Appying Pressure",
                    ArtistName = "Jermaine Cole"
                }
            };

            MusicList.AddRange(song2Add);
        }

        public void PlaylistLength()
        {
            throw new NotImplementedException();
        }

        public List<Music> AllSongs() => MusicList.ToList();
    }
}
