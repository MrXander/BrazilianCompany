#region usings

using Autofac;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class CommandExecutor
    {
        private readonly IContainer _container;

        public CommandExecutor(IContainer container)
        {
            _container = container;
        }

        public object Execute(string commandLine, Context context)
        {
            var command = CommandFactory.GetCommand(commandLine, _container);
            command.Execute(context);
            return command.GetState();
        }
    }
}