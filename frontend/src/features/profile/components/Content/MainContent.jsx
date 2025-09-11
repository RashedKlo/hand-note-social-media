import AboutTab from "../../../../features/profile/components/Tabs/AboutTab";
import PhotosTab from "../../../../features/profile/components/Tabs/PhotosTab";
import FriendsTab from "../../../../features/profile/components/Tabs/FriendsTab";
import VideosTab from "../../../../features/profile/components/Tabs/VideosTab";
import ReviewsTab from "../../../../features/profile/components/Tabs/ReviewsTab";
import { memo, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    ChatBubbleLeftIcon,
    EllipsisHorizontalIcon,
    PencilIcon,
    TrashIcon,
    ArrowUturnLeftIcon as ReplyIcon,
    GlobeAltIcon,

} from "@heroicons/react/24/outline";

const REACTIONS = ['👍', '❤️', '😂', '😮', '😢', '😡'];

const REACTION_NAMES = {
    '👍': 'Like',
    '❤️': 'Love',
    '😂': 'Haha',
    '😮': 'Wow',
    '😢': 'Sad',
    '😡': 'Angry'
};

const getReactionColor = (reaction) => {
    switch (reaction) {
        case '👍': return 'text-blue-600';
        case '❤️': return 'text-red-500';
        case '😂': return 'text-yellow-500';
        case '😮': return 'text-orange-500';
        case '😢': return 'text-blue-500';
        case '😡': return 'text-red-600';
        default: return 'text-gray-600';
    }
};
// Comment Component
const Comment = memo(({ comment, onReaction, onReply, currentUser, level = 0 }) => {
    const [showReplyBox, setShowReplyBox] = useState(false);
    const [replyText, setReplyText] = useState('');
    const [showReactions, setShowReactions] = useState(false);

    const totalReactions = Object.values(comment.reactions || {}).reduce((sum, r) => sum + r.count, 0);
    const userReaction = comment.userReaction;

    return (
        <div className={`${level > 0 ? 'ml-6 sm:ml-8' : ''}`}>
            <div className="flex space-x-2 sm:space-x-3">
                <img src={comment.user.avatar} alt={comment.user.name} className="w-6 h-6 sm:w-8 sm:h-8 rounded-full object-cover flex-shrink-0" />
                <div className="flex-1">
                    <div className="bg-gray-100 rounded-2xl px-3 py-2 inline-block">
                        <p className="font-semibold text-xs sm:text-sm text-gray-900">{comment.user.name}</p>
                        <p className="text-xs sm:text-sm text-gray-800">{comment.content}</p>
                    </div>

                    {totalReactions > 0 && (
                        <div className="flex items-center mt-1 ml-3">
                            <div className="flex items-center bg-white rounded-full px-2 py-0.5 shadow-sm border">
                                <div className="flex -space-x-1">
                                    {Object.keys(comment.reactions || {}).slice(0, 3).map(reaction => (
                                        <span key={reaction} className="text-xs">{reaction}</span>
                                    ))}
                                </div>
                                <span className="ml-1 text-xs text-gray-500">{totalReactions}</span>
                            </div>
                        </div>
                    )}

                    <div className="flex items-center space-x-4 mt-1 ml-3">
                        <span className="text-xs text-gray-500">{comment.timestamp}</span>
                        <div className="relative">
                            <button
                                className="text-xs font-semibold text-gray-600 hover:underline"
                                onMouseEnter={() => setShowReactions(true)}
                                onMouseLeave={() => setShowReactions(false)}
                                onClick={() => onReaction(comment.id, '👍')}
                            >
                                {userReaction ? REACTION_NAMES[userReaction] : 'Like'}
                            </button>

                            {showReactions && (
                                <div className="absolute bottom-full left-0 mb-2 bg-white rounded-full shadow-lg border flex items-center px-2 py-1 space-x-1 z-10">
                                    {REACTIONS.map(reaction => (
                                        <button
                                            key={reaction}
                                            className="hover:scale-125 transition-transform text-lg p-1"
                                            onClick={() => onReaction(comment.id, reaction)}
                                        >
                                            {reaction}
                                        </button>
                                    ))}
                                </div>
                            )}
                        </div>
                        <button
                            className="text-xs font-semibold text-gray-600 hover:underline"
                            onClick={() => setShowReplyBox(!showReplyBox)}
                        >
                            Reply
                        </button>
                    </div>

                    {showReplyBox && (
                        <div className="flex space-x-2 mt-2 ml-3">
                            <img src={currentUser.avatar} alt={currentUser.name} className="w-6 h-6 rounded-full object-cover" />
                            <div className="flex-1">
                                <input
                                    type="text"
                                    value={replyText}
                                    onChange={(e) => setReplyText(e.target.value)}
                                    placeholder={`Reply to ${comment.user.name}...`}
                                    className="w-full bg-gray-100 rounded-full px-3 py-1 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                                    onKeyDown={(e) => {
                                        if (e.key === 'Enter' && replyText.trim()) {
                                            onReply(comment.id, replyText);
                                            setReplyText('');
                                            setShowReplyBox(false);
                                        }
                                    }}
                                />
                            </div>
                        </div>
                    )}

                    {comment.replies?.map(reply => (
                        <div key={reply.id} className="mt-2">
                            <Comment
                                comment={reply}
                                onReaction={onReaction}
                                onReply={onReply}
                                currentUser={currentUser}
                                level={level + 1}
                            />
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
});

// Post Component
const Post = memo(({ post, onPostReaction, onCommentReaction, onComment, onReply, onShare, onEdit, onDelete, currentUser }) => {
    const [showComments, setShowComments] = useState(false);
    const [commentText, setCommentText] = useState('');
    const [showReactions, setShowReactions] = useState(false);
    const [showMenu, setShowMenu] = useState(false);

    const totalReactions = Object.values(post.reactions || {}).reduce((sum, r) => sum + r.count, 0);
    const userReaction = post.userReaction;
    const reactionName = userReaction ? REACTION_NAMES[userReaction] : 'Like';
    const reactionColor = userReaction ? getReactionColor(userReaction) : 'text-gray-600 group-hover:text-blue-600';
    const isOwnPost = post.user.id === currentUser.id;

    return (
        <div className="bg-white rounded-lg shadow-sm border border-gray-200 overflow-hidden">
            {/* Post Header */}
            <div className="p-4">
                <div className="flex items-center justify-between">
                    <div className="flex items-center space-x-3">
                        <img src={post.user.avatar} alt={post.user.name} className="w-10 h-10 rounded-full object-cover" />
                        <div>
                            <h3 className="font-semibold text-gray-900 text-sm">{post.user.name}</h3>
                            <div className="flex items-center text-xs text-gray-500">
                                <span>{post.timestamp}</span>
                                <span className="mx-1">·</span>
                                <GlobeAltIcon className="w-3 h-3" />
                            </div>
                        </div>
                    </div>
                    <div className="relative">
                        <button onClick={() => setShowMenu(!showMenu)} className="p-2 hover:bg-gray-100 rounded-full">
                            <EllipsisHorizontalIcon className="w-5 h-5 text-gray-500" />
                        </button>
                        {showMenu && (
                            <div className="absolute right-0 top-full mt-2 bg-white rounded-lg shadow-lg border border-gray-200 z-10 min-w-48">
                                <div className="py-1">
                                    {isOwnPost && (
                                        <>
                                            <button onClick={() => { onEdit(post); setShowMenu(false); }} className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full">
                                                <PencilIcon className="w-4 h-4 mr-3" />
                                                Edit post
                                            </button>
                                            <button onClick={() => { onDelete(post.id); setShowMenu(false); }} className="flex items-center px-4 py-2 text-sm text-red-600 hover:bg-gray-100 w-full">
                                                <TrashIcon className="w-4 h-4 mr-3" />
                                                Delete post
                                            </button>
                                        </>
                                    )}
                                    <button className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full">
                                        Hide post
                                    </button>
                                    <button className="flex items-center px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full">
                                        Report post
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
                    <img src={post.image} alt="Post content" className="w-full max-h-96 object-cover" loading="lazy" />
                </div>
            )}

            {/* Reaction Summary */}
            {(totalReactions > 0 || (post.comments?.length || 0) > 0) && (
                <div className="px-4 py-2 border-b border-gray-100">
                    <div className="flex items-center justify-between text-sm text-gray-500">
                        {totalReactions > 0 && (
                            <div className="flex items-center space-x-2">
                                <div className="flex -space-x-1">
                                    {Object.keys(post.reactions || {}).slice(0, 3).map(reaction => (
                                        <div key={reaction} className="w-5 h-5 bg-white rounded-full flex items-center justify-center text-xs border border-white shadow-sm">
                                            {reaction}
                                        </div>
                                    ))}
                                </div>
                                <span>{totalReactions}</span>
                            </div>
                        )}
                        <div className="flex items-center space-x-4">
                            {(post.comments?.length || 0) > 0 && (
                                <button className="hover:underline">{post.comments.length} comments</button>
                            )}
                            <button className="hover:underline">2 shares</button>
                        </div>
                    </div>
                </div>
            )}

            {/* Action Buttons */}
            <div className="px-4 py-2 border-b border-gray-100">
                <div className="flex items-center justify-around">
                    <div className="relative flex-1">
                        <button
                            className="flex items-center justify-center space-x-2 w-full py-2 hover:bg-gray-100 rounded-lg transition-colors group"
                            onMouseEnter={() => setShowReactions(true)}
                            onMouseLeave={() => setShowReactions(false)}
                            onClick={() => onPostReaction(post.id, '👍')}
                        >
                            {userReaction && userReaction !== '👍' ? (
                                <span className={`text-lg ${reactionColor}`}>{userReaction}</span>
                            ) : (
                                <svg className={`w-5 h-5 ${reactionColor}`} fill={userReaction === '👍' ? 'currentColor' : 'none'} stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14 10h4.764a2 2 0 011.789 2.894l-3.5 7A2 2 0 0115.263 21h-4.017c-.163 0-.326-.02-.485-.06L7 20m7-10V5a2 2 0 00-2-2h-.095c-.5 0-.905.405-.905.905 0 .714-.211 1.412-.608 2.006L9 9m5 1v10M4 11v8a2 2 0 002 2h2m0-10v10m0 0v-2a2 2 0 012-2h2.5" />
                                </svg>
                            )}
                            <span className={`text-sm font-medium ${reactionColor}`}>{reactionName}</span>
                        </button>

                        {showReactions && (
                            <div className="absolute bottom-full left-1/2 transform -translate-x-1/2 mb-2 bg-white rounded-full shadow-lg border flex items-center px-2 py-1 space-x-1 z-10">
                                {REACTIONS.map(reaction => (
                                    <button
                                        key={reaction}
                                        className="hover:scale-125 transition-transform text-lg p-1"
                                        onClick={() => onPostReaction(post.id, reaction)}
                                    >
                                        {reaction}
                                    </button>
                                ))}
                            </div>
                        )}
                    </div>

                    <button
                        className="flex items-center justify-center space-x-2 flex-1 py-2 hover:bg-gray-100 rounded-lg transition-colors group"
                        onClick={() => setShowComments(!showComments)}
                    >
                        <ChatBubbleLeftIcon className="w-5 h-5 text-gray-600 group-hover:text-blue-600" />
                        <span className="text-sm font-medium text-gray-600 group-hover:text-blue-600">Comment</span>
                    </button>

                    <button
                        className="flex items-center justify-center space-x-2 flex-1 py-2 hover:bg-gray-100 rounded-lg transition-colors group"
                        onClick={() => onShare(post)}
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
                <div className="p-4">
                    <div className="space-y-3">
                        {post.comments?.map(comment => (
                            <Comment
                                key={comment.id}
                                comment={comment}
                                onReaction={(commentId, reaction) => onCommentReaction(post.id, commentId, reaction)}
                                onReply={(commentId, content) => onReply(post.id, commentId, content)}
                                currentUser={currentUser}
                            />
                        ))}
                    </div>

                    {/* Comment Input */}
                    <div className="flex space-x-2 mt-4">
                        <img src={currentUser.avatar} alt={currentUser.name} className="w-8 h-8 rounded-full object-cover flex-shrink-0" />
                        <div className="flex-1 relative">
                            <input
                                type="text"
                                value={commentText}
                                onChange={(e) => setCommentText(e.target.value)}
                                placeholder="Write a comment..."
                                className="w-full bg-gray-100 rounded-full px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                                onKeyDown={(e) => {
                                    if (e.key === 'Enter' && commentText.trim()) {
                                        onComment(post.id, commentText);
                                        setCommentText('');
                                    }
                                }}
                            />
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
});
const MainContent = ({ activeTab, profileUser, mockFriends, mockPhotos }) => {
    const navigate = useNavigate();
    const currentUser = useMemo(() => ({
        id: 1,
        name: 'You',
        avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&h=150&fit=crop&crop=face'
    }), []);

    const [posts, setPosts] = useState([
        {
            id: 1,
            user: profileUser,
            content: 'Just wrapped up an amazing week at the tech conference! Met so many inspiring people and learned about the latest innovations in AI and machine learning. The future is bright! 🚀\n\nSpecial thanks to everyone who attended my talk on "Building Scalable Systems". Your questions and feedback were incredible!',
            image: 'https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=500&auto=format&fit=crop&q=60',
            timestamp: '3 hours ago',
            reactions: { '👍': { count: 45 }, '❤️': { count: 23 }, '😮': { count: 12 } },
            userReaction: null,
            comments: [
                {
                    id: 1,
                    user: mockFriends[0],
                    content: 'Amazing presentation! Your insights on system architecture were so valuable. Thank you for sharing! 🙌',
                    timestamp: '2 hours ago',
                    reactions: { '👍': { count: 5 }, '❤️': { count: 2 } },
                    userReaction: null,
                    replies: [
                        {
                            id: 11,
                            user: profileUser,
                            content: 'Thank you so much Alice! Really appreciate your kind words 😊',
                            timestamp: '1 hour ago',
                            reactions: { '❤️': { count: 3 } },
                            userReaction: null,
                            replies: []
                        }
                    ]
                },
                {
                    id: 2,
                    user: mockFriends[1],
                    content: 'Wish I could have been there! Any chance you\'ll be sharing the slides?',
                    timestamp: '1 hour ago',
                    reactions: { '👍': { count: 2 } },
                    userReaction: null,
                    replies: []
                }
            ]
        },
        {
            id: 2,
            user: profileUser,
            content: 'Weekend hiking adventure in Yosemite! Nothing beats disconnecting from the digital world and reconnecting with nature. The view from Half Dome was absolutely breathtaking! 🏔️ #NatureTherapy #YosemiteNationalPark',
            image: 'https://images.unsplash.com/photo-1551632811-561732d1e306?w=500&auto=format&fit=crop&q=60',
            timestamp: '2 days ago',
            reactions: { '❤️': { count: 67 }, '😮': { count: 34 }, '👍': { count: 23 } },
            userReaction: null,
            comments: [
                {
                    id: 3,
                    user: mockFriends[2],
                    content: 'Incredible shot! The lighting is perfect. Did you hike the cables route?',
                    timestamp: '1 day ago',
                    reactions: { '👍': { count: 4 } },
                    userReaction: null,
                    replies: []
                }
            ]
        },
        {
            id: 3,
            user: profileUser,
            content: 'Excited to share that our team just launched the new user dashboard! 🎉 Months of hard work, countless iterations, and amazing collaboration have led to this moment. Thank you to everyone who provided feedback during the beta phase.\n\nBuilding products that genuinely improve people\'s daily workflows is what drives me every day. On to the next challenge! 💪',
            timestamp: '1 week ago',
            reactions: { '👍': { count: 89 }, '❤️': { count: 45 }, '🤗': { count: 12 } },
            userReaction: null,
            comments: [
                {
                    id: 4,
                    user: mockFriends[3],
                    content: 'Congratulations! The new interface is so much more intuitive. Great work! 🎊',
                    timestamp: '6 days ago',
                    reactions: { '❤️': { count: 6 } },
                    userReaction: null,
                    replies: []
                },
                {
                    id: 5,
                    user: mockFriends[4],
                    content: 'Love the clean design and improved performance. You and your team nailed it!',
                    timestamp: '5 days ago',
                    reactions: { '👍': { count: 8 } },
                    userReaction: null,
                    replies: []
                }
            ]
        }
    ]);

    const handlePostReaction = (postId, reaction) => {
        setPosts(posts.map(post => {
            if (post.id === postId) {
                const newReactions = { ...post.reactions };
                const currentReaction = post.userReaction;

                if (currentReaction) {
                    newReactions[currentReaction].count--;
                    if (newReactions[currentReaction].count === 0) {
                        delete newReactions[currentReaction];
                    }
                }

                if (currentReaction !== reaction) {
                    newReactions[reaction] = newReactions[reaction] || { count: 0 };
                    newReactions[reaction].count++;
                    return { ...post, reactions: newReactions, userReaction: reaction };
                } else {
                    return { ...post, reactions: newReactions, userReaction: null };
                }
            }
            return post;
        }));
    };
    return <div className="lg:col-span-2">
        {activeTab === 'posts' && (
            <div className="space-y-4">
                {/* Create Post */}
                <div className="bg-white rounded-lg shadow-sm p-4">
                    <div className="flex items-center space-x-3">
                        <img
                            src={currentUser.avatar}
                            alt={currentUser.name}
                            className="w-10 h-10 rounded-full object-cover"
                        />
                        <button className="flex-1 bg-gray-100 hover:bg-gray-200 rounded-full py-3 px-4 text-left text-gray-500 transition-colors">
                            What's on your mind, {currentUser.name}?
                        </button>
                    </div>
                    <hr className="my-3" />
                    <div className="flex items-center justify-around">
                        <button className="flex items-center space-x-2 px-4 py-2 hover:bg-gray-100 rounded-lg transition-colors">
                            <div className="w-6 h-6 bg-red-500 rounded-full flex items-center justify-center">
                                <svg className="w-4 h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                    <path fillRule="evenodd" d="M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z" clipRule="evenodd" />
                                </svg>
                            </div>
                            <span className="text-gray-600 font-medium">Photo/Video</span>
                        </button>
                        <button className="flex items-center space-x-2 px-4 py-2 hover:bg-gray-100 rounded-lg transition-colors">
                            <div className="w-6 h-6 bg-blue-500 rounded-full flex items-center justify-center">
                                <svg className="w-4 h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                    <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clipRule="evenodd" />
                                </svg>
                            </div>
                            <span className="text-gray-600 font-medium">Life Event</span>
                        </button>
                    </div>
                </div>

                {/* Posts */}
                {posts.map(post => (
                    <Post
                        key={post.id}
                        post={post}
                        onPostReaction={handlePostReaction}
                        onCommentReaction={() => { }}
                        onComment={() => { }}
                        onReply={() => { }}
                        onShare={() => { }}
                        onEdit={() => { }}
                        onDelete={() => { }}
                        currentUser={currentUser}
                    />
                ))}
            </div>
        )}

        {activeTab === 'about' && (
            <AboutTab />
        )}

        {activeTab === 'friends' && (
            <FriendsTab mockFriends={mockFriends} />
        )}

        {activeTab === 'photos' && (
            <PhotosTab mockPhotos={mockPhotos} />
        )}

        {activeTab === 'videos' && (
            <VideosTab profileUser={profileUser} />
        )}

        {activeTab === 'reviews' && (
            <ReviewsTab />
        )}
    </div>
}
export default MainContent;