using StackExchange.Redis;

namespace RedisCachingDemo
{
    class MyCache
    {
        static readonly ConnectionMultiplexer redis =
            ConnectionMultiplexer.Connect("redis-16919.c256.us-east-1-2.ec2.cloud.redislabs.com:16919,password=44tjswnPQy2IC8UE62YUxP0ec3h3gR8B");

        public static IDatabase Get()
        {
            return redis.GetDatabase();
        }
    }
}