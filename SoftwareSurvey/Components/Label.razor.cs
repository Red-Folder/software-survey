using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;

namespace SoftwareSurvey.Components
{
    public partial class Label<TValue>
    {
        [Parameter] 
        public Expression<Func<TValue>> For { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        private string label => GetDisplayName();

        private string GetDisplayName()
        {
            var expression = (MemberExpression)For.Body;
            var value = expression.Member.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            return value?.DisplayName ?? expression.Member.Name ?? "";
        }
    }
}
