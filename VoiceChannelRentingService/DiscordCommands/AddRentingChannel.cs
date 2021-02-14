// -----------------------------------------------------------------------
// <copyright file="AddRentingChannel.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;
using VoiceChannelRentingService.API.Features;

namespace VoiceChannelRentingService.DiscordCommands
{
    internal class AddRentingChannel : ConditionDiscordCommand
    {
        public AddRentingChannel()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("voicechannelrenting.add"));
        }

        public override string Name { get; } = "voicechannelrenting add";

        public override DiscordCommandRunMode RunMode { get; }

        protected override async Task Execute(IMessage message, string parameter)
        {
            var match = Regex.Match(parameter, @"^(.+)$");
            try
            {
                VoiceChannelRenting.AddRentingChannel(((IGuildUser)message.Author).VoiceChannel, match.Result("$1"));
                await message.Channel.SendMessageAsync($"{message.Author.Mention} channel added!");
            }
            catch (RentingChannelException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} channel already is a renting channel!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid parameters!");
            }
        }
    }
}