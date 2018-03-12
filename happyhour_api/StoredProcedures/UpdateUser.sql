CREATE PROCEDURE `UpdateUser` (IN opt_in_time INT(11), IN webhook_url VARCHAR(255), IN slack_user_id VARCHAR(255))
BEGIN
UPDATE hh_users SET hh_users.opt_in_time = opt_in_time, hh_users.webhook_url = webhook_url
WHERE hh_users.slack_user_id = slack_user_id;
END

