﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace StoryBot.Core.Model
{
    public class SaveDocument
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        [BsonElement("id")]
        public long Id { get; set; }

        [BsonElement("current")]
        public SaveProgress Current { get; set; }

        [BsonElement("stats")]
        public List<SaveStoryStats> StoriesStats { get; set; }

        public SaveDocument(long Id, SaveProgress Current = null, List<SaveStoryStats> StoriesStats = null)
        {
            this.Id = Id;
            this.Current = Current ?? new SaveProgress();
            this.StoriesStats = StoriesStats ?? new List<SaveStoryStats>();
        }

        public SaveStoryStats GetStoryStats(int id)
        {
            var index = StoriesStats.FindIndex(x => x.StoryId == id);
            if (index != -1)
            {
                return StoriesStats[index];
            }
            else
            {
                var stats = new SaveStoryStats(id);
                StoriesStats.Add(stats);
                return stats;
            }
        }

        public void AddEnding(int storyId, int episodeId, int endingId)
        {
            Current = new SaveProgress { Story = Current.Story };

            int statsId = StoriesStats.FindIndex(x => x.StoryId == storyId);
            if (statsId == -1)
            {
                StoriesStats.Add(new SaveStoryStats(storyId, new List<SaveEpisodeStats>()));

                if (!StoriesStats[0].Episodes[episodeId].ObtainedEndings.Contains(endingId))
                    StoriesStats[0].Episodes[episodeId].ObtainedEndings.Add(endingId);
            }
            else
            {
                try
                {
                    if (!StoriesStats[0].Episodes[episodeId].ObtainedEndings.Contains(endingId))
                        StoriesStats[0].Episodes[episodeId].ObtainedEndings.Add(endingId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    StoriesStats[statsId].Episodes.Add(new SaveEpisodeStats());

                    if (!StoriesStats[statsId].Episodes[episodeId].ObtainedEndings.Contains(endingId))
                        StoriesStats[statsId].Episodes[episodeId].ObtainedEndings.Add(endingId);
                }
            }
        }

        public void AddAchievement(int storyId, int episodeId, int achievementId)
        {
            int statsId = StoriesStats.FindIndex(x => x.StoryId == storyId);
            if (statsId == -1)
            {
                StoriesStats.Add(new SaveStoryStats(storyId, new List<SaveEpisodeStats>())); 

                if (!StoriesStats[0].Episodes[episodeId].ObtainedAchievements.Contains(achievementId))
                    StoriesStats[0].Episodes[episodeId].ObtainedAchievements.Add(achievementId);
            }
            else
            {
                try
                {
                    if (!StoriesStats[statsId].Episodes[episodeId].ObtainedAchievements.Contains(achievementId))
                        StoriesStats[statsId].Episodes[episodeId].ObtainedAchievements.Add(achievementId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    StoriesStats[statsId].Episodes.Add(new SaveEpisodeStats());

                    if (!StoriesStats[statsId].Episodes[episodeId].ObtainedAchievements.Contains(achievementId))
                        StoriesStats[statsId].Episodes[episodeId].ObtainedAchievements.Add(achievementId);
                }
            }
        }

        public void AddUnlockable(string name)
        {
            Current.Unlockables.Add(name);
        }
    }
}
