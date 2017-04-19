#region usings

using System;
using Autofac;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Interface;
using BrazilianCompany.Model.Model;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class Engine : IEngine
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly Context _context;
        private readonly IUserInterface _userInterface;

        public Engine(IContainer container)
        {
            _context = new Context();
            _commandExecutor = new CommandExecutor(container);
            _userInterface = container.Resolve<IUserInterface>();
        }

        public void Start()
        {
            while (true)
            {
                var commandLine = _userInterface.ReadLine();

                if (string.IsNullOrWhiteSpace(commandLine)) continue;

                try
                {
                    var state = _commandExecutor.Execute(commandLine.Trim(), _context);
                    _userInterface.WriteLine(state.ToString());
                }
                catch (Exception ex)
                {
                    _userInterface.WriteLine(ex.Message);
                }
            }
        }
    }
}