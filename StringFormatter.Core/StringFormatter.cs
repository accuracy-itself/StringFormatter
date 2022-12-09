using StringFormatter.Core.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Core
{
    public class StringFormatter: IStringFormatter
    {
        public readonly static StringFormatter Shared = new StringFormatter();

        private ConcurrentDictionary<Type, ConcurrentDictionary<string, Func<object, string>>> _cache = new();

        public string Format(string template, object target)
        {
            var tokens = TokenMaker.GetTokens(template);

            if (!_cache.ContainsKey(target.GetType()))
                _cache.TryAdd(target.GetType(), new ConcurrentDictionary<string, Func<object, string>>());

            foreach (ReplaceToken tokenToReplace in tokens.Where(x => x is ReplaceToken))
            {
                if (!_cache[target.GetType()].ContainsKey(tokenToReplace.TokenText))
                {
                    Func<object, string> func = MakeExpression(target.GetType(), tokenToReplace.ClearFieldName);

                    _cache[target.GetType()].TryAdd(tokenToReplace.TokenText, func);
                }

                tokenToReplace.Replacement = _cache[target.GetType()][tokenToReplace.TokenText](target);
            }

            return string.Join("", tokens);
        }

        private static Func<object, string> MakeExpression(Type target, string asccesField)
        {
            ParameterExpression numParam = Expression.Parameter(typeof(object), "target");
            Expression cast = Expression.Convert(numParam, target);
            MemberExpression call = Expression.PropertyOrField(cast, asccesField);
            Expression toString = Expression.Call(call, "ToString", null);
            Expression<Func<object, string>> lambda =
                Expression.Lambda<Func<object, string>>(
                    toString,
                    new ParameterExpression[] { numParam });

            return lambda.Compile();
        }
    }
}
