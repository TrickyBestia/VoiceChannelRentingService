// -----------------------------------------------------------------------
// <copyright file="VoiceChannelRenting.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;

using Discord;
using TrickyBot;

namespace VoiceChannelRentingService.API.Features
{
    public static class VoiceChannelRenting
    {
        public static void AddRentingChannel(IVoiceChannel channel, string rentedChannelName)
        {
            var service = Bot.Instance.ServiceManager.GetService<VoiceChannelRentingService>();

            if (channel is null)
            {
                throw new ArgumentException(null, nameof(channel), new NullReferenceException());
            }

            if (string.IsNullOrWhiteSpace(rentedChannelName))
            {
                throw new ArgumentException(null, nameof(rentedChannelName));
            }

            if (service.Config.RentingChannels.Any(rentingChannel => rentingChannel.Id == channel.Id))
            {
                throw new RentingChannelException(RentingChannelExceptionType.ChannelAlreadyExists);
            }

            service.Config.RentingChannels.Add(new RentingChannelInfo()
            {
                Id = channel.Id,
                RentedChannelName = rentedChannelName,
            });
        }

        public static void RemoveRentingChannel(IVoiceChannel channel)
        {
            var service = Bot.Instance.ServiceManager.GetService<VoiceChannelRentingService>();

            if (channel is null)
            {
                throw new ArgumentException(null, nameof(channel), new NullReferenceException());
            }

            var rentingChannel = service.Config.RentingChannels.Find(rentingChannel => rentingChannel.Id == channel.Id);
            if (rentingChannel is null)
            {
                throw new RentingChannelException(RentingChannelExceptionType.ChannelDoesNotExist);
            }

            service.Config.RentingChannels.Remove(rentingChannel);
        }
    }
}