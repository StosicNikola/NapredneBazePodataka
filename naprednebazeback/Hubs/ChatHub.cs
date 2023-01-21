using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using naprednebazeback.RedisDataLayer;
using StackExchange.Redis;
using naprednebazeback.DTOs;

namespace naprednebazeback.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        public string RecMsg{get ; set;}
        private readonly IDictionary<string,UserConnection> _connections;
        //private Publisher publisher;
        RedisDao rd = new RedisDao();

        public ChatHub(IDictionary<string,UserConnection> connections){
            _botUser = "MyChat Bot";
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if(_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection)){
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room).SendAsync("ReceiveMessage",_botUser,
                $"{userConnection.User} has left");

                SendConnectedUsers(userConnection.Room);
            }
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message){
            if(_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection)){
                await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage",userConnection.User, message);
            }
            rd.SaveMessagesToHash(new MessageView(-2,"Guest:"+userConnection.User,userConnection.Room,message,DateTime.Now));
        }
        public async Task JoinRoom(UserConnection userConnection){
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            _connections[Context.ConnectionId] = userConnection;
            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage",_botUser,
            $"{userConnection.User} has joined {userConnection.Room}");
            await SendConnectedUsers(userConnection.Room);
            string room  = userConnection.Room;
    
            List<MessageView> msgs = rd.GetMessageRoomForToday(room);
            foreach(MessageView msg in msgs){
                await PreviousMessage(msg);
            }

            
        }

        public Task SendConnectedUsers(string room){
            var users = _connections.Values
                .Where(c => c.Room == room)
                .Select(c => c.User);
            return Clients.Group(room).SendAsync("UsersInRoom",users);
        }
    
        public Task PreviousMessage(MessageView msg){
            if(msg == null) return Clients.Group(msg.Room).SendAsync("ReceiveMessage","Redis bot", "History is empty..." );
            return Clients.Group(msg.Room).SendAsync("ReceiveMessage",msg.Name, msg.Message);
        }

    }
}