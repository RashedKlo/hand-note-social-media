// features/chat/Chat.jsx
import React from 'react';
import { useChatState } from '../../features/chat/hooks/useChatState';
import ChatSidebar from '../../features/chat/components/ChatSidebar/ChatSidebar';
import ChatInterface from '../../features/chat/components/ChatInterface/ChatInterface';
import EmptyState from '../../features/chat/components/EmptyState/EmptyState';

const Chat = () => {
  const {
    // Chat state
    selectedChat,
    chats,
    messages,
    filteredChats,
    selectedChatData,
    
    // UI state
    isChatListVisible,
    showProfile,
    showEmojiPicker,
    showReactionPicker,
    isTyping,
    
    // Input state
    message,
    searchTerm,
    
    // Handlers
    handleSelectChat,
    handleBackToList,
    handleSendMessage,
    handleFileUpload,
    handleImageUpload,
    handleReaction,
    handleEmojiClick,
    handleCall,
    handleVideoCall,
    setMessage,
    setSearchTerm,
    setShowProfile,
    setShowEmojiPicker,
    setShowReactionPicker,
    startTyping,
    
    // Refs
    fileInputRef,
    imageInputRef
  } = useChatState();

  return (
    <div className="flex h-screen bg-white overflow-hidden">
      {/* Chat List Sidebar */}
      <ChatSidebar
        isVisible={isChatListVisible}
        chats={filteredChats}
        selectedChat={selectedChat}
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        onChatSelect={handleSelectChat}
      />

      {/* Main Chat Interface */}
      <div className={`${
        !isChatListVisible || selectedChat ? 'flex' : 'hidden'
      } md:flex flex-col flex-1 bg-white min-w-0`}>
        {selectedChat ? (
          <ChatInterface
            selectedChatData={selectedChatData}
            messages={messages}
            message={message}
            showProfile={showProfile}
            showEmojiPicker={showEmojiPicker}
            showReactionPicker={showReactionPicker}
            isTyping={isTyping}
            onBackToList={handleBackToList}
            onSendMessage={handleSendMessage}
            onMessageChange={setMessage}
            onCall={handleCall}
            onVideoCall={handleVideoCall}
            onToggleProfile={() => setShowProfile(!showProfile)}
            onFileUpload={handleFileUpload}
            onImageUpload={handleImageUpload}
            onReaction={handleReaction}
            onEmojiClick={handleEmojiClick}
            onToggleEmojiPicker={() => setShowEmojiPicker(!showEmojiPicker)}
            onToggleReactionPicker={setShowReactionPicker}
            onStartTyping={startTyping}
            fileInputRef={fileInputRef}
            imageInputRef={imageInputRef}
          />
        ) : (
          <EmptyState />
        )}
      </div>

      {/* Click outside to close dropdowns */}
      {(showEmojiPicker || showReactionPicker) && (
        <div 
          className="fixed inset-0 z-0" 
          onClick={() => {
            setShowEmojiPicker(false);
            setShowReactionPicker(null);
          }}
        />
      )}
    </div>
  );
};

export default Chat;