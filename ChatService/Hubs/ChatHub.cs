using ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    // Defining the ChatHub, which allows sending and receiving messages between users
    // It also handles connection management and provides real-time updates
    public class ChatHub : Hub
    {
        // JoinChat method to notify all connected clients that a new user has joined
        // It uses Clients.All to send a message to all connected clients
        // "ReceieveMessage" is the method name on the client-side that will be called to receive the message
        public async Task JoinChat(UserConnection conn)
        {
            // Sending a message to all clients notifying that the user has joined
            await Clients.All
                .SendAsync("ReceieveMessage", "admin", $"{conn.UserName} has joined");
        }

        // JoinSpeceificChatRoom method allows a user to join a specific chat room
        // It adds the user to a group based on the chat room they want to join
        // The user will be added to the group using their connection ID and the specified chat room name
        public async Task JoinSpeceificChatRoom(UserConnection conn)
        {
            // Adding the user to the specified chat room (group)
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            // Sending a message to the specific chat room that the user has joined
            // The message will be sent to the group with the name of the chat room
            await Clients.Group(conn.ChatRoom).SendAsync("ReceieveMessage", "admin", $"{conn.UserName} has joined {conn.ChatRoom}");
        }
    }
}
