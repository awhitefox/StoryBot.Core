﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoryBot.Core.Model
{
    public class StoryAchievement
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
