// features/chat/hooks/useChatState.js
import { useState, useRef } from 'react';
import { mockChatData } from '../services/mockData';

export const useChatState = () => {
  // Chat state
  const [selectedChat, setSelectedChat] = useState(null);
  const [chats] = useState(mockChatData);
  const [messages, setMessages] = useState([]);
  
  // UI state
  const [isChatListVisible, setIsChatListVisible] = useState(true);
  const [showProfile, setShowProfile] = useState(false);
  const [showEmojiPicker, setShowEmojiPicker] = useState(false);
  const [showReactionPicker, setShowReactionPicker] = useState(null);
  const [isTyping, setIsTyping] = useState(false);
  
  // Input state
  const [message, setMessage] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  
  // Refs
  const fileInputRef = useRef(null);
  const imageInputRef = useRef(null);

  // Computed values
  const filteredChats = chats.filter(chat =>
    chat.name.toLowerCase().includes(searchTerm.toLowerCase())
  );
  
  const selectedChatData = chats.find(c => c.id === selectedChat);

  // Handlers
  const handleSelectChat = (chatId) => {
    setSelectedChat(chatId);
    const chat = chats.find(c => c.id === chatId);
    setMessages(chat?.messages || []);
    setShowProfile(false);
    
    // Hide chat list on mobile when a chat is selected
    if (window.innerWidth < 768) {
      setIsChatListVisible(false);
    }
  };

  const handleBackToList = () => {
    setIsChatListVisible(true);
    setSelectedChat(null);
    setShowProfile(false);
  };

  const handleSendMessage = () => {
    if (message.trim() && selectedChat) {
      const newMessage = {
        id: messages.length + 1,
        text: message,
        sender: 'me',
        timestamp: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
        read: true
      };
      setMessages([...messages, newMessage]);
      setMessage('');
    }
  };

  const handleFileUpload = (event) => {
    const file = event.target.files[0];
    if (file && selectedChat) {
      const newMessage = {
        id: messages.length + 1,
        type: 'file',
        fileName: file.name,
        fileSize: (file.size / (1024 * 1024)).toFixed(1) + ' MB',
        sender: 'me',
        timestamp: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
        read: true
      };
      setMessages([...messages, newMessage]);
    }
  };

  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    if (file && selectedChat) {
      const reader = new FileReader();
      reader.onload = (e) => {
        const newMessage = {
          id: messages.length + 1,
          type: 'image',
          src: e.target.result,
          sender: 'me',
          timestamp: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
          read: true
        };
        setMessages([...messages, newMessage]);
      };
      reader.readAsDataURL(file);
    }
  };

  const handleReaction = (messageId, reaction) => {
    setMessages(prev => prev.map(msg => {
      if (msg.id === messageId) {
        const reactions = { ...msg.reactions } || {};
        reactions[reaction] = (reactions[reaction] || 0) + 1;
        return { ...msg, reactions };
      }
      return msg;
    }));
    setShowReactionPicker(null);
  };

  const handleEmojiClick = (emoji) => {
    setMessage(prev => prev + emoji);
    setShowEmojiPicker(false);
  };

  const handleCall = () => {
    alert('Voice call feature would be implemented here');
  };

  const handleVideoCall = () => {
    alert('Video call feature would be implemented here');
  };

  const startTyping = () => {
    setIsTyping(true);
    // Simulate stopping typing after 3 seconds
    setTimeout(() => setIsTyping(false), 3000);
  };

  return {
    // State
    selectedChat,
    chats,
    messages,
    filteredChats,
    selectedChatData,
    isChatListVisible,
    showProfile,
    showEmojiPicker,
    showReactionPicker,
    isTyping,
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
    startTyping,
    
    // Setters
    setMessage,
    setSearchTerm,
    setShowProfile,
    setShowEmojiPicker,
    setShowReactionPicker,
    
    // Refs
    fileInputRef,
    imageInputRef
  };
};