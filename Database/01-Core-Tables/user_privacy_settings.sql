CREATE TABLE user_privacy_settings (
    user_id INT PRIMARY KEY,
    default_post_audience VARCHAR(20) NOT NULL DEFAULT 'public',
    profile_visibility VARCHAR(20) NOT NULL DEFAULT 'public',
    friend_list_visibility VARCHAR(20) NOT NULL DEFAULT 'public',
    friend_request_from VARCHAR(20) NOT NULL DEFAULT 'public',
    message_from VARCHAR(20) NOT NULL DEFAULT 'public',
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    updated_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT fk_user_privacy_user_id FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT chk_default_post_audience CHECK (default_post_audience IN ('public', 'friends', 'only_me')),
    CONSTRAINT chk_profile_visibility CHECK (profile_visibility IN ('public', 'friends', 'only_me')),
    CONSTRAINT chk_friend_list_visibility CHECK (friend_list_visibility IN ('public', 'friends', 'only_me')),
    CONSTRAINT chk_friend_request_from CHECK (friend_request_from IN ('public', 'friends_of_friends')),
    CONSTRAINT chk_message_from CHECK (message_from IN ('public', 'friends_of_friends', 'friends')),
    CONSTRAINT chk_message_privacy_logic CHECK (
        NOT (friend_request_from = 'friends_of_friends' AND message_from = 'public')
    ),
    CONSTRAINT chk_profile_consistency CHECK (
        NOT (profile_visibility = 'only_me' AND friend_list_visibility IN ('public', 'friends')) AND
        NOT (profile_visibility = 'friends' AND friend_list_visibility = 'public')
    )
);
