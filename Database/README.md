# HandNote Database Schema

This repository contains the SQL Server database schema for **HandNote**, a social media platform inspired by Facebook. The database is organized into several modules to make it modular and maintainable.

## Folder Structure

```
Database/
├── 01-Core-Tables
│   ├── users.sql
│   ├── user_sessions.sql
│   ├── user_oauth_accounts.sql
│   └── user_privacy_settings.sql
├── 02-Posts-System
│   ├── posts.sql
│   ├── post_media.sql
│   ├── post_reactions.sql
│   ├── comments.sql
│   └── comment_reactions.sql
├── 03-Social-Features
│   ├── friendships.sql
│   └── notifications.sql
├── 04-Chat-System
│   ├── conversations.sql
│   ├── conversation_members.sql
│   └── messages.sql
├── 05-Media-Storage
│   └── media_files.sql
├── 06-Setup-Scripts
│   └── (optional: indexes, initial data)
├── 07-Indexes-Views
│   └── (optional: views, indexes)
```

## Core Tables (01-Core-Tables)

- **users**: Stores user personal info, authentication data, profile details.
- **user_sessions**: Tracks login sessions, devices, tokens.
- **user_oauth_accounts**: Stores OAuth info for Google/Facebook logins.
- **user_privacy_settings**: Configurable privacy options for each user.

## Posts System (02-Posts-System)

- **posts**: Stores all posts, including text, media, and shared posts.
- **post_media**: Stores mapping of media files attached to posts.
- **post_reactions**: Stores reactions on posts (like, love, haha, etc.).
- **comments**: Stores comments on posts, supports replies and media attachments.
- **comment_reactions**: Stores reactions on comments.

## Social Features (03-Social-Features)

- **friendships**: Stores friend requests and friendship statuses.
- **notifications**: Stores notifications for users, including system and user-generated notifications.

## Chat System (04-Chat-System)

- **conversations**: Stores chat conversations (direct or group).
- **conversation_members**: Stores members of each conversation and their roles.
- **messages**: Stores messages within conversations, supports replies, media, and message types.

## Media Storage (05-Media-Storage)

- **media_files**: Stores uploaded media with metadata like size, type, dimensions, and thumbnails.

## Constraints & Business Logic

- Most tables include foreign key relationships for data integrity.
- Enum-like fields are enforced with `CHECK` constraints.
- Timestamps (`created_at`, `updated_at`) ensure proper chronological order.
- Logic constraints prevent invalid states (e.g., self-friendship, invalid reactions).

## Setup Instructions

1. Create the database in SQL Server.
2. Run scripts in the following order to respect dependencies:
   1. `01-Core-Tables/*`
   2. `05-Media-Storage/*`
   3. `02-Posts-System/*`
   4. `03-Social-Features/*`
   5. `04-Chat-System/*`
3. (Optional) Run scripts in `06-Setup-Scripts` for indexes or initial data.

## Notes

- All timestamps use `DATETIME2` or `DATETIME`.
- All identity columns auto-increment starting from 1.
- Media size limit: 100MB per file.
- Posts support text length up to 1200 characters.
- Enum-like values are strictly validated using `CHECK` constraints.

---

This schema is designed for **modular development**, allowing easy expansion of social, chat, and media features.

