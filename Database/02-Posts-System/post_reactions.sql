CREATE TABLE post_reactions (
    reaction_id INT IDENTITY(1,1) PRIMARY KEY,
    post_id INT NOT NULL,
    user_id INT NOT NULL,
    reaction_type NVARCHAR(20) NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_post_reactions_post FOREIGN KEY (post_id) REFERENCES posts(post_id) ON DELETE CASCADE,
    CONSTRAINT FK_post_reactions_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT UQ_post_reactions UNIQUE (post_id, user_id),
    CONSTRAINT CK_post_reaction_type CHECK (reaction_type IN ('like','love','care','haha','wow','sad','angry'))
);
