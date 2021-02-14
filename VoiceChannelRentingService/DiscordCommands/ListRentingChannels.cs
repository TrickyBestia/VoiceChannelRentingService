// -----------------------------------------------------------------------
// <copyright file="ListRentingChannels.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;
using TrickyBot;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;
using TrickyBot.Services.SingleServerInfoProviderService;

namespace VoiceChannelRentingService.DiscordCommands
{
    internal class ListRentingChannels : ConditionDiscordCommand
    {
        public ListRentingChannels()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("voicechannelrenting.list"));
        }

        public override string Name { get; } = "voicechannelrenting list";

        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<VoiceChannelRentingService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService>().Guild;
            var embedBuilder = new EmbedBuilder().WithCurrentTimestamp().WithTitle("Voice channel renting channels:");
            foreach (var rentingChannel in service.Config.RentingChannels)
            {
                var channel = guild.GetVoiceChannel(rentingChannel.Id);
                embedBuilder.AddField(channel.Name, $"Rented channels name: `{rentingChannel.RentedChannelName}`");
            }

            await message.Channel.SendMessageAsync(embed: embedBuilder.Build());
        }
    }
}