CREATE TABLE post_media (
    post_media_id INT IDENTITY(1,1) PRIMARY KEY,
    post_id INT NOT NULL,
    media_id INT NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_post_media_post FOREIGN KEY (post_id) REFERENCES posts(post_id) ON DELETE CASCADE,
    CONSTRAINT FK_post_media_media FOREIGN KEY (media_id) REFERENCES media_files(media_id) ON DELETE CASCADE,
    CONSTRAINT UQ_post_media UNIQUE (post_id, media_id)
);
