using Firebase.Database;
using Firebase.Database.Query;

namespace ChatMaui
{
    internal class RealtimeDatabaseService
    {
        internal FirebaseClient DatabaseClient { get; set; }

        public RealtimeDatabaseService()
        {
            var firebaseDatabaseUrl = "https://chatdatabase-b9a22-default-rtdb.firebaseio.com/";

            // Initialize FirebaseClient.
            DatabaseClient = new FirebaseClient(firebaseDatabaseUrl);
        }

        // Method to store message in Firebase Database
        internal async Task SendMessageAsync(ChatMessage content)
        {
            await DatabaseClient
                .Child("ChatSource")
                .PostAsync(content);
        }
    }
}
