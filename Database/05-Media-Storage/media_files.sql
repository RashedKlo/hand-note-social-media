CREATE TABLE media_files (
    media_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    file_name NVARCHAR(255) NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_media_files_user_id FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);
