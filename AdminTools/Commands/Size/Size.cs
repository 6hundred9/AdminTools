﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace AdminTools.Commands.Size
{
    using PlayerRoles;
    using System.Collections.Generic;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    class Size : ParentCommand
    {
        public Size() => LoadGeneratedCommands();

        public override string Command { get; } = "size";

        public override string[] Aliases { get; } = new string[] { };

        public override string Description { get; } = "Sets the size of all users or a user";

        public override void LoadGeneratedCommands() { }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("at.size"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage:\nsize (player id / name) or (all / *)) (x value) (y value) (z value)" +
                    "\nsize reset";
                return false;
            }
            if (arguments.At(0) is "reset")
            {
                foreach (Player ply in Player.List)
                {
                    EventHandlers.SetPlayerScale(ply, 1, 1, 1);
                }
                response = "all the size has been reset";
                return true;
            }

            IEnumerable<Player> players = Player.GetProcessedData(arguments);

            if (arguments.Count != 4)
            {
                response = "Usage: size (all / *) (x) (y) (z)";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float xval))
            {
                response = $"Invalid value for x size: {arguments.At(1)}";
                return false;
            }

            if (!float.TryParse(arguments.At(2), out float yval))
            {
                response = $"Invalid value for y size: {arguments.At(2)}";
                return false;
            }

            if (!float.TryParse(arguments.At(3), out float zval))
            {
                response = $"Invalid value for z size: {arguments.At(3)}";
                return false;
            }

            foreach (Player ply in players)
            {
                EventHandlers.SetPlayerScale(ply, xval, yval, zval);
            }

            response = $"Everyone's scale has been set to {xval} {yval} {zval}";
            return true;
        }
    }
}
