﻿using System;
using System.Linq;
using Automatica.Core.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Automatica.Core.Internals.Cache.Logic
{
    internal class LogicTemplateCache : AbstractCache<RuleTemplate>, ILogicTemplateCache
    {
        public LogicTemplateCache(IConfiguration configuration) : base(configuration)
        {
        }

        protected override IQueryable<RuleTemplate> GetAll(AutomaticaContext context)
        {
            Clear();
            return context.RuleTemplates.AsNoTracking()
                .Include(a => a.RuleInterfaceTemplate).ThenInclude(b => b.This2RuleInterfaceDirectionNavigation);
        }

        protected override Guid GetKey(RuleTemplate obj)
        {
            return obj.ObjId;
        }

        public void AddOrUpdate(RuleTemplate template)
        {
            if (Contains(template.ObjId))
            {
                Update(template.ObjId, template);
            }
            else
            {
                Add(template.ObjId, template);
            }
        }
    }
}
