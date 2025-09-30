CREATE TABLE user_sessions (
    session_id int IDENTITY(1,1) PRIMARY KEY,
    user_id int NOT NULL,
    refresh_token NVARCHAR(255) NOT NULL,
    device_id NVARCHAR(10) NOT NULL,
    ip_address NVARCHAR(45) NOT NULL,
    is_active BIT DEFAULT 1 NOT NULL,
    session_type NVARCHAR(20) DEFAULT 'regular' NOT NULL,
    expires_at DATETIME2(0) NOT NULL,
    created_at DATETIME2(0) DEFAULT GETDATE() NOT NULL,
    updated_at DATETIME2(0) DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_user_sessions_user_id FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT UQ_user_sessions_refresh_token UNIQUE (refresh_token),
    CONSTRAINT CHK_expires_at_future CHECK (expires_at > created_at),
    CONSTRAINT CHK_device_id_valid CHECK (device_id IN ('web', 'ios', 'android', 'desktop')),
    CONSTRAINT CHK_session_type_valid CHECK (session_type IN ('regular', 'remember_me')),
    CONSTRAINT CHK_refresh_token_length CHECK (LEN(TRIM(refresh_token)) >= 32),
    CONSTRAINT CHK_session_expiry_limits CHECK (
        (session_type = 'regular' AND expires_at <= DATEADD(HOUR, 24, created_at)) OR
        (session_type = 'remember_me' AND expires_at <= DATEADD(DAY, 30, created_at))
    ),
    CONSTRAINT CHK_active_session_token CHECK (is_active = 0)
);
