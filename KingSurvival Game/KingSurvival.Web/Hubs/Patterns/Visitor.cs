namespace KingSurvival.Web.Hubs.Contracts
{
    public class Visitor : IVisitor
    {
        public void SendMessageToRoom(Chat chat, string message, string[] rooms)
        {
            string msg = string.Format("{0}: {1}", chat.Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                chat.Clients.Group(rooms[i]).addMessage(msg);
            }
        }
    }
}