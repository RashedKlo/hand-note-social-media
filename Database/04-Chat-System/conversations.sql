CREATE TABLE conversations (
    id INT PRIMARY KEY IDENTITY(1,1),
    type VARCHAR(10) NOT NULL CHECK (type IN ('direct', 'group')),
    name VARCHAR(255) NULL,
    description VARCHAR(1000) NULL,
    created_by INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME NOT NULL DEFAULT GETDATE(),
    is_active BIT NOT NULL DEFAULT 1,
    max_members INT NULL,
    CONSTRAINT fk_conv_created_by FOREIGN KEY (created_by) REFERENCES users(user_id),
    CONSTRAINT chk_conv_name_for_groups CHECK ((type = 'group' AND name IS NOT NULL AND LEN(LTRIM(RTRIM(name))) > 0) OR (type = 'direct' AND name IS NULL)),
    CONSTRAINT chk_conv_description_for_groups CHECK ((type = 'group' AND description IS NOT NULL) OR (type = 'direct' AND description IS NULL)),
    CONSTRAINT chk_conv_max_members CHECK ((type = 'group' AND max_members IS NOT NULL AND max_members > 0) OR (type = 'direct' AND max_members IS NULL)),
    CONSTRAINT chk_conv_name_length CHECK (name IS NULL OR LEN(name) BETWEEN 1 AND 255),
    CONSTRAINT chk_conv_description_length CHECK (description IS NULL OR LEN(description) <= 1000),
    CONSTRAINT chk_conv_timestamps CHECK (created_at <= updated_at)
);
