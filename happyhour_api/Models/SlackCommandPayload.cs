﻿using System;
using happyhour_api.Enums;
namespace happyhour_api.Models
{
    public class SlackCommandPayload
    {
        public string token { get; set; }
        public string team_id { get; set; }
        public string team_domain { get; set; }
        public string enterprise_id { get; set; }
        public string enterprise_name { get; set; }
        public string channel_id { get; set; }
        public string channel_name { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string command { get; set; }
        public string text { get; set; }
        public string response_url { get; set; }
        public string trigger_id { get; set; }
        public OptedInTime opt_in_time { get; set; }
    }
}
