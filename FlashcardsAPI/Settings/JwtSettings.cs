﻿namespace FlashcardsAPI.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; } 
        public int TokenLifetimeInMinutes { get; set; } = 30;
    }
}
