namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.SignalR;
    using Contracts;

    /// <summary>
    /// Chat class for sending messages between players
    /// </summary>
    public class Chat : Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.All.addMessage(msg);
        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.joinRoom(room);
        }

        /// <summary>
        /// Visitor pattern applied when sending message to room
        /// </summary>
        /// <param name="visitor">The visitor instance that will handle the method for sending message to room</param>
        /// <param name="message">The message that will be send to room</param>
        /// <param name="rooms">Rooms that will receive the message</param>
        public void Accept(IVisitor visitor, string message, string[] rooms)
        {
            visitor.SendMessageToRoom(this, message, rooms);
        }
        
    }
}