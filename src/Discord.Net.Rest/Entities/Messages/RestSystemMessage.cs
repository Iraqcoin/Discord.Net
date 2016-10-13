﻿using System.Diagnostics;
using Model = Discord.API.Message;

namespace Discord.Rest
{
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public class RestSystemMessage : RestMessage, ISystemMessage
    {
        public MessageType Type { get; private set; }

        internal RestSystemMessage(BaseDiscordClient discord, ulong id, IMessageChannel channel, RestUser author, IGuild guild)
            : base(discord, id, channel, author, guild)
        {
        }
        internal new static RestSystemMessage Create(BaseDiscordClient discord, IGuild guild, Model model)
        {
            var entity = new RestSystemMessage(discord, model.Id, 
                RestVirtualMessageChannel.Create(discord, model.ChannelId), 
                RestUser.Create(discord, model.Author.Value), guild);
            entity.Update(model);
            return entity;
        }
        internal override void Update(Model model)
        {
            base.Update(model);

            Type = model.Type;
        }

        private string DebuggerDisplay => $"{Author}: {Content} ({Id}, {Type})";
    }
}