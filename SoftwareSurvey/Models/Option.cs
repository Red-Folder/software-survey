using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SoftwareSurvey.Models
{
    public class Option
    {
        private readonly PropertyInfo _propertyInfo;

        private readonly object _parent;
        private readonly object _model;

        private string _id;
        private string _label;
        private string _description;
        private Expression<Func<object>> _for;

        public Option(object parent, object model, PropertyInfo propertyInfo)
        {
            _parent = parent;
            _model = model;
            _propertyInfo = propertyInfo;
        }

        public string Id
        {
            get
            {
                if (_id == null)
                {
                    var source = _propertyInfo.Name;
                    StringBuilder builder = new StringBuilder();
                    for (var i = 0; i < source.Length; i++)
                    {
                        if (char.IsLower(source[i])) // if current char is already lowercase
                        {
                            builder.Append(source[i]);
                        }
                        else if (i == 0) // if current char is the first char
                        {
                            builder.Append(char.ToLower(source[i]));
                        }
                        else if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
                        {
                            builder.Append("-");
                            builder.Append(char.ToLower(source[i]));
                        }
                        else if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
                        {
                            builder.Append(char.ToLower(source[i]));
                        }
                        else // if current char is upper and next char is lower
                        {
                            builder.Append("-");
                            builder.Append(char.ToLower(source[i]));
                        }
                    }
                    _id = builder.ToString();
                }

                return _id;
            }
        }

        public string Label
        {
            get
            {
                if (_label == null)
                {
                    var memberInfo = _propertyInfo as MemberInfo;

                    var displayNameAttribute = (System.ComponentModel.DisplayNameAttribute)memberInfo.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), false).SingleOrDefault();
                    _label = displayNameAttribute?.DisplayName ?? "NOT SET";
                }

                return _label;
            }
        }

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    var memberInfo = _propertyInfo as MemberInfo;

                    var descriptionAttribute = (System.ComponentModel.DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).SingleOrDefault();
                    _description = descriptionAttribute?.Description ?? "NOT SET";
                }

                return _description;
            }
        }

        public Expression<Func<object>> For
        {
            get
            {
                if (_for == null)
                {
                    var parent = Expression.Constant(_parent);
                    var model = Expression.PropertyOrField(parent, "Model");
                    var property = Expression.PropertyOrField(model, _propertyInfo.Name);
                    var body = Expression.Convert(property, typeof(object));
                    _for = Expression.Lambda<Func<object>>(body);
                }
                return _for;
            }
        }

        public Action<int?> ValueSetter
        {
            get
            {
                return (value) =>
                    _propertyInfo.SetValue(_model, value);
            }
        }

        public Func<int?> ValueGetter
        {
            get
            {
                return () => _propertyInfo.GetValue(_model) as Nullable<int>;
            }
        }
    }
}
