import { memo, useState } from "react";

const REACTIONS = ['👍', '❤️', '🤗', '😂', '😮', '😢', '😡'];
const REACTION_NAMES = {
  '👍': 'Like',
  '❤️': 'Love',
  '🤗': 'Care',
  '😂': 'Haha',
  '😮': 'Wow',
  '😢': 'Sad',
  '😡': 'Angry'
};

const getReactionColor = (reaction) => {
  switch (reaction) {
    case '👍': return 'text-blue-500';
    case '❤️': return 'text-red-500';
    case '🤗': return 'text-orange-500';
    case '😂': return 'text-yellow-500';
    case '😮': return 'text-purple-500';
    case '😢': return 'text-blue-500';
    case '😡': return 'text-red-500';
    default: return 'text-gray-600';
  }
};

// Comment Component (recursive for replies)
const Comment = memo(({ comment, onReaction, onReply, onComment, currentUser, depth = 0 }) => {
  const [showReplies, setShowReplies] = useState(true);
  const [replyText, setReplyText] = useState('');
  const [showReplyInput, setShowReplyInput] = useState(false);
  const [showReactions, setShowReactions] = useState(false);

  const totalReactions = Object.values(comment.reactions || {}).reduce((sum, r) => sum + r.count, 0);
  const userReaction = comment.userReaction;
  const reactionName = userReaction ? REACTION_NAMES[userReaction] : 'Like';
  const reactionColor = userReaction ? getReactionColor(userReaction) : 'text-gray-500 hover:text-blue-600';
  const isLiked = userReaction === '👍';

  const handleSubmitReply = () => {
    if (replyText.trim()) {
      onReply(comment.id, replyText);
      setReplyText('');
      setShowReplyInput(false);
    }
  };

  return (
    <div className={`flex gap-2 ${depth > 0 ? 'ml-6 md:ml-10' : ''}`}>
      {/* Avatar */}
      <div className="flex-shrink-0">
        <img
          src={comment.user.avatar}
          alt={comment.user.name}
          className="w-8 h-8 rounded-full object-cover border border-gray-200"
        />
      </div>

      {/* Comment Content */}
      <div className="flex-1 min-w-0">
        {/* Comment Bubble */}
        <div className="bg-gray-100 rounded-2xl px-3 py-2 inline-block max-w-full">
          <div className="font-semibold text-sm text-gray-900 mb-1">{comment.user.name}</div>
          <div className="text-sm text-gray-800 break-words whitespace-pre-wrap">{comment.content}</div>
        </div>

        {/* Reaction Summary for Comment */}
        {totalReactions > 0 && (
          <div className="flex items-center gap-1 mt-1 ml-3">
            <div className="flex -space-x-1">
              {Object.keys(comment.reactions || {}).slice(0, 3).map(reaction => (
                <div
                  key={reaction}
                  className="w-4 h-4 bg-white rounded-full flex items-center justify-center text-xs border-2 border-white shadow-sm"
                >
                  {reaction}
                </div>
              ))}
            </div>
            <span className="text-xs text-gray-500 font-medium">{totalReactions}</span>
          </div>
        )}

        {/* Comment Actions */}
        <div className="flex items-center gap-4 mt-1 ml-3 text-xs text-gray-500 font-semibold">
          {/* Like Button with Reactions */}
          <div className="relative">
            <button
              className={`hover:underline transition-colors ${reactionColor}`}
              onMouseEnter={() => setShowReactions(true)}
              onMouseLeave={() => setShowReactions(false)}
              onClick={() => onReaction(comment.id, '👍')}
            >
              {reactionName}
            </button>

            {/* Reaction Picker */}
            {showReactions && (
              <div className="absolute left-0 bottom-full mb-2 bg-white rounded-full shadow-lg border border-gray-200 flex items-center px-2 py-1 gap-1 z-20 animate-in fade-in duration-200">
                {REACTIONS.map(reaction => (
                  <button
                    key={reaction}
                    className="hover:scale-125 transition-transform duration-150 p-1 rounded-full hover:bg-gray-100"
                    onClick={() => onReaction(comment.id, reaction)}
                  >
                    <span className="text-lg">{reaction}</span>
                  </button>
                ))}
              </div>
            )}
          </div>

          <button
            className="hover:underline transition-colors hover:text-gray-700"
            onClick={() => setShowReplyInput(!showReplyInput)}
          >
            Reply
          </button>

          <span className="text-gray-400">{comment.timestamp}</span>
        </div>

        {/* Reply Input */}
        {showReplyInput && (
          <div className="flex gap-2 mt-3">
            <img
              src={currentUser.avatar}
              alt={currentUser.name}
              className="w-6 h-6 rounded-full object-cover border border-gray-200 flex-shrink-0"
            />
            <div className="flex-1">
              <input
                type="text"
                value={replyText}
                onChange={(e) => setReplyText(e.target.value)}
                placeholder="Write a reply..."
                className="w-full bg-gray-100 rounded-full px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:bg-white border-0 placeholder-gray-500"
                onKeyDown={(e) => {
                  if (e.key === 'Enter' && replyText.trim()) {
                    handleSubmitReply();
                  }
                }}
              />
            </div>
          </div>
        )}

        {/* Replies */}
        {comment.replies && comment.replies.length > 0 && showReplies && (
          <div className="mt-3 space-y-3">
            {comment.replies.map(reply => (
              <Comment
                key={reply.id}
                comment={reply}
                onReaction={(replyId, reaction) => onReaction(replyId, reaction, comment.id)}
                onReply={(replyId, content) => onReply(replyId, content, comment.id)}
                currentUser={currentUser}
                depth={depth + 1}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
});

export default Comment;