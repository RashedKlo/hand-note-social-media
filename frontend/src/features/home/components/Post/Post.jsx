import { memo, useState } from "react";
import Comment from "./Comment/components/Comment";

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



// Post Component
const Post = memo(({ post, currentUser, onPostReaction, onComment, onCommentReaction, onReply, onEdit, onDelete, onShare }) => {
  const [showComments, setShowComments] = useState(false);
  const [commentText, setCommentText] = useState('');
  const [showReactions, setShowReactions] = useState(false);
  const [showMenu, setShowMenu] = useState(false);

  const totalReactions = Object.values(post.reactions || {}).reduce((sum, r) => sum + r.count, 0);
  const userReaction = post.userReaction;
  const reactionName = userReaction ? REACTION_NAMES[userReaction] : 'Like';
  const reactionColor = userReaction ? getReactionColor(userReaction) : 'text-gray-600';
  const isLiked = userReaction === '👍';
  const isOwnPost = post.user.id === currentUser.id;

  const handleCommentReaction = (commentId, reaction, parentCommentId = null) => {
    onCommentReaction(post.id, commentId, reaction, parentCommentId);
  };

  const handleReply = (commentId, content, parentCommentId = null) => {
    onReply(post.id, commentId, content, parentCommentId);
  };

  const handleSubmitComment = () => {
    if (commentText.trim()) {
      onComment(post.id, commentText);
      setCommentText('');
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-sm border border-gray-200 mb-4 overflow-hidden">
      {/* Post Header */}
      <div className="p-4">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3">
            <img
              src={post.user.avatar}
              alt={post.user.name}
              className="w-10 h-10 rounded-full object-cover border border-gray-200"
            />
            <div>
              <h3 className="font-semibold text-gray-900 text-sm hover:underline cursor-pointer">
                {post.user.name}
              </h3>
              <div className="flex items-center text-xs text-gray-500">
                <span>{post.timestamp}</span>
                <span className="mx-1">·</span>
                <svg className="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM4.332 8.027a6.012 6.012 0 011.912-2.706C6.512 5.73 6.974 6 7.5 6A1.5 1.5 0 019 7.5V8a2 2 0 004 0 2 2 0 011.523-1.943A5.977 5.977 0 0116 10c0 3.314-2.686 6-6 6s-6-2.686-6-6a5.98 5.98 0 01.332-1.973z" clipRule="evenodd" />
                </svg>
              </div>
            </div>
          </div>

          <div className="relative">
            <button
              onClick={() => setShowMenu(!showMenu)}
              className="p-2 hover:bg-gray-100 rounded-full transition-colors"
            >
              <svg className="w-5 h-5 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 5v.01M12 12v.01M12 19v.01" />
              </svg>
            </button>
            {showMenu && (
              <div className="absolute right-0 top-full mt-1 bg-white rounded-lg shadow-lg border border-gray-200 z-10 min-w-48">
                <div className="py-1">
                  {isOwnPost && (
                    <>
                      <button
                        onClick={() => { onEdit && onEdit(post); setShowMenu(false); }}
                        className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full transition-colors"
                      >
                        <svg className="w-4 h-4 mr-3" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
                        </svg>
                        Edit post
                      </button>
                      <button
                        onClick={() => { onDelete && onDelete(post.id); setShowMenu(false); }}
                        className="flex items-center px-4 py-2 text-sm text-red-600 hover:bg-gray-100 w-full transition-colors"
                      >
                        <svg className="w-4 h-4 mr-3" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                        </svg>
                        Move to trash
                      </button>
                    </>
                  )}
                  <button className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full transition-colors">
                    <svg className="w-4 h-4 mr-3" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.878 9.878L3 3m6.878 6.878L12 12m-3.172-3.172a4 4 0 00-.707.707m12.02 2.27a4 4 0 00.707-.708M9.19 14.81l-5.54 5.54a1 1 0 00-.22.462" />
                    </svg>
                    Hide post
                  </button>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* Post Content */}
        <div className="mt-3">
          <p className="text-gray-900 text-sm leading-relaxed whitespace-pre-line">{post.content}</p>
        </div>
      </div>

      {/* Post Image */}
      {post.image && (
        <div className="w-full">
          <img
            src={post.image}
            alt="Post content"
            className="w-full max-h-96 object-cover cursor-pointer hover:opacity-95 transition-opacity"
            loading="lazy"
          />
        </div>
      )}

      {/* Reaction and Comment Summary */}
      {(totalReactions > 0 || (post.comments && post.comments.length > 0)) && (
        <div className="px-4 py-2 border-b border-gray-100">
          <div className="flex items-center justify-between text-sm text-gray-500">
            {totalReactions > 0 && (
              <div className="flex items-center gap-2 cursor-pointer hover:underline">
                <div className="flex -space-x-1">
                  {Object.keys(post.reactions || {}).slice(0, 3).map(reaction => (
                    <div
                      key={reaction}
                      className="w-4 h-4 bg-white rounded-full flex items-center justify-center text-xs border-2 border-white shadow-sm"
                    >
                      {reaction}
                    </div>
                  ))}
                </div>
                <span>{totalReactions}</span>
              </div>
            )}
            <div className="flex items-center gap-4">
              {post.comments && post.comments.length > 0 && (
                <button
                  className="hover:underline"
                  onClick={() => setShowComments(!showComments)}
                >
                  {post.comments.length} comment{post.comments.length !== 1 ? 's' : ''}
                </button>
              )}
              <button className="hover:underline">2 shares</button>
            </div>
          </div>
        </div>
      )}

      {/* Action Buttons */}
      <div className="px-4 py-2 border-b border-gray-100">
        <div className="flex items-center">
          {/* Like Button */}
          <div className="flex-1 relative">
            <button
              className={`flex items-center justify-center gap-2 py-2 px-4 hover:bg-gray-100 rounded-lg transition-colors group w-full ${userReaction ? reactionColor : 'text-gray-600'
                }`}
              onMouseEnter={() => setShowReactions(true)}
              onMouseLeave={() => setShowReactions(false)}
              onClick={() => onPostReaction(post.id, '👍')}
            >
              {userReaction === '👍' ? (
                <svg className="w-5 h-5 fill-current" viewBox="0 0 24 24">
                  <path d="M14 9V5a3 3 0 0 0-3-3l-4 9v11h11.28a2 2 0 0 0 2-1.7l1.38-9a2 2 0 0 0-2-2.3zM7 22H4a2 2 0 0 1-2-2v-7a2 2 0 0 1 2-2h3v11z" />
                </svg>
              ) : userReaction === '❤️' ? (
                <svg className="w-5 h-5 fill-current" viewBox="0 0 24 24">
                  <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                </svg>
              ) : userReaction ? (
                <span className="text-lg">{userReaction}</span>
              ) : (
                <svg className="w-5 h-5 group-hover:text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14 10h4.764a2 2 0 011.789 2.894l-3.5 7A2 2 0 0115.263 21h-4.017c-.163 0-.326-.02-.485-.06L7 20m7-10V5a2 2 0 00-2-2h-.095c-.5 0-.905.405-.905.905 0 .714-.211 1.412-.608 2.006L9 9m5 1v10M4 11v8a2 2 0 002 2h2m0-10v10m0 0v-2a2 2 0 012-2h2.5" />
                </svg>
              )}
              <span className={`text-sm font-medium ${!userReaction ? 'group-hover:text-blue-600' : ''}`}>
                {reactionName}
              </span>
            </button>

            {showReactions && (
              <div className="absolute bottom-full left-1/2 transform -translate-x-1/2 mb-2 bg-white rounded-full shadow-lg border border-gray-200 flex items-center px-2 py-1 gap-1 z-10 animate-in fade-in duration-200">
                {REACTIONS.map(reaction => (
                  <button
                    key={reaction}
                    className="hover:scale-125 transition-transform duration-150 p-1 rounded-full hover:bg-gray-100"
                    onClick={() => onPostReaction(post.id, reaction)}
                  >
                    <span className="text-xl">{reaction}</span>
                  </button>
                ))}
              </div>
            )}
          </div>

          {/* Comment Button */}
          <button
            className="flex-1 flex items-center justify-center gap-2 py-2 px-4 hover:bg-gray-100 rounded-lg transition-colors group"
            onClick={() => setShowComments(!showComments)}
          >
            <svg className="w-5 h-5 text-gray-600 group-hover:text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z" />
            </svg>
            <span className="text-sm font-medium text-gray-600 group-hover:text-blue-600">Comment</span>
          </button>

          {/* Share Button */}
          <button
            className="flex-1 flex items-center justify-center gap-2 py-2 px-4 hover:bg-gray-100 rounded-lg transition-colors group"
            onClick={() => onShare && onShare(post)}
          >
            <svg className="w-5 h-5 text-gray-600 group-hover:text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8.684 13.342C8.886 12.938 9 12.482 9 12c0-.482-.114-.938-.316-1.342m0 2.684a3 3 0 110-2.684m0 2.684l6.632 3.316m-6.632-6l6.632-3.316m0 0a3 3 0 105.367-2.684 3 3 0 00-5.367 2.684zm0 9.316a3 3 0 105.367 2.684 3 3 0 00-5.367-2.684z" />
            </svg>
            <span className="text-sm font-medium text-gray-600 group-hover:text-green-600">Share</span>
          </button>
        </div>
      </div>

      {/* Comments Section */}
      {showComments && (
        <div className="p-4 bg-gray-50">
          {/* Comment Input */}
          <div className="flex gap-2 mb-4">
            <img
              src={currentUser.avatar}
              alt={currentUser.name}
              className="w-8 h-8 rounded-full object-cover border border-gray-200 flex-shrink-0"
            />
            <div className="flex-1 relative">
              <input
                type="text"
                value={commentText}
                onChange={(e) => setCommentText(e.target.value)}
                placeholder="Write a comment..."
                className="w-full bg-white rounded-full px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500 border border-gray-200 placeholder-gray-500"
                onKeyDown={(e) => {
                  if (e.key === 'Enter' && commentText.trim()) {
                    handleSubmitComment();
                  }
                }}
              />
              <button className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600">
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14.828 14.828a4 4 0 01-5.656 0M9 10h1.5a2.5 2.5 0 011.75.75M15 10h-1.5a2.5 2.5 0 00-1.75.75m.75 4l-.75.75M10.5 13.5l-.75.75m0 0a2.5 2.5 0 011.75-1.25M13.5 13.5a2.5 2.5 0 01-1.75-1.25" />
                </svg>
              </button>
            </div>
          </div>

          {/* Comments List */}
          <div className="space-y-3">
            {post.comments?.map(comment => (
              <Comment
                key={comment.id}
                comment={comment}
                onReaction={handleCommentReaction}
                onReply={handleReply}
                currentUser={currentUser}
              />
            ))}
          </div>
        </div>
      )}
    </div>
  );
});

export default Post;