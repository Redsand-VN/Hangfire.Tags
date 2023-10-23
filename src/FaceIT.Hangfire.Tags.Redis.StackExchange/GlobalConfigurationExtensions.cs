﻿using Hangfire.Redis;
using Hangfire.Tags.Dashboard;

namespace Hangfire.Tags.Redis.StackExchange
{
    /// <summary>
    /// Provides extension methods to setup FaceIT.Hangfire.Tags
    /// </summary>
    public static class GlobalConfigurationExtensions
    {
        /// <summary>
        /// Configures Hangfire to use Tags.
        /// </summary>
        /// <param name="configuration">Global configuration</param>
        /// <param name="options">Options for tags</param>
        /// <param name="redisOptions">Options for Redis storage</param>
        /// <param name="jobStorage">The jobStorage for which this configuration is used.</param>
        /// <returns></returns>
        public static IGlobalConfiguration UseTagsWithRedis(this IGlobalConfiguration configuration, TagsOptions options = null, RedisStorageOptions redisOptions = null, JobStorage jobStorage = null)
        {
            options = options ?? new TagsOptions();
            redisOptions = redisOptions ?? new RedisStorageOptions();

            var storage = new RedisTagsServiceStorage(redisOptions);
            (jobStorage ?? JobStorage.Current).Register(options, storage);

            // configuration.UseStorage(new RedisTagsStorage((RedisStorage) (jobStorage ?? JobStorage.Current), storage, redisOptions));

            var config = configuration.UseTags(options).UseFilter(new RedisStateFilter(storage));
            return config;
        }
    }
}
