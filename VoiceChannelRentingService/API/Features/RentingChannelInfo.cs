// -----------------------------------------------------------------------
// <copyright file="RentingChannelInfo.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace VoiceChannelRentingService.API.Features
{
    public class RentingChannelInfo
    {
        public ulong Id { get; set; }

        public string RentedChannelName { get; set; }
    }
}