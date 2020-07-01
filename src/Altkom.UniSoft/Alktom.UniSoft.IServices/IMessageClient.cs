using Altkom.UniSoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alktom.UniSoft.IServices
{
    public interface IMessageClient
    {
        Task YouHaveGotMessage(Message message);
        Task Pong();
    }

}
