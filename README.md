# save-chat-messages-to-firebase-realtime-database-net-maui-chat
This demo explains about how to save the chat conversations to FireBase Realtime database .NET MAUI Chat(SfChat).

## Sample
 
```xaml
 
RealtimeDatabaseService:
 
    internal class RealtimeDatabaseService
    {
        internal FirebaseClient DatabaseClient { get; set; }
 
        public RealtimeDatabaseService()
        {
            // Replace with your own Firebase Realtime Database URL
            var firebaseDatabaseUrl = "https://chatsource-91d0c-default-rtdb.firebaseio.com/";
 
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
 
ViewModel:
 
    public IDisposable LoadMessages()
    {
            return databaseClient.Child("ChatSource")
                .AsObservable<ChatMessage>().Subscribe((data) =>
                {
                    if (data.Object != null && data.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                    {
                        Author author;
                        if (data.Object.Author.Name == CurrentUser.Name)
                        {
                            author = CurrentUser;
                        }
                        else
                        {
                            author = new Author()
                            {
                                Name = data.Object.Author.Name,
                                Avatar = data.Object.Author.Avatar != null ? data.Object.Author.Avatar : "steven.png"
                            };
                        }
 
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            var message = new TextMessage()
                            {
                                Author = author,
                                Text = data.Object!.Content!,
                                DateTime = data.Object.Timestamp
                            };
 
                            this.Messages.Add(message);
                        });
                    }
                });
    }
 
```
 
## Requirements to run the demo
 
To run the demo, refer to [System Requirements for .NET MAUI]( https://help.syncfusion.com/maui/system-requirements)
 
## Troubleshooting:
### Path too long exception
 
If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.
 
## License
 
Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.
