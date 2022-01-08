﻿using TaskoMask.Domain.Core.Events;

namespace TaskoMask.Domain.Team.Entities.Members.Events
{
    public class MemberUpdatedEvent : DomainEvent
    {
        public MemberUpdatedEvent(string id, string displayName, string email, string userName) : base(entityId: id, entityType: nameof(Member))
        {
            Id = id;
            DisplayName = displayName;
            UserName = userName;
            Email = email;
        }


        public string Id { get; }
        public string DisplayName { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}