CREATE PROCEDURE `AddUser` (IN opt_in_time INT(11), IN channel_id VARCHAR(255), IN team_id VARCHAR(255), IN team_domain VARCHAR(255), IN slack_user_id VARCHAR(255), IN user_name VARCHAR(255), IN webhook_url VARCHAR(255))
BEGIN
INSERT INTO happyhour.hh_users (hh_users.opt_in_time, hh_users.channel_id, hh_users.team_id, hh_users.team_domain, hh_users.slack_user_id, hh_users.user_name, hh_users.webhook_url) VALUES
(opt_in_time, channel_id, team_id, team_domain, slack_user_id, user_name, webhook_url);
END
