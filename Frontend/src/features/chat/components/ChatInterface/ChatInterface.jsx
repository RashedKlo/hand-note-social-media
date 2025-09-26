// features/chat/components/ChatInterface/ChatInterface.jsx
import React from 'react';
import ChatHeader from './ChatHeader';
import MessageList from './MessageList';
import MessageInput from './MessageInput';
import ProfileSidebar from '../ProfileSidebar/ProfileSidebar';

const ChatInterface = ({
  selectedChatData,
  messages,
  message,
  showProfile,
  showEmojiPicker,
  showReactionPicker,
  isTyping,
  onBackToList,
  onSendMessage,
  onMessageChange,
  onCall,
  onVideoCall,
  onToggleProfile,
  onFileUpload,
  onImageUpload,
  onReaction,
  onEmojiClick,
  onToggleEmojiPicker,
  onToggleReactionPicker,
  onStartTyping,
  fileInputRef,
  imageInputRef
}) => {
  return (
    <>
      {/* Chat Header */}
      <ChatHeader
        chatData={selectedChatData}
        isTyping={isTyping}
        onBackToList={onBackToList}
        onCall={onCall}
        onVideoCall={onVideoCall}
        onToggleProfile={onToggleProfile}
        showProfile={showProfile}
      />

      <div className="flex flex-1 overflow-hidden">
        {/* Messages Area */}
        <div className="flex-1 flex flex-col min-w-0">
          <MessageList
            messages={messages}
            showReactionPicker={showReactionPicker}
            onReaction={onReaction}
            onToggleReactionPicker={onToggleReactionPicker}
          />

          <MessageInput
            message={message}
            showEmojiPicker={showEmojiPicker}
            onMessageChange={onMessageChange}
            onSendMessage={onSendMessage}
            onFileUpload={onFileUpload}
            onImageUpload={onImageUpload}
            onEmojiClick={onEmojiClick}
            onToggleEmojiPicker={onToggleEmojiPicker}
            onStartTyping={onStartTyping}
            fileInputRef={fileInputRef}
            imageInputRef={imageInputRef}
          />
        </div>

        {/* Profile Sidebar */}
        {showProfile && selectedChatData && (
          <ProfileSidebar
            chatData={selectedChatData}
            onClose={() => onToggleProfile()}
            onCall={onCall}
            onVideoCall={onVideoCall}
          />
        )}
      </div>
    </>
  );
};

export default ChatInterface;