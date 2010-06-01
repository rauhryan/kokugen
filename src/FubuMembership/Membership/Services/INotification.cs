using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuMembership.Membership.Services
{
    public interface INotification
    {
        string[] Messages { get; }
        bool IsValid();
    }

    public class Notification : INotification
    {
        private IList<string> _messages = new List<string>();
        
        public string[] Messages
        {
            get { return _messages.ToArray(); }
        }

        public bool IsValid()
        {
            return Messages.Length == 0;
        } 

        public INotification AddMessage(string message)
        {
            _messages.Add(message);
            return this;
        }
    }
}