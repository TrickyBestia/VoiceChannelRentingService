// -----------------------------------------------------------------------
// <copyright file="RentedChannelInfo.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace VoiceChannelRentingService.API.Features
{
    public class RentedChannelInfo
    {
        public ulong Id { get; set; }

        public ulong ParentChannelId { get; set; }

        public ulong CreatorId { get; set; }
    }
}