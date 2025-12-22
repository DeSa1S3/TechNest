using Backend.Middleware_Components.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Backend.Middleware_Components.Cache
{

    //Unique Team - реализация в команде

    public class CacheSDK : ICacheService
    {
        private IDatabase _cacheDb;
        private ISubscriber _cacheSub;

        public CacheSDK()
        {
            var redis = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints = { "redis_cache:6379" },
                    Password = "root",
                    AbortOnConnectFail = false
                });

            _cacheDb = redis.GetDatabase();

            _cacheDb.Execute("CONFIG", "SET", "notify-keyspace-events", "Ex");

            _cacheSub = redis.GetSubscriber();
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);

            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);


            return default;
        }

        public ISubscriber RedisSubscriber()
        {
            return _cacheSub;
        }

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);

            if (_exist)
                return _cacheDb.KeyDelete(key);


            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.UtcNow);

            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
        }

        public DateTime GetKeyExpirationTime(string key)
        {
            var ttl = _cacheDb.KeyTimeToLive(key);

            // Ульяновская временная зона (UTC+4)
            var ulyanovskTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");

            if (ttl.HasValue)
            {
                var utcExpiration = DateTime.UtcNow.Add(ttl.Value);
                return TimeZoneInfo.ConvertTimeFromUtc(utcExpiration, ulyanovskTimeZone).AddHours(1);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ulyanovskTimeZone).AddHours(1);
        }

        public void WriteKeyInStorage(int id_user, string type, string key, DateTime extime)
        {
            SetData($"{type}_storage_{id_user}", key, extime);
        }

        public void WriteKeyInStorage<T>(int id_user, string type, T key, DateTime extime)
        {
            SetData($"{type}_storage_{id_user}", key, extime);
        }

        public void WriteKeyInStorageObject<T>(string storage_desc, T key, DateTime extime)
        {
            SetData($"{storage_desc}_storage", key, extime);
        }

        public void DeleteKeyFromStorage(string storage_desc)
        {
            RemoveData($"{storage_desc}_storage");
        }

        public void DeleteKeyFromStorage(int id_user, string type)
        {
            RemoveData($"{type}_storage_{id_user}");
        }

        public bool CheckExistKeysStorage(int id_user, string type)
        {
            var cache_data = GetData<string>($"{type}_storage_{id_user}");

            if (cache_data != null)
                return true;

            return false;
        }

        public bool CheckExistKeysStorage<T>(int id_user, string type)
        {
            var cache_data = GetData<T>($"{type}_storage_{id_user}");

            if (cache_data != null)
                return true;

            return false;
        }

        public bool CheckExistKeysStorage<T>(string storage_desc)
        {
            var cache_data = GetData<T>($"{storage_desc}_storage");

            if (cache_data != null)
                return true;

            return false;
        }

        public string? GetKeyFromStorage(int id_user, string type)
        {
            var cache_data = GetData<string>($"{type}_storage_{id_user}");

            return cache_data;
        }


        public T GetKeyFromStorage<T>(int id_user, string type)
        {
            var cache_data = GetData<T>($"{type}_storage_{id_user}");

            return cache_data;
        }

        public T GetKeyFromStorage<T>(string storage_desc)
        {
            var cache_data = GetData<T>($"{storage_desc}_storage");

            return cache_data;
        }
    }
}