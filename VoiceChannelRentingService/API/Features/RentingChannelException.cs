// -----------------------------------------------------------------------
// <copyright file="RentingChannelException.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace VoiceChannelRentingService.API.Features
{
    public class RentingChannelException : Exception
    {
        public RentingChannelException(RentingChannelExceptionType type)
        {
            this.Type = type;
        }

        public RentingChannelExceptionType Type { get; }
    }
}