CREATE TABLE posts (
    post_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    content NVARCHAR(MAX) NULL,
    privacy_setting NVARCHAR(20) DEFAULT 'public' NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE() NOT NULL,
    updated_at DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_posts_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT CK_posts_privacy CHECK (privacy_setting IN ('public','friends','only_me')),
    CONSTRAINT CK_posts_content_length CHECK (content IS NULL OR LEN(content) <= 5000)
);
