CREATE TABLE conversation_members (
    conversation_id INT NOT NULL,
    user_id INT NOT NULL,
    joined_at DATETIME NOT NULL DEFAULT GETDATE(),
    last_seen DATETIME NULL,
    role VARCHAR(10) NOT NULL DEFAULT 'member' CHECK (role IN ('admin', 'moderator', 'member')),
    is_active BIT NOT NULL DEFAULT 1,
    left_at DATETIME NULL,
    CONSTRAINT pk_cm PRIMARY KEY (conversation_id, user_id),
    CONSTRAINT fk_cm_conversation FOREIGN KEY (conversation_id) REFERENCES conversations(id),
    CONSTRAINT fk_cm_user FOREIGN KEY (user_id) REFERENCES users(user_id),
    CONSTRAINT chk_cm_active_left_consistency CHECK ((is_active = 1 AND left_at IS NULL) OR (is_active = 0 AND left_at IS NOT NULL)),
    CONSTRAINT chk_cm_timestamps_order CHECK (left_at IS NULL OR joined_at <= left_at),
    CONSTRAINT chk_cm_last_seen_after_joined CHECK (last_seen IS NULL OR joined_at <= last_seen)
);
