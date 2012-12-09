using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;

namespace Noesis.Javascript.Dynamic
{
    public class JContext : DynamicObject
    {
        public JavascriptContext Context { get; private set; }
        private IEnumerable<string> Members { get; set; }

        #region Constructors

        public JContext()
        {
            Initialise(new JavascriptContext());
        }

        public JContext(JavascriptContext javascript)
        {
            Initialise(javascript);
        }

        public JContext(string script) : this()
        {
            Execute(script);
        }

        public JContext(JavascriptContext javascript, string script) : this(javascript)
        {
            Execute(script);
        }

        private void Initialise(JavascriptContext context)
        {
            Context = context;
            Members = GetType().GetMembers().Select(x => x.Name);
        }

        #endregion

        #region Public Members

        public void Execute(string script)
        {
            Context.Run(script);
        }

        public dynamic Evaluate(string script)
        {
            var scriptResult = Context.Run(script);
            if (scriptResult is Dictionary<string, object>)
                return DictionaryConverter.AsDynamic((Dictionary<string, object>)scriptResult);
            return scriptResult;
        }
        #endregion

        #region Dymamic Methods

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = null;

            if (args.Length > 0)
            {
                result = Evaluate(args[0].ToString());
                args.Skip(1).Select(x => x.ToString()).ToList().ForEach(Execute);                
            }
            
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if(Members.Contains(binder.Name))
                return base.TryInvokeMember(binder, args, out result);
            result = Evaluate(ConstructInvokeStatement(binder, args));
            return true;
        }

        private string ConstructInvokeStatement(InvokeMemberBinder binder, object[] args)
        {
            return string.Format("{0}({1})", binder.Name, string.Join(",", args.Select(ObjectExtensions.AsArgument)));
        }

        #endregion

        #region Dynamic Properties

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(Members.Contains(binder.Name))
                return base.TryGetMember(binder, out result);
            result = Context.GetParameter(binder.Name);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Members.Contains(binder.Name))
                return base.TrySetMember(binder, value);
            Context.SetParameter(binder.Name, value);
            return true;
        }

        #endregion
    }
}
