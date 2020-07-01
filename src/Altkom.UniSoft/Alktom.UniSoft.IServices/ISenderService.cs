using Altkom.UniSoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alktom.UniSoft.IServices
{
    public interface ISenderService
    {
        void Send(Customer customer);
    }
}
