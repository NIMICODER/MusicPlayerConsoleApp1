namespace MusicPlayerConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MusicPlayer myApp = new MusicPlayer();
            Playlist playlist = new Playlist();
            Application application = new(myApp, playlist);
            application.RunApp();
        }
    }
}