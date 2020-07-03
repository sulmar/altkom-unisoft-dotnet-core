using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.Models
{
    public class Lamp
    {
        public LampStatus Status { get; set; }

        public void Push()
        {
            if (Status == LampStatus.Off)
            {
                Status = LampStatus.On;
            }
            else
            {
                Status = LampStatus.Off;
            }
        }
    }

    public enum LampStatus
    {
        On,
        Off
    }
}
