using Stateless;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.Models
{
    // dotnet add package Stateless

    public class LampProxy : Lamp
    {
        private int temp;

        public int Temp
        {
            get { return temp; }
            set {
                temp = value;                
                machine.Fire(LampTrigger.ValueChanged);
            
            }
        }


        private StateMachine<LampStatus, LampTrigger> machine;
        public override LampStatus Status => machine.State;

        public LampProxy()
        {
            machine = new StateMachine<LampStatus, LampTrigger>(LampStatus.Off);

            machine.Configure(LampStatus.Off)
                .OnEntry(() => Console.WriteLine("Dziekujemy za wylaczenie osw."), "Wyslanie sms")
                .Permit(LampTrigger.Push, LampStatus.On);

            machine.Configure(LampStatus.On)
                .Permit(LampTrigger.Push, LampStatus.Blinking)
                .PermitIf(LampTrigger.ValueChanged, LampStatus.Red, () => this.Temp > 50, ">50")
                .IgnoreIf(LampTrigger.ValueChanged, ()=>this.Temp <= 50, "below 50 temp")
                ;
                
            ;



            //machine.Configure(LampStatus.Blinking)
            //    .Permit(LampTrigger.Push, LampStatus.On);

        }
        public void Push() => machine.Fire(LampTrigger.Push);

        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());
    }

    public class Lamp
    {
        public virtual LampStatus Status { get; set; }
    }

    public enum LampStatus
    {
        On,
        Blinking,
        Off,
        Red
    }

    public enum LampTrigger
    {
        Push,
        ValueChanged,
        ElapsedTime
    }
}
