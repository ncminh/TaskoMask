﻿using System.ComponentModel.DataAnnotations;
using TaskoMask.Application.Share.Resources;
using TaskoMask.Domain.Share.Resources;

namespace TaskoMask.Application.Workspace.Projects.Commands.Models
{
    public class UpdateProjectCommand : ProjectBaseCommand
    {
        public UpdateProjectCommand(string id, string name, string description, string organizationId)
                   : base(name, description, organizationId)
        {
            Id = id;
        }

        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        public string Id { get; }


    }
}