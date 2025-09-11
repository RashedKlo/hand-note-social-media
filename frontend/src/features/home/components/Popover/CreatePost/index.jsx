import React, { useState, useRef, memo } from 'react';
import {
  XMarkIcon as X,
  PhotoIcon as Image,
  UserGroupIcon as Users,
  MapPinIcon as MapPin,
  FaceSmileIcon as Smile,
  GlobeAltIcon as Globe,
  LockClosedIcon as Lock,
  UserIcon as Users2,
  VideoCameraIcon as Video,
  EllipsisHorizontalIcon as MoreHorizontal
} from '@heroicons/react/24/outline';

/* ========================================
   CONFIGURATION CONSTANTS
   ======================================== */

// Current user mock data
const CURRENT_USER = {
  id: 1,
  name: 'Alex Thompson',
  avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=40&h=40&fit=crop&crop=face'
};

// Mock friends data for tagging functionality
const MOCK_FRIENDS = [
  { id: 2, name: 'Sarah Johnson', avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b1c3?w=40&h=40&fit=crop&crop=face' },
  { id: 3, name: 'John Smith', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=32&h=32&fit=crop&crop=face' },
  { id: 4, name: 'Emma Wilson', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=32&h=32&fit=crop&crop=face' },
  { id: 5, name: 'Mike Chen', avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=32&h=32&fit=crop&crop=face' },
];

// Available emoji reactions
const REACTIONS = ['😀', '😂', '😍', '😢', '😮', '😡', '👍', '❤️', '🔥', '🎉', '👏', '🤔', '💯', '🙌', '😎', '🤩'];

// Privacy settings configuration
const PRIVACY_OPTIONS = [
  { id: 'public', label: 'Public', icon: Globe, description: 'Anyone on or off Facebook', color: 'text-blue-600' },
  { id: 'friends', label: 'Friends', icon: Users, description: 'Your friends on Facebook', color: 'text-green-600' },
  { id: 'friends_except', label: 'Friends except...', icon: Users2, description: 'Friends; except...', color: 'text-amber-600' },
  { id: 'only_me', label: 'Only me', icon: Lock, description: 'Only you', color: 'text-gray-600' },
];

// Feelings and activities options
const FEELINGS = [
  { emoji: '😊', text: 'happy', category: 'emotion' },
  { emoji: '😍', text: 'loved', category: 'emotion' },
  { emoji: '😎', text: 'cool', category: 'emotion' },
  { emoji: '😴', text: 'sleepy', category: 'state' },
  { emoji: '🤩', text: 'excited', category: 'emotion' },
  { emoji: '😌', text: 'blessed', category: 'emotion' },
  { emoji: '🥳', text: 'celebrating', category: 'activity' },
  { emoji: '💪', text: 'motivated', category: 'state' },
];

/* ========================================
   MAIN COMPONENT
   ======================================== */

const CreatePostPopover = memo(({ open, handleClose }) => {
  /* ========================================
     STATE MANAGEMENT
     ======================================== */

  // Content and media states
  const [postContent, setPostContent] = useState('');
  const [selectedImages, setSelectedImages] = useState([]);
  const [selectedVideos, setSelectedVideos] = useState([]);
  const [location, setLocation] = useState('');

  // UI visibility states
  const [showEmojiPicker, setShowEmojiPicker] = useState(false);
  const [showFriendTagger, setShowFriendTagger] = useState(false);
  const [showPrivacySelector, setShowPrivacySelector] = useState(false);
  const [showFeelingSelector, setShowFeelingSelector] = useState(false);
  const [showLocationInput, setShowLocationInput] = useState(false);

  // Selection states
  const [selectedPrivacy, setSelectedPrivacy] = useState(PRIVACY_OPTIONS[0]);
  const [taggedFriends, setTaggedFriends] = useState([]);
  const [selectedFeeling, setSelectedFeeling] = useState(null);

  // File input references
  const fileInputRef = useRef(null);
  const videoInputRef = useRef(null);

  // Early return if modal is not open
  if (!open) return null;

  /* ========================================
     EVENT HANDLERS
     ======================================== */

  /**
   * Reset all states and close modal
   */
  const closeModal = () => {
    // Reset content states
    setPostContent('');
    setSelectedImages([]);
    setSelectedVideos([]);
    setTaggedFriends([]);
    setSelectedFeeling(null);
    setLocation('');

    // Reset UI visibility states
    setShowEmojiPicker(false);
    setShowFriendTagger(false);
    setShowPrivacySelector(false);
    setShowFeelingSelector(false);
    setShowLocationInput(false);

    // Close modal
    handleClose();
  };

  /**
   * Handle image file selection and preview
   */
  const handleImageSelect = (e) => {
    const files = Array.from(e.target.files);
    files.forEach(file => {
      if (file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = (event) => {
          setSelectedImages(prev => [...prev, {
            id: Date.now() + Math.random(),
            url: event.target.result,
            file,
            type: 'image'
          }]);
        };
        reader.readAsDataURL(file);
      }
    });
  };

  /**
   * Handle video file selection and preview
   */
  const handleVideoSelect = (e) => {
    const files = Array.from(e.target.files);
    files.forEach(file => {
      if (file.type.startsWith('video/')) {
        const reader = new FileReader();
        reader.onload = (event) => {
          setSelectedVideos(prev => [...prev, {
            id: Date.now() + Math.random(),
            url: event.target.result,
            file,
            type: 'video'
          }]);
        };
        reader.readAsDataURL(file);
      }
    });
  };

  /**
   * Remove selected media item
   */
  const removeMedia = (mediaId, type) => {
    if (type === 'image') {
      setSelectedImages(prev => prev.filter(img => img.id !== mediaId));
    } else {
      setSelectedVideos(prev => prev.filter(vid => vid.id !== mediaId));
    }
  };

  /**
   * Add emoji to post content
   */
  const addEmoji = (emoji) => {
    setPostContent(prev => prev + emoji);
    setShowEmojiPicker(false);
  };

  /**
   * Tag a friend in the post
   */
  const tagFriend = (friend) => {
    if (!taggedFriends.find(f => f.id === friend.id)) {
      setTaggedFriends(prev => [...prev, friend]);
      setPostContent(prev => prev + `@${friend.name} `);
    }
    setShowFriendTagger(false);
  };

  /**
   * Remove friend tag
   */
  const removeTag = (friendId) => {
    const friend = taggedFriends.find(f => f.id === friendId);
    if (friend) {
      setPostContent(prev => prev.replace(`@${friend.name} `, ''));
      setTaggedFriends(prev => prev.filter(f => f.id !== friendId));
    }
  };

  /**
   * Select feeling/activity
   */
  const selectFeeling = (feeling) => {
    setSelectedFeeling(feeling);
    setShowFeelingSelector(false);
  };

  /**
   * Toggle UI panels
   */
  const togglePanel = (panelName) => {
    // Close all panels first
    setShowEmojiPicker(false);
    setShowFriendTagger(false);
    setShowPrivacySelector(false);
    setShowFeelingSelector(false);
    setShowLocationInput(false);

    // Open the requested panel
    switch (panelName) {
      case 'emoji': setShowEmojiPicker(true); break;
      case 'friends': setShowFriendTagger(true); break;
      case 'privacy': setShowPrivacySelector(true); break;
      case 'feelings': setShowFeelingSelector(true); break;
      case 'location': setShowLocationInput(true); break;
    }
  };

  /**
   * Handle post submission
   */
  const handlePost = () => {
    const postData = {
      content: postContent,
      images: selectedImages,
      videos: selectedVideos,
      taggedFriends,
      privacy: selectedPrivacy,
      feeling: selectedFeeling,
      location,
      timestamp: new Date().toISOString()
    };

    console.log('Creating post:', postData);
    alert('Post created successfully!');
    closeModal();
  };

  /* ========================================
     COMPUTED VALUES
     ======================================== */

  const PrivacyIcon = selectedPrivacy.icon;
  const hasContent = postContent.trim() || selectedImages.length > 0 || selectedVideos.length > 0;
  const firstName = CURRENT_USER.name.split(' ')[0];

  /* ========================================
     RENDER FUNCTIONS
     ======================================== */

  /**
   * Render privacy selector dropdown
   */
  const renderPrivacySelector = () => {
    if (!showPrivacySelector) return null;

    return (
      <div className="mb-4 bg-white border border-[var(--color-border-primary)] rounded-lg shadow-lg overflow-hidden">
        <div className="p-3 bg-[var(--color-bg-secondary)] border-b border-[var(--color-border-primary)]">
          <h3 className="font-semibold text-[var(--color-text-primary)]">Select audience</h3>
          <p className="text-sm text-[var(--color-text-secondary)]">Who can see your post?</p>
        </div>
        {PRIVACY_OPTIONS.map((option) => {
          const OptionIcon = option.icon;
          const isSelected = selectedPrivacy.id === option.id;

          return (
            <button
              key={option.id}
              onClick={() => {
                setSelectedPrivacy(option);
                setShowPrivacySelector(false);
              }}
              className="w-full p-3 hover:bg-[var(--color-bg-hover)] flex items-center space-x-3 text-left transition-colors"
            >
              <div className="w-10 h-10 bg-[var(--color-bg-secondary)] rounded-full flex items-center justify-center">
                <OptionIcon className={`w-5 h-5 ${option.color}`} />
              </div>
              <div className="flex-1">
                <p className="font-medium text-[var(--color-text-primary)]">{option.label}</p>
                <p className="text-sm text-[var(--color-text-secondary)]">{option.description}</p>
              </div>
              {isSelected && (
                <div className="w-5 h-5 bg-[var(--color-primary)] rounded-full flex items-center justify-center">
                  <svg className="w-3 h-3 text-white" fill="currentColor" viewBox="0 0 20 20">
                    <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                  </svg>
                </div>
              )}
            </button>
          );
        })}
      </div>
    );
  };

  /**
   * Render location input field
   */
  const renderLocationInput = () => {
    if (!showLocationInput) return null;

    return (
      <div className="mb-4">
        <div className="flex items-center space-x-2 p-3 border border-[var(--color-border-primary)] rounded-lg bg-[var(--color-surface-sunken)]">
          <MapPin className="w-5 h-5 text-red-500" />
          <input
            type="text"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            placeholder="Where are you?"
            className="flex-1 focus:outline-none bg-transparent text-[var(--color-text-primary)] placeholder-[var(--color-text-placeholder)]"
          />
          <button
            onClick={() => {
              setLocation('');
              setShowLocationInput(false);
            }}
            className="text-[var(--color-text-tertiary)] hover:text-[var(--color-text-secondary)] transition-colors"
          >
            <X className="w-4 h-4" />
          </button>
        </div>
      </div>
    );
  };

  /**
   * Render tagged friends display
   */
  const renderTaggedFriends = () => {
    if (taggedFriends.length === 0) return null;

    return (
      <div className="mb-4">
        <div className="flex flex-wrap gap-2">
          {taggedFriends.map(friend => (
            <div key={friend.id} className="flex items-center space-x-2 bg-[var(--color-bg-active)] text-[var(--color-primary)] px-3 py-1 rounded-full text-sm">
              <img src={friend.avatar} alt={friend.name} className="w-5 h-5 rounded-full" />
              <span>{friend.name}</span>
              <button
                onClick={() => removeTag(friend.id)}
                className="text-[var(--color-primary)] hover:text-[var(--color-primary-dark)] transition-colors"
              >
                <X className="w-3 h-3" />
              </button>
            </div>
          ))}
        </div>
      </div>
    );
  };

  /**
   * Render selected media preview
   */
  const renderMediaPreview = () => {
    const allMedia = [...selectedImages, ...selectedVideos];
    if (allMedia.length === 0) return null;

    return (
      <div className="mb-4">
        <div className="border border-[var(--color-border-primary)] rounded-lg p-3">
          <div className="grid grid-cols-2 gap-2">
            {selectedImages.map((image) => (
              <div key={image.id} className="relative group">
                <img
                  src={image.url}
                  alt="Selected"
                  className="w-full h-32 object-cover rounded-lg"
                />
                <button
                  onClick={() => removeMedia(image.id, 'image')}
                  className="absolute top-2 right-2 p-1 bg-black bg-opacity-75 rounded-full text-white opacity-0 group-hover:opacity-100 transition-opacity"
                >
                  <X className="w-4 h-4" />
                </button>
              </div>
            ))}
            {selectedVideos.map((video) => (
              <div key={video.id} className="relative group">
                <video
                  src={video.url}
                  className="w-full h-32 object-cover rounded-lg"
                  controls
                />
                <button
                  onClick={() => removeMedia(video.id, 'video')}
                  className="absolute top-2 right-2 p-1 bg-black bg-opacity-75 rounded-full text-white opacity-0 group-hover:opacity-100 transition-opacity"
                >
                  <X className="w-4 h-4" />
                </button>
              </div>
            ))}
          </div>
        </div>
      </div>
    );
  };

  /**
   * Render emoji picker
   */
  const renderEmojiPicker = () => {
    if (!showEmojiPicker) return null;

    return (
      <div className="mb-4 bg-[var(--color-bg-secondary)] rounded-lg p-4">
        <h4 className="text-sm font-medium text-[var(--color-text-primary)] mb-2">Choose an emoji</h4>
        <div className="grid grid-cols-8 gap-2">
          {REACTIONS.map(emoji => (
            <button
              key={emoji}
              onClick={() => addEmoji(emoji)}
              className="text-2xl p-2 hover:bg-[var(--color-bg-hover)] rounded-lg transition-colors"
            >
              {emoji}
            </button>
          ))}
        </div>
      </div>
    );
  };

  /**
   * Render friend tagger
   */
  const renderFriendTagger = () => {
    if (!showFriendTagger) return null;

    return (
      <div className="mb-4 bg-[var(--color-bg-secondary)] rounded-lg p-4 max-h-48 overflow-y-auto">
        <h4 className="text-sm font-medium text-[var(--color-text-primary)] mb-2">Tag friends</h4>
        {MOCK_FRIENDS.map(friend => {
          const isAlreadyTagged = taggedFriends.find(f => f.id === friend.id);

          return (
            <button
              key={friend.id}
              onClick={() => tagFriend(friend)}
              disabled={isAlreadyTagged}
              className="w-full flex items-center space-x-3 p-2 hover:bg-[var(--color-bg-hover)] rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <img src={friend.avatar} alt={friend.name} className="w-8 h-8 rounded-full" />
              <span className="text-sm font-medium text-[var(--color-text-primary)]">{friend.name}</span>
              {isAlreadyTagged && (
                <span className="text-xs text-[var(--color-primary)]">✓ Tagged</span>
              )}
            </button>
          );
        })}
      </div>
    );
  };

  /**
   * Render feeling selector
   */
  const renderFeelingSelector = () => {
    if (!showFeelingSelector) return null;

    return (
      <div className="mb-4 bg-[var(--color-bg-secondary)] rounded-lg p-4">
        <h4 className="text-sm font-medium text-[var(--color-text-primary)] mb-2">How are you feeling?</h4>
        <div className="grid grid-cols-2 gap-2">
          {FEELINGS.map((feeling, index) => (
            <button
              key={index}
              onClick={() => selectFeeling(feeling)}
              className="flex items-center space-x-2 p-2 hover:bg-[var(--color-bg-hover)] rounded-lg transition-colors text-left"
            >
              <span className="text-xl">{feeling.emoji}</span>
              <span className="text-sm font-medium text-[var(--color-text-primary)]">{feeling.text}</span>
            </button>
          ))}
        </div>
      </div>
    );
  };

  /* ========================================
     MAIN RENDER
     ======================================== */

  return (
    <div className="fixed inset-0 bg-black bg-opacity-40 z-50 flex items-center justify-center p-4">
      <div className="bg-[var(--color-bg-primary)] rounded-lg w-full max-w-lg max-h-[95vh] overflow-hidden flex flex-col shadow-2xl">

        {/* ========== HEADER ========== */}
        <div className="flex items-center justify-between px-4 py-3 border-b border-[var(--color-border-primary)]">
          <div className="w-8"></div>
          <h2 className="text-xl font-bold text-[var(--color-text-primary)]">Create post</h2>
          <button
            onClick={closeModal}
            className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
          >
            <X className="w-6 h-6 text-[var(--color-text-secondary)]" />
          </button>
        </div>

        {/* ========== CONTENT ========== */}
        <div className="flex-1 overflow-y-auto">
          <div className="p-4">

            {/* User Info Section */}
            <div className="flex items-center space-x-3 mb-4">
              <img
                src={CURRENT_USER.avatar}
                alt={CURRENT_USER.name}
                className="w-10 h-10 rounded-full object-cover"
              />
              <div className="flex-1">
                <div className="flex items-center space-x-1">
                  <p className="font-semibold text-[var(--color-text-primary)]">{CURRENT_USER.name}</p>
                  {selectedFeeling && (
                    <>
                      <span className="text-[var(--color-text-secondary)]">is feeling</span>
                      <span className="text-[var(--color-primary)] font-medium">
                        {selectedFeeling.emoji} {selectedFeeling.text}
                      </span>
                    </>
                  )}
                </div>

                {/* Tagged Friends Display */}
                {taggedFriends.length > 0 && (
                  <div className="flex items-center space-x-1 text-sm text-[var(--color-text-secondary)]">
                    <span>with</span>
                    {taggedFriends.map((friend, index) => (
                      <span key={friend.id} className="text-[var(--color-primary)]">
                        {friend.name}{index < taggedFriends.length - 1 ? ',' : ''}
                      </span>
                    ))}
                  </div>
                )}

                {/* Location Display */}
                {location && (
                  <div className="flex items-center space-x-1 text-sm text-[var(--color-text-secondary)]">
                    <MapPin className="w-3 h-3" />
                    <span>at {location}</span>
                  </div>
                )}

                {/* Privacy Button */}
                <button
                  onClick={() => togglePanel('privacy')}
                  className="flex items-center space-x-1 text-sm text-[var(--color-text-secondary)] bg-[var(--color-bg-secondary)] px-2 py-1 rounded mt-1 hover:bg-[var(--color-bg-hover)] transition-colors"
                >
                  <PrivacyIcon className="w-3 h-3" />
                  <span>{selectedPrivacy.label}</span>
                  <svg className="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                    <path fillRule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clipRule="evenodd" />
                  </svg>
                </button>
              </div>
            </div>

            {/* Dynamic Content Sections */}
            {renderPrivacySelector()}

            {/* Post Content Textarea */}
            <textarea
              value={postContent}
              onChange={(e) => setPostContent(e.target.value)}
              placeholder={`What's on your mind, ${firstName}?`}
              className="w-full min-h-[120px] text-xl placeholder-[var(--color-text-placeholder)] text-[var(--color-text-primary)] resize-none focus:outline-none mb-4 leading-relaxed bg-transparent"
              autoFocus
            />

            {/* Dynamic UI Sections */}
            {renderLocationInput()}
            {renderTaggedFriends()}
            {renderMediaPreview()}
            {renderEmojiPicker()}
            {renderFriendTagger()}
            {renderFeelingSelector()}

            {/* Add to Post Section */}
            <div className="border border-[var(--color-border-primary)] rounded-lg p-3 mb-4">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium text-[var(--color-text-primary)]">Add to your post</span>
                <div className="flex space-x-1">
                  {/* Photo Button */}
                  <button
                    onClick={() => fileInputRef.current?.click()}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Photo"
                  >
                    <Image className="w-6 h-6 text-green-500" />
                  </button>

                  {/* Video Button */}
                  <button
                    onClick={() => videoInputRef.current?.click()}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Video"
                  >
                    <Video className="w-6 h-6 text-red-500" />
                  </button>

                  {/* Tag Friends Button */}
                  <button
                    onClick={() => togglePanel('friends')}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Tag Friends"
                  >
                    <Users className="w-6 h-6 text-blue-500" />
                  </button>

                  {/* Feeling Button */}
                  <button
                    onClick={() => togglePanel('feelings')}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Feeling/Activity"
                  >
                    <Smile className="w-6 h-6 text-yellow-500" />
                  </button>

                  {/* Location Button */}
                  <button
                    onClick={() => togglePanel('location')}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Check in"
                  >
                    <MapPin className="w-6 h-6 text-red-500" />
                  </button>

                  {/* Emoji Button */}
                  <button
                    onClick={() => togglePanel('emoji')}
                    className="p-2 hover:bg-[var(--color-bg-hover)] rounded-full transition-colors"
                    title="Emoji"
                  >
                    <MoreHorizontal className="w-6 h-6 text-[var(--color-text-secondary)]" />
                  </button>
                </div>
              </div>
            </div>

            {/* Hidden File Inputs */}
            <input
              type="file"
              ref={fileInputRef}
              onChange={handleImageSelect}
              multiple
              accept="image/*"
              className="hidden"
            />
            <input
              type="file"
              ref={videoInputRef}
              onChange={handleVideoSelect}
              multiple
              accept="video/*"
              className="hidden"
            />
          </div>
        </div>

        {/* ========== FOOTER ========== */}
        <div className="p-4 border-t border-[var(--color-border-primary)]">
          <button
            onClick={handlePost}
            disabled={!hasContent}
            className={`w-full py-2 px-4 rounded-lg font-medium transition-all ${hasContent
              ? 'bg-[var(--color-primary)] hover:bg-[var(--color-primary-hover)] text-white hover:shadow-lg'
              : 'bg-[var(--color-bg-disabled)] text-[var(--color-text-disabled)] cursor-not-allowed'
              }`}
          >
            Post
          </button>
        </div>
      </div>
    </div>
  );
});

// Set display name for debugging
CreatePostPopover.displayName = 'CreatePostPopover';

export default CreatePostPopover;