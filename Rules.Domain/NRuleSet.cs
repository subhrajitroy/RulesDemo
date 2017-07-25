using System;
using System.Collections.Generic;
using Rules.Authoring;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rules.Domain
{
    public class NRuleSet
    {
        public NRuleSet()
        {
            Rules = new List<NRule>();
        }
        public string Name { get; set; }

        public Guid Id { get; set; }

        public List<NRule> Rules { get; set; }

        public String LastUpdatedBy { get; set; }

        public bool IsActive { get; set; }

        public string Version { get; set; }

        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,Formatting.Indented);
        }
    }

    public class NRule
    {
        public NRule()
        {
            ActionDetails = new List<TriggerAction>();
        }
        public string Name { get; set; }
        public Guid Id { get; set; }

        public AggregateCondition Condition { get; set; }

        public bool IsActive { get; set; }

        public List<TriggerAction> ActionDetails { get; }
    }

    public class TriggerAction
    {
        public String Name { get; set; }

        public Guid Id { get; set; }

        public Guid ExecutorId { get; set; }

        public ActionType ActionType { get; set; }

        public ActionSchedule Schedule { get; set; }

        public JObject ActionDetail { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public interface IActionDetail
    {
        List<IReceiver> Receivers { get; }
    }

    public abstract class NotificationActionDetail : IActionDetail
    {
        public ActionType Type => ActionType.Notification;
        public abstract List<IReceiver> Receivers { get; }

        public abstract NotificationType NotificationType { get; }

    }

    public class EmailActionDetail : NotificationActionDetail
    {
        public EmailActionDetail()
        {
            Receivers = new List<IReceiver>();
        }
        public override List<IReceiver> Receivers { get; }
        public override NotificationType NotificationType => NotificationType.EMAIL;
    }

    public interface IReceiver
    {
        string Address { get; }
    }

    public class UserEmail : IReceiver
    {
        public UserEmail(string email)
        {
            Address = email;
        }

        public string Address { get; }
    }

    public class Device : IReceiver
    {
        public Device(Guid deviceId)
        {
            Address = deviceId.ToString();
        }
        public string Address { get; }
    }

    public enum ActionType
    {
        Device,
        Notification
    }

    public enum NotificationType
    {
        EMAIL,
        SMS,
        InAPP,
        Push
    }

    public class ActionSchedule
    {

        public ActionSchedule()
        {
            StartTime = DateTime.Now;;
            Frequency = 1;
        }
        public DateTime StartTime { get; set; }

        public int Frequency { get; set; }
    }

}
