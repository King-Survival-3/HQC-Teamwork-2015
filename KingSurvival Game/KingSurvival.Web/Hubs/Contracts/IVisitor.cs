using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingSurvival.Web.Hubs.Contracts
{
    public interface IVisitor
    {
        void SendMessageToRoom(Chat chat, string message, string[] rooms);
    }
}
