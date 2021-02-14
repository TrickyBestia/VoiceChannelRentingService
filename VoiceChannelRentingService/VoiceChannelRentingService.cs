// -----------------------------------------------------------------------
// <copyright file="VoiceChannelRentingService.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using TrickyBot;
using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.SingleServerInfoProviderService;
using VoiceChannelRentingService.API.Features;

namespace VoiceChannelRentingService
{
    public class VoiceChannelRentingService : ServiceBase<VoiceChannelRentingServiceConfig>
    {
        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Author = "TrickyBestia",
            Name = "VoiceChannelRentingService",
            Version = new Version(1, 0, 0, 0),
            GithubRepositoryUrl = "https://github.com/TrickyBestia/VoiceChannelRentingService",
        };

        protected override Task OnStart()
        {
            Bot.Instance.Client.UserVoiceStateUpdated += this.OnUserVoiceStateUpdated;
            return Task.CompletedTask;
        }

        protected override Task OnStop()
        {
            Bot.Instance.Client.UserVoiceStateUpdated -= this.OnUserVoiceStateUpdated;
            return Task.CompletedTask;
        }

        private async Task OnUserVoiceStateUpdated(SocketUser user, SocketVoiceState previousState, SocketVoiceState currentState)
        {
            if (previousState.VoiceChannel is not null)
            {
                var rentedChannel = this.Config.RentedChannels.Find(channel => channel.Id == previousState.VoiceChannel.Id);
                if (rentedChannel is not null && previousState.VoiceChannel.Users.Count == 0)
                {
                    this.Config.RentedChannels.Remove(rentedChannel);
                    await previousState.VoiceChannel.DeleteAsync();
                }
            }

            if (currentState.VoiceChannel is not null)
            {
                var rentingChannel = this.Config.RentingChannels.Find(channel => channel.Id == currentState.VoiceChannel.Id);
                if (rentingChannel is not null)
                {
                    var formattedChannelName = rentingChannel.RentedChannelName.Replace("{channelCreator}", user.ToString());
                    var rentedChannel = await Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService>().Guild.CreateVoiceChannelAsync(formattedChannelName, channel =>
                    {
                        channel.Bitrate = currentState.VoiceChannel.Bitrate;
                        channel.CategoryId = currentState.VoiceChannel.CategoryId;
                        channel.UserLimit = currentState.VoiceChannel.UserLimit;
                    });
                    this.Config.RentedChannels.Add(new RentedChannelInfo()
                    {
                        Id = rentedChannel.Id,
                        CreatorId = user.Id,
                        ParentChannelId = rentingChannel.Id,
                    });
                    await ((IGuildUser)user).ModifyAsync(properties => properties.Channel = rentedChannel);
                }
            }
        }
    }
}