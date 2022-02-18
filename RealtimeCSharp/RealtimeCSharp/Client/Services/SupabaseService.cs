using Blazored.LocalStorage;
using RealtimeCSharp.Shared.Models;
using Supabase.Realtime;

namespace RealtimeCSharp.Client.Services
{
    public class SupabaseService
    {
        private readonly ILocalStorageService _localStorage;

        public SupabaseService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async void Initialise(string supabaseUrl, string supabaseKey)
        {/*
            await Supabase.Client.InitializeAsync(
                 supabaseUrl,
                 supabaseKey,
                 new Supabase.SupabaseOptions
                 {
                     AutoConnectRealtime = true,
                     //AutoRefreshToken = true,
                     //PersistSession = true,
                     ShouldInitializeRealtime = true, //System.Security.Cryptography.Csp
                     //SessionPersistor = async (session) => { await _localStorage.SetItemAsync("SUPABASE_SESSION", session); return true; },
                     //SessionRetriever = async () => { return await _localStorage.GetItemAsync<Supabase.Gotrue.Session>("SUPABASE_SESSION"); },
                     //SessionDestroyer = async () => { await _localStorage.RemoveItemAsync("SUPABASE_SESSION"); return true; }
                 }
             );
            var instance = Supabase.Client.Instance;
            var channel = instance.Realtime.Channel("realtime", "departments");
            // Per Event Callbacks
            channel.OnInsert += (sender, args) => Console.WriteLine("New item inserted: " + args.Response.Payload.Record);
            channel.OnUpdate += (sender, args) => Console.WriteLine("Item updated: " + args.Response.Payload.Record);
            channel.OnDelete += (sender, args) => Console.WriteLine("Item deleted");

            // Callback for any event, INSERT, UPDATE, or DELETE
            channel.OnMessage += (sender, args) => Console.WriteLine(args.Response.Event);

            await channel.Subscribe();*/

            // Connect to db and web socket server
            var postgrestClient = Postgrest.Client.Initialize("http://localhost:3000");
            var realtimeClient = Supabase.Realtime.Client.Initialize("ws://localhost:4000/socket");

            //Socket events
            realtimeClient.OnOpen += (s, args) => Console.WriteLine("OPEN");
            realtimeClient.OnClose += (s, args) => Console.WriteLine("CLOSED");
            realtimeClient.OnError += (s, args) => Console.WriteLine("ERROR");

            await realtimeClient.ConnectAsync();

            // Subscribe to a channel and events
            var channelUsers = realtimeClient.Channel("realtime", "public", "users");
            channelUsers.OnInsert += (object s, SocketResponseEventArgs args) => Console.WriteLine("New item inserted: " + args.Response.Payload.Record);
            channelUsers.OnUpdate += (object s, SocketResponseEventArgs args) => Console.WriteLine("Item updated: " + args.Response.Payload.Record);
            channelUsers.OnDelete += (object s, SocketResponseEventArgs args) => Console.WriteLine("Item deleted");

            Console.WriteLine("Subscribing to users channel");
            await channelUsers.Subscribe();

            //Subscribing to another channel
            var channelTodos = realtimeClient.Channel("realtime", "public", "todos");
            channelTodos.OnClose += (object sender, ChannelStateChangedEventArgs args) => Console.WriteLine($"Channel todos { args.State}!!");
            Console.WriteLine("Subscribing to todos channel");
            await channelTodos.Subscribe();

            //Unsubscribing from channelTodos to trigger the OnClose event
            channelTodos.Unsubscribe();

            Console.WriteLine($"Users channel state after unsubscribing from todos channel: {channelUsers.State}");

            var response = await postgrestClient.Table<User>().Insert(new User { Name = "exampleUser" });
            var user = response.Models.FirstOrDefault();
            user.Name = "exampleUser2.0";
            await user.Update<User>();
            await user.Delete<User>();
        }
    }
}
