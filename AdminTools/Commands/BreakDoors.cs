﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace AdminTools.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class BreakDoors : ICommand, IUsageProvider
    {
        public string Command { get; } = "breakdoors";

        public string[] Aliases { get; } = new string[] { "bd" };

        public string Description { get; } = "Manage breaking door/gate properties for players";

        public string[] Usage { get; } = new string[] { "%player%",  };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("at.bd"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            IEnumerable<Player> players = Player.GetProcessedData(arguments);

            foreach (Player player in players)
                if (Main.BreakDoors.Contains(player))
                    Main.BreakDoors.Remove(player);
                else
                    Main.BreakDoors.Add(player);

            response = $"{players.Count()} players have been updated. (Players with BD were removed, those without it were added)";
            return true;
        }
    }
}
