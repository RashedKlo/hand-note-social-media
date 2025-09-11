
import { useState } from "react";
import ProfileHeader from "../../features/profile/components/Header/ProfileHeader";
import LeftSidebar from "../../features/profile/components/Sidebar/LeftSidebar";
import MainContent from "../../features/profile/components/Content/MainContent";

const profileUser = {
  id: 2,
  name: 'Michael Johnson',
  bio: 'Passionate about technology, travel, and making meaningful connections. Always learning something new! 🌟',
  location: 'San Francisco, CA',
  work: 'Senior Software Engineer at Google',
  education: 'Stanford University',
  relationship: 'In a relationship',
  website: 'www.michaeljohnson.dev',
  joinedDate: 'Joined May 2018',
  avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=200&h=200&fit=crop&crop=face',
  coverPhoto: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=1200&h=400&fit=crop',
  friendsCount: 1247,
  photosCount: 156,
  followersCount: 2341,
  followingCount: 892
};

const mockFriends = [
  { id: 1, name: "Alex Johnson", avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&h=150&fit=crop&crop=face", mutualFriends: 12 },
  { id: 2, name: "Mark Lee", avatar: "https://images.unsplash.com/photo-1519085360753-af0119f7cbe7?w=150&h=150&fit=crop&crop=face", mutualFriends: 8 },
  { id: 3, name: "Samuel Gomez", avatar: "https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=150&h=150&fit=crop&crop=face", mutualFriends: 15 },
  { id: 4, name: "David Kim", avatar: "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&h=150&fit=crop&crop=face", mutualFriends: 6 },
  { id: 5, name: "Oliver Brown", avatar: "https://images.unsplash.com/photo-1463453091185-61582044d556?w=150&h=150&fit=crop&crop=face", mutualFriends: 20 },
  { id: 6, name: "James Wilson", avatar: "https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?w=150&h=150&fit=crop&crop=face", mutualFriends: 3 },
  { id: 7, name: "Ethan Davis", avatar: "https://images.unsplash.com/photo-1566492031773-4f4e44671d66?w=150&h=150&fit=crop&crop=face", mutualFriends: 11 },
  { id: 8, name: "Ryan Miller", avatar: "https://images.unsplash.com/photo-1507591064344-4c6ce005b128?w=150&h=150&fit=crop&crop=face", mutualFriends: 7 },
  { id: 9, name: "Isaac Garcia", avatar: "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=150&h=150&fit=crop&crop=face", mutualFriends: 14 },
];

const mockPhotos = [
  "https://images.unsplash.com/photo-1469474968028-56623f02e42e?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1501436513145-30f24e19fcc4?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1518837695005-2083093ee35b?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1472214103451-9374bd1c798e?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1441974231531-c6227db76b6e?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1530549387789-4c1017266635?w=300&h=300&fit=crop",
  "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=300&h=300&fit=crop",
];

const mockPosts = [
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
            content: 'Thank you so much Alex! Really appreciate your kind words 😊',
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
];

const Profile = () => {

  const [activeTab, setActiveTab] = useState("posts");

  return (
    <div className="bg-gray-100 min-h-screen">


      <main className="max-w-6xl mx-auto px-0 sm:px-6 lg:px-8 pb-8">
        {/* Profile Header */}


        <ProfileHeader setActiveTab={setActiveTab} activeTab={activeTab} profileUser={profileUser} mockFriends={mockFriends} posts={mockPosts} />
        {/* Content Area */}
        <div className="mt-4 grid grid-cols-1 lg:grid-cols-3 gap-4 px-4 sm:px-0">
          {/* Left Sidebar - About */}
          <LeftSidebar profileUser={profileUser} mockFriends={mockFriends} mockPhotos={mockPhotos} />

          {/* Main Content */}
          <MainContent profileUser={profileUser} activeTab={activeTab} mockFriends={mockFriends} mockPhotos={mockPhotos} />
        </div>

      </main>
    </div>
  )
};
export default Profile;