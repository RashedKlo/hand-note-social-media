// features/chat/services/mockData.js

export const mockChatData = [
  {
    id: 1,
    name: 'Samuel Johnson',
    avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&h=150&fit=crop&crop=face',
    lastMessage: 'Hey! How are you doing?',
    timestamp: '2m',
    unread: 2,
    online: true,
    bio: 'Software Developer at Tech Corp | Coffee enthusiast ☕',
    phone: '+1 (555) 123-4567',
    email: 'samuel.johnson@email.com',
    mutualFriends: 12,
    messages: [
      { 
        id: 1, 
        text: 'Hey there! How\'s your day going?', 
        sender: 'other', 
        timestamp: '10:30 AM', 
        read: true, 
        reactions: { '👍': 1 } 
      },
      { 
        id: 2, 
        text: 'Hi Samuel! Pretty good, just working on some projects. How about you?', 
        sender: 'me', 
        timestamp: '10:32 AM', 
        read: true 
      },
      { 
        id: 3, 
        text: 'I\'m doing great! Just finished my morning workout 💪 Feeling energized!', 
        sender: 'other', 
        timestamp: '10:35 AM', 
        read: true, 
        reactions: { '🔥': 1, '💯': 1 } 
      },
      { 
        id: 4, 
        text: 'That\'s awesome! What kind of workout did you do? I need some motivation 😅', 
        sender: 'me', 
        timestamp: '10:36 AM', 
        read: true 
      },
      { 
        id: 5, 
        text: 'Hey! How are you doing?', 
        sender: 'other', 
        timestamp: '10:45 AM', 
        read: false 
      },
      { 
        id: 6, 
        type: 'image', 
        src: 'https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=300&h=200&fit=crop', 
        sender: 'other', 
        timestamp: '10:50 AM', 
        read: false, 
        caption: 'My workout setup at home! 💪 You should join me sometime!' 
      }
    ]
  },
  {
    id: 2,
    name: 'Mike Chen',
    avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&h=150&fit=crop&crop=face',
    lastMessage: 'Thanks for the help earlier!',
    timestamp: '1h',
    unread: 0,
    online: false,
    bio: 'UX Designer & Coffee Enthusiast | Always learning something new 🚀',
    phone: '+1 (555) 987-6543',
    email: 'mike.chen@email.com',
    mutualFriends: 8,
    messages: [
      { 
        id: 1, 
        text: 'Hey! Can you help me with the new project wireframes?', 
        sender: 'other', 
        timestamp: '9:15 AM', 
        read: true 
      },
      { 
        id: 2, 
        text: 'Sure! What do you need help with specifically?', 
        sender: 'me', 
        timestamp: '9:20 AM', 
        read: true 
      },
      { 
        id: 3, 
        type: 'file', 
        fileName: 'project_wireframes_v2.pdf', 
        fileSize: '2.4 MB', 
        sender: 'other', 
        timestamp: '9:25 AM', 
        read: true 
      },
      { 
        id: 4, 
        text: 'These look great! I\'ll review them and get back to you with feedback.', 
        sender: 'me', 
        timestamp: '9:30 AM', 
        read: true 
      },
      { 
        id: 5, 
        text: 'Thanks for the help earlier! Really appreciate your input 🙏', 
        sender: 'other', 
        timestamp: '9:45 AM', 
        read: true, 
        reactions: { '❤️': 1 } 
      },
    ]
  },
  {
    id: 3,
    name: 'Ethan Davis',
    avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&h=150&fit=crop&crop=face',
    lastMessage: 'See you tomorrow! 👋',
    timestamp: '3h',
    unread: 0,
    online: true,
    bio: 'Marketing Manager | Travel Lover 🌍 | Always planning the next adventure',
    phone: '+1 (555) 456-7890',
    email: 'ethan.davis@email.com',
    mutualFriends: 15,
    messages: [
      { 
        id: 1, 
        text: 'Hey! Are we still meeting tomorrow for the presentation review?', 
        sender: 'other', 
        timestamp: '7:30 AM', 
        read: true 
      },
      { 
        id: 2, 
        text: 'Yes! 2 PM at the usual coffee shop, right?', 
        sender: 'me', 
        timestamp: '7:35 AM', 
        read: true 
      },
      { 
        id: 3, 
        text: 'Perfect! I\'ll bring the latest designs. Can\'t wait to get your thoughts!', 
        sender: 'other', 
        timestamp: '7:40 AM', 
        read: true 
      },
      { 
        id: 4, 
        text: 'See you tomorrow! 👋 This is going to be great!', 
        sender: 'other', 
        timestamp: '7:42 AM', 
        read: true 
      },
    ]
  },
  {
    id: 4,
    name: 'Alex Thompson',
    avatar: 'https://images.unsplash.com/photo-1519085360753-af0119f7cbe7?w=150&h=150&fit=crop&crop=face',
    lastMessage: 'The presentation went amazing!',
    timestamp: '1d',
    unread: 1,
    online: false,
    bio: 'Product Manager | Tech enthusiast | Weekend photographer 📸',
    phone: '+1 (555) 321-9876',
    email: 'alex.thompson@email.com',
    mutualFriends: 5,
    messages: [
      { 
        id: 1, 
        text: 'Good luck with your presentation today! You\'ve got this! 💪', 
        sender: 'me', 
        timestamp: 'Yesterday', 
        read: true 
      },
      { 
        id: 2, 
        text: 'The presentation went amazing! Thanks for all your support and feedback!', 
        sender: 'other', 
        timestamp: 'Yesterday', 
        read: false 
      },
    ]
  },
  {
    id: 5,
    name: 'Lucas Wang',
    avatar: 'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=150&h=150&fit=crop&crop=face',
    lastMessage: 'Let\'s grab lunch next week!',
    timestamp: '2d',
    unread: 0,
    online: true,
    bio: 'Data Scientist | AI Research | Marathon runner 🏃‍♂️',
    phone: '+1 (555) 654-3210',
    email: 'lucas.wang@email.com',
    mutualFriends: 20,
    messages: [
      { 
        id: 1, 
        text: 'Hey! How was your weekend? Did you finish the marathon training?', 
        sender: 'other', 
        timestamp: '2 days ago', 
        read: true 
      },
      { 
        id: 2, 
        text: 'It was great! Completed a 20-mile run. Getting ready for the big race!', 
        sender: 'me', 
        timestamp: '2 days ago', 
        read: true,
        reactions: { '🔥': 1, '💯': 1 }
      },
      { 
        id: 3, 
        text: 'That\'s incredible! Let\'s grab lunch next week and you can tell me all about it!', 
        sender: 'other', 
        timestamp: '2 days ago', 
        read: true 
      },
    ]
  }
];