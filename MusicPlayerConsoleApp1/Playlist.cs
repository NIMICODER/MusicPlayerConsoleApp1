using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerConsoleApp1
{
    internal class Playlist
    {
        public string PlaylistTitle { get; set; }
        public List<Music> PlaylistSongs { get; set; } = new List<Music>();

        private string PlaylistLength()
        {
            throw new NotImplementedException();
        }
    }
}
