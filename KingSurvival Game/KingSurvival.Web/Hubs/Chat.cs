namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.SignalR;
    using Contracts;

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

        //visitor instead of SendMessageToRoom mehod

        public void Accept(IVisitor visitor, string message, string[] rooms)
        {
            visitor.SendMessageToRoom(this, message, rooms);
        }

        //public void SendMessageToRoom(string message, string[] rooms)
        //{
        //    var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

        //    for (int i = 0; i < rooms.Length; i++)
        //    {
        //        Clients.Group(rooms[i]).addMessage(msg);
        //    }
        //}
    }
}