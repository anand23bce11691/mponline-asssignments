using System;
using System.Collections.Generic;

class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Duration { get; set; }

    public Song(string title, string artist, string duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }
}

class PlaylistManager
{
    private List<Song> songs = new List<Song>();

    public void AddSong(string title, string artist, string duration)
    {
        Song newSong = new Song(title, artist, duration);
        songs.Add(newSong);
        Console.WriteLine($"\n[Success] '{title}' added to the playlist.");
    }

    public void DisplayPlaylist()
    {
        Console.WriteLine("\n==================================================");
        Console.WriteLine("                CURRENT PLAYLIST                  ");
        Console.WriteLine("==================================================");

        if (songs.Count == 0)
        {
            Console.WriteLine("The playlist is currently empty.");
            Console.WriteLine("==================================================\n");
            return;
        }

        Console.WriteLine($"{"Index",-8} | {"Song Title",-22} | {"Artist",-15} | {"Duration",-8}");
        Console.WriteLine(new string('-', 60));

        for (int i = 0; i < songs.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]      | {songs[i].Title,-22} | {songs[i].Artist,-15} | {songs[i].Duration,-8}");
        }

        Console.WriteLine("==================================================\n");
    }

    public void RemoveSong(int index)
    {
        if (songs.Count == 0)
        {
            Console.WriteLine("\n[Error] Cannot remove song. Playlist is empty.");
            return;
        }

        if (index >= 1 && index <= songs.Count)
        {
            Song removed = songs[index - 1];
            songs.RemoveAt(index - 1);
            Console.WriteLine($"\n[Success] Song at index {index} ('{removed.Title}') has been removed.");
        }
        else
        {
            Console.WriteLine($"\n[Error] Invalid song index '{index}'. Valid range is 1 to {songs.Count}.");
        }
    }

    public void SearchSong(string title)
    {
        if (songs.Count == 0)
        {
            Console.WriteLine("\n[Info] Playlist is empty.");
            return;
        }

        bool found = false;
        Console.WriteLine($"\nSearch Results for '{title}':");
        for (int i = 0; i < songs.Count; i++)
        {
            if (songs[i].Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($" -> Index [{i + 1}]: Title: \"{songs[i].Title}\" | Artist: {songs[i].Artist} | Duration: {songs[i].Duration}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No songs found matching your search term.");
        }
    }

    public void ClearPlaylist()
    {
        songs.Clear();
        Console.WriteLine("\n[Success] All songs cleared from the playlist.");
    }
}

class Program
{
    static void Main()
    {
        PlaylistManager playlist = new PlaylistManager();

        // Sample initial songs for quick testing experience
        playlist.AddSong("Shape of You", "Ed Sheeran", "3:53");
        playlist.AddSong("Blinding Lights", "The Weeknd", "3:20");
        playlist.AddSong("Kesariya", "Arijit Singh", "4:28");

        while (true)
        {
            Console.WriteLine("================ PLAYLIST CREATOR MENU ================");
            Console.WriteLine("1. View Playlist (Show Song Index & Title)");
            Console.WriteLine("2. Add New Song to Playlist");
            Console.WriteLine("3. Remove Song by Index");
            Console.WriteLine("4. Search Song by Title");
            Console.WriteLine("5. Clear All Songs");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.\n");
                continue;
            }

            if (choice == 6)
            {
                Console.WriteLine("Exiting Playlist Application. Keep enjoying music!");
                break;
            }

            switch (choice)
            {
                case 1:
                    playlist.DisplayPlaylist();
                    break;

                case 2:
                    Console.Write("\nEnter Song Title: ");
                    string title = Console.ReadLine() ?? "Untitled";

                    Console.Write("Enter Artist Name: ");
                    string artist = Console.ReadLine() ?? "Unknown Artist";

                    Console.Write("Enter Duration (e.g. 3:45): ");
                    string duration = Console.ReadLine() ?? "0:00";

                    playlist.AddSong(title, artist, duration);
                    break;

                case 3:
                    playlist.DisplayPlaylist();
                    Console.Write("Enter Index of song to remove: ");
                    int idx;
                    if (int.TryParse(Console.ReadLine(), out idx))
                    {
                        playlist.RemoveSong(idx);
                    }
                    else
                    {
                        Console.WriteLine("Invalid numeric index.");
                    }
                    break;

                case 4:
                    Console.Write("\nEnter Song Title to search: ");
                    string searchTerm = Console.ReadLine() ?? "";
                    playlist.SearchSong(searchTerm);
                    break;

                case 5:
                    playlist.ClearPlaylist();
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }
}
