CREATE TABLE friendships (
    friendship_id INT IDENTITY(1,1) PRIMARY KEY,
    requester_id INT NOT NULL,
    addressee_id INT NOT NULL,
    status NVARCHAR(20) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2 NULL DEFAULT GETDATE(),
    CONSTRAINT FK_friendships_requester_id FOREIGN KEY (requester_id) REFERENCES users(user_id),
    CONSTRAINT FK_friendships_addressee_id FOREIGN KEY (addressee_id) REFERENCES users(user_id),
    CONSTRAINT UQ_friendship UNIQUE (requester_id, addressee_id),
    CONSTRAINT CK_friendships_no_self_friendship CHECK (requester_id != addressee_id),
    CONSTRAINT CK_friendships_status CHECK (status IN ('pending', 'accepted', 'declined', 'blocked', 'cancelled'))
);
