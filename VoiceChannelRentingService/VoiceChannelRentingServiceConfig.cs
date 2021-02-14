// -----------------------------------------------------------------------
// <copyright file="VoiceChannelRentingServiceConfig.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Interfaces;
using VoiceChannelRentingService.API.Features;

namespace VoiceChannelRentingService
{
    public class VoiceChannelRentingServiceConfig : IConfig
    {
        public bool IsEnabled { get; set; } = false;

        public List<RentingChannelInfo> RentingChannels { get; set; } = new List<RentingChannelInfo>();

        public List<RentedChannelInfo> RentedChannels { get; set; } = new List<RentedChannelInfo>();
    }
}