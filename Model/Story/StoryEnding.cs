﻿using MongoDB.Bson.Serialization.Attributes;

namespace StoryBot.Core.Model
{
    public class StoryEnding
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("content")]
        public string[] Content { get; set; }
    }
}
