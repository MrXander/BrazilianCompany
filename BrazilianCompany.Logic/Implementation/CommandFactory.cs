#region usings

using System;
using Autofac;
using BrazilianCompany.Logic.Command;
using BrazilianCompany.Logic.Interface;
using BrazilianCompany.Model.Model;
using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    internal static class CommandFactory
    {
        public static ICommand GetCommand(string command, IContainer container)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new InvalidOperationException("Invalid command");

            try
            {
                command = command.Trim();
                return DefineCommand(command, container);
            }
            catch (JsonSerializationException e)
            {
                throw new InvalidOperationException("Invalid command");
            }
        }

        private static ICommand DefineCommand(string command, IContainer container)
        {
            //TODO: Check and catch deserialization exception
            if (command.StartsWith(SupportedCommands.SETUP_PARK))
                return new SetupParkCommand(GetCommandParams(command, SupportedCommands.SETUP_PARK), container);

            if (command.StartsWith(SupportedCommands.PARK))
                return new ParkCommand(GetCommandParams(command, SupportedCommands.PARK), container);

            if (command.StartsWith(SupportedCommands.STATUS))
                return new StatusCommand(GetCommandParams(command, SupportedCommands.STATUS));

            if (command.StartsWith(SupportedCommands.EXIT))
                return new ExitCommand(GetCommandParams(command, SupportedCommands.EXIT));

            if (command.StartsWith(SupportedCommands.FIND_VEHICLE))
                return new FindVehicleCommand(GetCommandParams(command, SupportedCommands.FIND_VEHICLE));

            if (command.StartsWith(SupportedCommands.VEHICLES_BY_OWNER))
                return new FindVehicleByOwnerCommand(GetCommandParams(command, SupportedCommands.VEHICLES_BY_OWNER));

            throw new InvalidOperationException("Invalid command");
        }

        private static string GetCommandParams(string command, string prefix)
        {
            if (command.Length == prefix.Length)
                throw new InvalidOperationException("Invalid command");

            var cmdParams = command.Substring(prefix.Length, command.Length - prefix.Length);
            if (string.IsNullOrWhiteSpace(cmdParams))
                throw new InvalidOperationException("Invalid command");

            return cmdParams;
        }
    }
}