using System;
using System.Collections.Generic;
using Rules.Authoring;

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

        public ActionSchedule Schedule { get; set; }

        public IActionDetail ActionDetail { get; set; }
    }

    public interface IActionDetail
    {
        ActionType Type { get; }

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
        public DateTime StartTime { get; set; }

        public int Frequency { get; set; }
    }

}