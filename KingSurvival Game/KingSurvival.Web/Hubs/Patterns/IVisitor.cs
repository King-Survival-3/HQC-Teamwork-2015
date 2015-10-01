namespace KingSurvival.Web.Hubs.Contracts
{
    public interface IVisitor
    {
        void SendMessageToRoom(Chat chat, string message, string[] rooms);
    }
}