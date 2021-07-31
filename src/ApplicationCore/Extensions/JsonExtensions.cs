﻿using System.Text.Json;

namespace ApplicationCore.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static T FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize<T>(obj, _jsonOptions);
        }
    }
}