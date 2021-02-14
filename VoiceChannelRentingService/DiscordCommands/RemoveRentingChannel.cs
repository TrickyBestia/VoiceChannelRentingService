// -----------------------------------------------------------------------
// <copyright file="RemoveRentingChannel.cs" company="TrickyBestia">
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
using VoiceChannelRentingService.API.Features;

namespace VoiceChannelRentingService.DiscordCommands
{
    internal class RemoveRentingChannel : ConditionDiscordCommand
    {
        public RemoveRentingChannel()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("voicechannelrenting.remove"));
        }

        public override string Name { get; } = "voicechannelrenting remove";

        public override DiscordCommandRunMode RunMode => throw new System.NotImplementedException();

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<VoiceChannelRentingService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService>().Guild;
            try
            {
                var currentChannel = ((IGuildUser)message.Author).VoiceChannel;
                var rentingChannel = guild.GetVoiceChannel(service.Config.RentedChannels.Find(channel => channel.Id == currentChannel.Id).ParentChannelId);
                VoiceChannelRenting.RemoveRentingChannel(rentingChannel);
                await message.Channel.SendMessageAsync($"{message.Author.Mention} channel removed!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} channel is not a renting channel!");
            }
        }
    }
}