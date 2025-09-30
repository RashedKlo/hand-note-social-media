CREATE TABLE comment_reactions (
    reaction_id INT IDENTITY(1,1) PRIMARY KEY,
    comment_id INT NOT NULL,
    user_id INT NOT NULL,
    reaction_type NVARCHAR(20) NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_comment_reactions_comment FOREIGN KEY (comment_id) REFERENCES comments(comment_id) ON DELETE CASCADE,
    CONSTRAINT FK_comment_reactions_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT UQ_comment_reactions UNIQUE (comment_id, user_id),
    CONSTRAINT CK_comment_reaction_type CHECK (reaction_type IN ('like','love','care','haha','wow','sad','angry'))
);
