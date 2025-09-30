CREATE TABLE user_oauth_accounts (
    oauth_id int IDENTITY(1,1) NOT NULL,
    user_id int NOT NULL,
    provider NVARCHAR(50) NOT NULL,
    provider_user_id NVARCHAR(255) NOT NULL,
    provider_email NVARCHAR(320) NULL,
    provider_username NVARCHAR(100) NULL,
    access_token NVARCHAR(MAX) NULL,
    refresh_token NVARCHAR(MAX) NULL,
    token_expires_at DATETIME2 NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_user_oauth_accounts PRIMARY KEY (oauth_id),
    CONSTRAINT FK_user_oauth_accounts_users FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT CK_user_oauth_accounts_provider_valid CHECK (provider IN ('facebook', 'google')),
    CONSTRAINT CK_user_oauth_accounts_provider_user_id_not_empty CHECK (LEN(TRIM(provider_user_id)) > 0)
);
